using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM
{
    public class RelayCommand : ICommand
    {
        #region Fields

        protected readonly Action<object> _execute;
        protected readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public interface ISupportUndoCommand : ICommand
        {
            void UnExecute(object parameter);
        }

        public class SupportUndoRelayCommand : RelayCommand, ISupportUndoCommand
        {
            protected readonly Action<object> _unexecute;

            /// <summary>
            /// Creates a new command that can always execute.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            public SupportUndoRelayCommand(Action<object> execute, Action<object> unexecute)
                : this(execute, unexecute, null)
            {
            }

            /// <summary>
            /// Creates a new command.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            /// <param name="canExecute">The execution status logic.</param>
            public SupportUndoRelayCommand(Action<object> execute, Action<object> unexecute, Predicate<object> canExecute)
                : base(execute, canExecute)
            {
                _unexecute = unexecute;
            }

            public void UnExecute(object parameter)
            {
                _unexecute(parameter);
            }
        }

        public class UndoRedoItem<T>
        {
            public ISupportUndoCommand Cmd { get; private set; }
            public T NewItem { get; private set; } // reference of new item or updated item.
            public T OldItem { get; private set; } // cloned object of deleted or updated item.

            public UndoRedoItem(T old, T @new, ISupportUndoCommand cmd)
            {
                NewItem = @new;
                OldItem = old;
                Cmd = cmd;
            }

            public void SwapOldAndNew()
            {
                T temp = NewItem;
                NewItem = OldItem;
                OldItem = temp;
            }
        }

        public class UndoManager<T> where T : class
        {
            Stack<UndoRedoItem<T>> _undoStack = new Stack<UndoRedoItem<T>>();
            Stack<UndoRedoItem<T>> _redoStack = new Stack<UndoRedoItem<T>>();

            public bool CanUndo { get { return (_undoStack.Count > 0); } }
            public bool CanRedo { get { return (_redoStack.Count > 0); } }

            public void Redo()
            {
                UndoRedoItem<T> top = (UndoRedoItem<T>)_redoStack.Pop();
                top.Cmd.Execute(top);

                top.SwapOldAndNew();
                _undoStack.Push(top);
                //RemoveChangeLog(top);
            }

            public void Undo()
            {
                Undo(true);
            }

            public void Undo(bool canRedo)
            {
                UndoRedoItem<T> top = _undoStack.Pop();
                top.Cmd.UnExecute(top);

                if (canRedo == true)
                {
                    top.SwapOldAndNew();
                    _redoStack.Push(top);
                }

                //AddChangeLog(top);
            }

            public void AddHistory(UndoRedoItem<T> history)
            {
                _undoStack.Push(history);
                _redoStack.Clear();

                //AddChangeLog(history);
            }

            public UndoRedoItem<T> GetLastUndoItem()
            {
                if (_undoStack.Count > 0)
                    return _undoStack.Peek();
                return null;
            }

            public void Clear()
            {
                _undoStack.Clear();
                _redoStack.Clear();
            }

            #endregion // ICommand Members
        }
    }
}

using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mirroring.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        public static IntPtr main_hWnd;          //MainWindow Handle
        public static IntPtr sub_hWnd;           //SubWindow Handle
        static public IntPtr highlight_hWnd;     //HighlightWindow hWnd







        private double _leftMainWindow;
        public double LeftMainWindow
        {
            get { return _leftMainWindow; }
            set
            {
                _leftMainWindow = value;
                OnPropertyChanged("LeftMainWindow");
            }
        }

        private double _topMainWindow;
        public double TopMainWindow
        {
            get { return _topMainWindow; }
            set
            {
                _topMainWindow = value;
                OnPropertyChanged("TopMainWindow");
            }
        }

        private bool _isToggledOnCapture;
        public bool IsToggledOnCapture
        {
            get
            {
                return _isToggledOnCapture;
            }
            set
            {
                _isToggledOnCapture = value;
                OnPropertyChanged("IsToggledOnCapture");
            }
        }


        /// <summary>
        /// Capture 시작 Command
        /// </summary>
        public RelayCommand _startCaptureCommand;
        public ICommand StartCaptureCommand
        {
            get
            {
                if(_startCaptureCommand == null)
                {
                    _startCaptureCommand = new RelayCommand(StartCaptureCommand_Execute, canExecute => true);
                }
                return _startCaptureCommand;
            }
        }

        /// <summary>
        /// MainWindow Capture Start
        /// </summary>
        /// <param name="obj"></param>
        private void StartCaptureCommand_Execute(object obj)
        {
            if(IsToggledOnCapture)
            {

            }
        }

        public MainWindowVM(MainWindow mainWindow, IntPtr rHwnd)
        {
            //LeftMainWindow = 0;
            //TopMainWindow = 0;
        }

        /// <summary>
        /// View Model Async Close
        /// </summary>
        public event EventHandler RequestClose;
        public void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }

}

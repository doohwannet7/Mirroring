using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirroring.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
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

using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Mirroring.ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        public static IntPtr main_hWnd;          //MainWindow Handle
        public static IntPtr sub_hWnd;           //SubWindow Handle
        public static IntPtr highlight_hWnd;     //HighlightWindow hWnd
        public static MainWindow _mainWindow;    //MainWindow 
        public static IntPtr _hWnd = IntPtr.Zero;       //Capture 되고있는 Window hWnd




        public Screen[] screenArr;                      //Monitor Array Windows Form Add(Referece)
        public string mainMonitorNum;                   //MainWindow 뷰잉되는 모니터 번호
        public string tabletMonitorNum;                 //tablet Monitor 번호(=SubWindow 뷰잉되는 모니터 번호)


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

        private int _subWindowHeight;
        public int SubWindowHeight
        {
            get { return _subWindowHeight; }
            set
            {
                _subWindowHeight = value;
                OnPropertyChanged("SubWindowHeight");
            }
        }
        private int _subWindowWidth;
        public int SubWindowWidth
        {
            get { return _subWindowWidth; }
            set
            {
                _subWindowWidth = value;
                OnPropertyChanged("SubWindowWidth");
            }
        }


        /// <summary>
        /// SubWindow X좌표
        /// </summary>
        public double LeftSubWindow
        {
            get { return _leftSubWindow; }
            set
            {
                _leftSubWindow = value;
                OnPropertyChanged("LeftSubWindow");
            }
        }
        private double _leftSubWindow;

        /// <summary>
        /// SubWindow Y 좌표
        /// </summary>
        public double TopSubWindow
        {
            get { return _topSubWindow; }
            set
            {
                _topSubWindow = value;
                OnPropertyChanged("TopSubWindow");
            }
        }
        private double _topSubWindow;


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
            try
            {
                _mainWindow = mainWindow;
                _hWnd = rHwnd;

                SettingMonitor();



            }catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void SettingMonitor()
        {
            //메인모니터 설정
            FindMainMonitor();

            //모니터 이동
            MoveTargetMonitor();
        }

        /// <summary>
        /// Main Primary Screen 위치 셋팅, Sub Screen 셋팅
        /// </summary>
        private void MoveTargetMonitor()
        {
            System.Windows.Forms.Screen[] sc = System.Windows.Forms.Screen.AllScreens;
            int mainScreenNum=0, tabletScreenNum=0;

            if (string.IsNullOrEmpty(mainMonitorNum) == false && string.IsNullOrEmpty(tabletMonitorNum) == false)
            {
                mainScreenNum = Convert.ToInt32(mainMonitorNum);
                tabletScreenNum = Convert.ToInt32(tabletMonitorNum);
            }else
            {
                mainScreenNum = 1;
                tabletScreenNum = 1;
            }

            if (mainScreenNum > 0)
            {
                LeftMainWindow = sc[mainScreenNum - 1].Bounds.Location.X;
                TopMainWindow = sc[mainScreenNum - 1].Bounds.Location.Y;
            }

            if (tabletScreenNum > 0)
            {
                LeftSubWindow = sc[tabletScreenNum - 1].Bounds.Location.X;
                TopSubWindow = sc[tabletScreenNum - 1].Bounds.Location.Y;
                SubWindowHeight = sc[tabletScreenNum - 1].Bounds.Height;
                SubWindowWidth = sc[tabletScreenNum - 1].Bounds.Width;
            }
        }

        /// <summary>
        /// Monitor가 2개가 있다고 가정하고 코딩
        /// </summary>
        private void FindMainMonitor()
        {
            //메인모니터는 Primary(주모니터)
            screenArr = Screen.AllScreens;
            int monitorIndex = 1;
            foreach (Screen scrn in screenArr)
            {
                if (scrn.Primary)
                {
                    //Main Montior
                    mainMonitorNum = monitorIndex.ToString();
                }
                else
                {
                    tabletMonitorNum = monitorIndex.ToString();
                }

                monitorIndex++;
            }
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

using Mirroring.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mirroring
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowVM _mainWindowVM = null;

        //MainWindow 크기
        //스크린 캡쳐시 배율계산을 위해 초기 저장
        double oriWidth;
        double oriHeight;

        //윈도우 핸들 값
        IntPtr _rHwnd;

        //Handle Init Data
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        const Int32 SW_SHOWNORMAL = 3;




        #region dll setting

        [DllImport("user32.dll")]
        public static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow); //Window Show
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags); //Window 창 위치 설정

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            InitMainWindow();
        }

        /// <summary>
        /// 초기값 설정
        /// </summary>
        private void InitMainWindow()
        {
            _rHwnd = IntPtr.Zero;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            //MainWindow View Model Data Context
            _mainWindowVM = new MainWindowVM(this, _rHwnd);
            this.DataContext = _mainWindowVM;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            oriWidth = this.Width;
            oriHeight = this.Height;

            //ShowWindow(_rHwnd, SW_SHOWNORMAL);

            //SetWindowPos함수는 Window의 형태, 크기, 표시Leve등 Window의 속성을 변경하는 함수
            //SetWindowPos(_rHwnd, HWND_TOP, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _mainWindowVM.OnRequestClose();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}

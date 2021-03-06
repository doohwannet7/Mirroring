﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mirroring.ViewModel
{
    /// <summary>
    /// SubWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SubWindow : Window
    {

        double oriWidth, oriHeight;
        MainWindowVM _mainWindowVM = null;
        public static IntPtr subHandle;


        public SubWindow(MainWindowVM mainWindowVM)
        {
            InitializeComponent();
            _mainWindowVM = mainWindowVM;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Main Window의 
            oriHeight = _mainWindowVM.SubWindowHeight;
            oriWidth = _mainWindowVM.SubWindowWidth;


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _mainWindowVM.OnRequestClose();
        }
    }
}

using DataManager.Common.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pomodoroTimer.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region Field

        private WindowState _currentWindowState;

        #endregion

        #region Property

        public ViewModelBase CurrentViewModel { get;  }

        public WindowState CurrentWindowState
        {
            get { return _currentWindowState; }
            set { _currentWindowState = value; OnPropertyChanged(nameof(CurrentWindowState)); }
        }

        #endregion

        #region Command

        public ICommand WindowMinimizeCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    WindowMinimize();
                }, delegate () { return true; });
            }
        }


        public ICommand WindowMaxmizeCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    WindowMaximize();
                }, delegate () { return true; });
            }
        }


        public ICommand LoginWindowOpenCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    LoginWindowOpen();
                }, delegate () { return true; });
            }
        }

        #endregion

        #region Event Handler

        #endregion

        #region Constructor

        public MainViewModel()
        {
            _initialize();
        }

        #endregion

        #region IDispose Method

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

        #region Initialize Method

        private void _initialize()
        {
        }

        #endregion

        #region User Method

        #endregion

        #region Command Method


        private void WindowMinimize()
        {
            CurrentWindowState = WindowState.Minimized;
        }

        private void WindowMaximize()
        {

            if (CurrentWindowState == WindowState.Maximized)
            {
                CurrentWindowState = WindowState.Normal;
            }
            else
            {
                CurrentWindowState = WindowState.Maximized;
            }
        }


        private void LoginWindowOpen()
        {
            View.LoginView login = new View.LoginView();
            login.Show();
        }



        #endregion

        #region Event Method

        #endregion

    }
}
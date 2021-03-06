using pomodoroTimer.View;
using PomodoroTimer.Common.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace pomodoroTimer.ViewModel
{
    public class MainViewModel : BasePropertyChanged
    {


        // https://ddka.tistory.com/entry/C-WPF%EC%97%90%EC%84%9C-user-control%EA%B3%BC-data-binding%EC%97%90-%EB%8C%80%ED%95%9C-%EA%B0%84%EB%8B%A8%ED%95%9C-%EC%84%A4%EB%AA%85
        // https://narup.tistory.com/68
        // 위 링크를 통해 데이터 바인딩과 UserControl이 어떻게 작동 되는지 공부
        // UserControl은 데이터만 변경 가능한 것 같음
        // UserControl 사용 X

        // https://stackoverflow.com/questions/19654295/wpf-mvvm-navigate-views
        // 위 링크의 답변 천천히 읽어보면서 Multiple View에 대한 방법 이해
        #region Field


        #endregion

        #region Property

        public WindowState CurrentWindowState
        {
            get { return _currentWindowState; }
            set { _currentWindowState = value; OnPropertyChanged(nameof(CurrentWindowState)); }
        }

        private WindowState _currentWindowState;
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


        public ICommand CloseCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Environment.Exit(0);  // Close all Windows in WPF App
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




        #endregion

        #region Event Method

        public void LoginMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();

            /* 전혁공부
             * ShowDialog()함수는 second window가 열렸을 때 Main window를 클릭하지 못하게 한다.
             * Show()함수는 second window를 열고 Main window 클릭이 가능하다.
             */
            login.ShowDialog();
        }

        #endregion

    }
}
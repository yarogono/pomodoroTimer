using DataManager.Common.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace pomodoroTimer.ViewModel
{
    public class MainViewModel : BasePropertyChanged
    {

        #region Field

        private WindowState _currentWindowState;

        private DispatcherTimer _timer;
        private int _sessionTime = 25;
        private TimeSpan _time;
        private string _currentStatus = "Session";

        private string _timeTextBlock = TimeSpan.FromMinutes(25).ToString("c");

        private string _timerStartButton = "▶";
        private bool _sessionActive = false;
        private bool _breaktimeActive = false;

        private int _breakTime = 5;

        private string _selectedSound = "magicRing";
        private ObservableCollection<string> _collectionSound;

        #endregion

        #region Property

        public WindowState CurrentWindowState
        {
            get { return _currentWindowState; }
            set { _currentWindowState = value; OnPropertyChanged(nameof(CurrentWindowState)); }
        }


        public string CurrentStatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; OnPropertyChanged(nameof(CurrentStatus)); }
        }



        public string CurrentTimeTextBlock
        {
            get { return _timeTextBlock; }
            set { _timeTextBlock = value; OnPropertyChanged(nameof(CurrentTimeTextBlock)); }
        }


        public string TimerStartButton
        {
            get { return _timerStartButton; }
            set { _timerStartButton = value; OnPropertyChanged(nameof(TimerStartButton)); }
        }


        public int BreakTime
        {
            get { return _breakTime; }
            set { _breakTime = value; OnPropertyChanged(nameof(BreakTime)); }
        }



        public int SessionTime
        {
            get { return _sessionTime; }
            set { _sessionTime = value; OnPropertyChanged(nameof(SessionTime)); }
        }




        public ObservableCollection<string> CollectionSound
        {
            get { return _collectionSound; }
            set { _collectionSound = value; OnPropertyChanged(nameof(CollectionSound)); }
        }



        public string SelectedSound
        {
            get { return _selectedSound; }
            set { _selectedSound = value; OnPropertyChanged(nameof(SelectedSound)); }
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


        public ICommand StartTimerCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    TimerStartTimer();
                }, delegate () { return true; });
            }
        }


        public ICommand ResetTimerCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    ResetTimer();
                }, delegate () { return true; });
            }
        }



        public ICommand BreakTimeMinusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    BreakTiimeMinus();
                }, delegate () { return true; });
            }
        }


        public ICommand BreakTimePlusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    BreakTimePlus();
                }, delegate () { return true; });
            }
        }



        public ICommand SessionTimeMinusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SessionTimeMinus();
                }, delegate () { return true; });
            }
        }

        public ICommand SessionTimePlusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SessionTimePlus();
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
            CollectionSound = new ObservableCollection<string>();
            CollectionSound.Add("magicRing");
            CollectionSound.Add("ringSound");
            CollectionSound.Add("toyTelephone");

        }

        #endregion

        #region User Metod



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


        private void TimerStartTimer()
        {
            if (_sessionActive == false && _breaktimeActive == false )
            {
                _sessionActive = true;
                TimerStartButton = "⏸";
                _time = TimeSpan.FromMinutes(SessionTime);


                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), 
                DispatcherPriority.Normal, delegate
                {
                    CurrentTimeTextBlock = _time.ToString("c");
                    if (_time == TimeSpan.Zero)
                    {
                        RingTheBell();

                        _timer.Stop();
                        CurrentStatus = "Break";


                        TimerStartButton = "▶";
                        _time = TimeSpan.FromMinutes(BreakTime);
                        CurrentTimeTextBlock = _time.ToString("c");
                        _breaktimeActive = true;
                    }
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                _timer.Start();
            }
            else if (_breaktimeActive == true)
            {
                TimerStartButton = "⏸";

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1),
                DispatcherPriority.Normal, delegate
                {
                    CurrentTimeTextBlock = _time.ToString("c");
                    if (_time == TimeSpan.Zero)
                    {
                        RingTheBell();

                        _timer.Stop();
                        CurrentStatus = "Session";


                        TimerStartButton = "▶";
                        _time = TimeSpan.FromMinutes(SessionTime);
                        CurrentTimeTextBlock = _time.ToString("c");
                        _breaktimeActive = false;
                        _sessionActive = false;
                    }
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                _timer.Start();
            }
            else
            {
                _sessionActive = false;
                _breaktimeActive = false;
                _timer.Stop();
                TimerStartButton = "▶";
            }
        }


        private void RingTheBell()
        {
            string magicRing
                        = @"D:\coding\c#\toyProject\pomodoroTimer\pomodoroTimer\RingSound\magicRing.wav";

            string ringSound
                        = @"D:\coding\c#\toyProject\pomodoroTimer\pomodoroTimer\RingSound\ringSound.wav";

            string toyTelephone
                        = @"D:\coding\c#\toyProject\pomodoroTimer\pomodoroTimer\RingSound\toyTelephone.wav";


            string plyerFilePath = "";
            switch (SelectedSound)
            {
                case "magicRing":
                    plyerFilePath = magicRing;
                    break;
                case "ringSound":
                    plyerFilePath = ringSound;
                    break;
                case "toyTelephone":
                    plyerFilePath = toyTelephone;
                    break;
            }
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new System.Uri(plyerFilePath));
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }


        private void ResetTimer()
        {
            if (_sessionActive == true && _breaktimeActive == false)
            {
                _sessionActive = false;
                _timer.Stop();
                CurrentStatus = "Session";
                TimerStartButton = "▶";
                SessionTime = 25;
                _time = TimeSpan.FromMinutes(SessionTime);
                CurrentTimeTextBlock = _time.ToString("c");
            }
            else
            {
                TimerStartButton = "▶";
                SessionTime = 25;
                _timer.Stop();
                CurrentStatus = "Session";
                _time = TimeSpan.FromMinutes(SessionTime);
                CurrentTimeTextBlock = _time.ToString("c");
            }
        }
        private void BreakTiimeMinus()
        {
            if (BreakTime > 0)
            {
                BreakTime--;
            }
        }
        private void BreakTimePlus()
        {
            if (BreakTime < 10)
            {
                BreakTime++;
            }
        }

        private void SessionTimeMinus()
        {
            if (SessionTime > 1 && _breaktimeActive == false && _sessionActive == false)
            {
                SessionTime--;

                _time = TimeSpan.FromMinutes(SessionTime);
                CurrentTimeTextBlock = _time.ToString("c");
            }
        }

        private void SessionTimePlus()
        {
            if (_breaktimeActive == false && _sessionActive == false)
            {
                SessionTime++;

                _time = TimeSpan.FromMinutes(SessionTime);
                CurrentTimeTextBlock = _time.ToString("c");
            }
        }

        #endregion

        #region Event Method

        #endregion
    }
}

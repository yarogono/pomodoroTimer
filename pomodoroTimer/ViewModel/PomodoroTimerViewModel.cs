using PomodoroTimer.Common.Lib;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace pomodoroTimer.ViewModel
{
    public class PomodoroTimerViewModel : MainViewModel
    {

        #region Field


        private DispatcherTimer _timer;
        private TimeSpan _time;


        #endregion

        #region Property


        public string CurrentStatus
        {
            get 
            { 
                return _currentStatus; 
            }
            set 
            { 
                _currentStatus = value; 
                OnPropertyChanged(nameof(CurrentStatus)); 
            }
        }

        private string _currentStatus = "Focus Stop";


        public string RemainTimeTextBlock
        {
            get 
            { 
                return _remainTimeTextBlock;
            }
            set
            {
                _remainTimeTextBlock = value;
                OnPropertyChanged(nameof(RemainTimeTextBlock));
            }
        }

        private string _remainTimeTextBlock = TimeSpan.FromMinutes(25).ToString("c");

        public string TimerStartButton
        {
            get 
            { 
                return _timerStartButton; 
            }
            set 
            { 
                _timerStartButton = value; 
                OnPropertyChanged(nameof(TimerStartButton)); 
            }
        }

        private string _timerStartButton = "▶";


        public int BreakTime
        {
            get 
            { 
                return _breakTime; 
            }
            set 
            { 
                _breakTime = value; 
                OnPropertyChanged(nameof(BreakTime)); 
            }
        }

        private int _breakTime = 5;


        public int SessionTime
        {
            get 
            { 
                return _sessionTime; 
            }
            set 
            { 
                _sessionTime = value; 
                OnPropertyChanged(nameof(SessionTime)); 
            }
        }

        private int _sessionTime = 25;



        public ObservableCollection<string> CollectionSound
        {
            get 
            { 
                return _collectionSound; 
            }
            set 
            { 
                _collectionSound = value; 
                OnPropertyChanged(nameof(CollectionSound)); 
            }
        }

        private ObservableCollection<string> _collectionSound;


        public string SelectedSound
        {
            get 
            { 
                return _selectedSound; 
            }
            set 
            { 
                _selectedSound = value; 
                OnPropertyChanged(nameof(SelectedSound)); 
            }
        }

        private string _selectedSound = "magicRing";


        #endregion

        #region Command


        public ICommand StartTimerCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _startTimer();
                }, delegate () { return true; });
            }
        }


        public ICommand ResetTimerCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _resetTimer();
                }, delegate () { return true; });
            }
        }



        public ICommand BreakTimeMinusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _breakTiimeMinus();
                }, delegate () { return true; });
            }
        }


        public ICommand BreakTimePlusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _breakTimePlus();
                }, delegate () { return true; });
            }
        }



        public ICommand SessionTimeMinusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _sessionTimeMinus();
                }, delegate () { return true; });
            }
        }

        public ICommand SessionTimePlusCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _sessionTimePlus();
                }, delegate () { return true; });
            }
        }




        #endregion

        #region Event Handler

        #endregion

        #region Constructor

        public PomodoroTimerViewModel()
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

        #endregion

        #region Command Method



        private void _startTimer()
        {

            switch (CurrentStatus)
            {
                case "Focus Stop":
                    CurrentStatus = "Focus Running";
                    TimerStartButton = "⏸";
                    _time = TimeSpan.FromMinutes(SessionTime);


                    _timer = new DispatcherTimer(new TimeSpan(0, 0, 1),
                    DispatcherPriority.Normal, delegate
                    {
                        RemainTimeTextBlock = _time.ToString("c");
                        if (_time == TimeSpan.Zero)
                        {
                            RingTheBell();

                            _timer.Stop();
                            CurrentStatus = "Breaktime Stop";


                            TimerStartButton = "▶";
                            _time = TimeSpan.FromMinutes(BreakTime);
                            RemainTimeTextBlock = _time.ToString("c");
                        }
                        _time = _time.Add(TimeSpan.FromSeconds(-1));
                    }, Application.Current.Dispatcher);

                    _timer.Start();
                    break;

                case "Focus Running":
                    CurrentStatus = "Focus Pause";
                    TimerStartButton = "▶";
                    _timer.Stop();
                    break;

                case "Focus Pause":
                    CurrentStatus = "Focus Running";
                    TimerStartButton = "⏸";
                    _timer.Start();
                    break;

                case "Breaktime Stop":
                    CurrentStatus = "Breaktime Running";
                    TimerStartButton = "⏸";

                    _timer = new DispatcherTimer(new TimeSpan(0, 0, 1),
                    DispatcherPriority.Normal, delegate
                    {
                        RemainTimeTextBlock = _time.ToString("c");
                        if (_time == TimeSpan.Zero)
                        {
                            RingTheBell();

                            _timer.Stop();
                            CurrentStatus = "Focus Stop";


                            TimerStartButton = "▶";
                            _time = TimeSpan.FromMinutes(SessionTime);
                            RemainTimeTextBlock = _time.ToString("c");
                        }
                        _time = _time.Add(TimeSpan.FromSeconds(-1));
                    }, Application.Current.Dispatcher);

                    _timer.Start();
                    break;

                case "Breaktime Running":
                    CurrentStatus = "Breaktime Pause";
                    TimerStartButton = "▶";
                    _timer.Stop();
                    break;

                case "Breaktime Pause":
                    CurrentStatus = "Breaktime Running";
                    TimerStartButton = "⏸";
                    _timer.Start();
                    break;
            }
        }


        private void _resetTimer()
        {
            _timer.Stop();
            CurrentStatus = "Focus Stop";
            TimerStartButton = "▶";
            SessionTime = 25;
            _time = TimeSpan.FromMinutes(SessionTime);
            RemainTimeTextBlock = _time.ToString("c");
        }
        private void _breakTiimeMinus()
        {
            if (BreakTime > 0)
            {
                BreakTime--;
            }
        }
        private void _breakTimePlus()
        {
            if (BreakTime < 10)
            {
                BreakTime++;
            }
        }

        private void _sessionTimeMinus()
        {
            if (SessionTime > 1 && CurrentStatus == "Focus Stop")
            {
                SessionTime--;

                _time = TimeSpan.FromMinutes(SessionTime);
                RemainTimeTextBlock = _time.ToString("c");
            }
        }

        private void _sessionTimePlus()
        {
            if (CurrentStatus == "Focus Stop")
            {
                SessionTime++;

                _time = TimeSpan.FromMinutes(SessionTime);
                RemainTimeTextBlock = _time.ToString("c");
            }
        }

        #endregion

        #region Event Method


        #endregion
    }
}

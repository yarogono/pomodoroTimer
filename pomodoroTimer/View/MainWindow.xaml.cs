using System;
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
using pomodoroTimer.ViewModel;

namespace pomodoroTimer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new PomodoroTimerViewModel();

            // 앱 실행 시 스크린이 센터에서 시작 되도록
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void TimerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PomodoroTimerViewModel();
        }

        private void ToDoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ToDoViewModel();
        }
    }
}

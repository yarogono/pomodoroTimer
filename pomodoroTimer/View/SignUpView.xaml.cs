using pomodoroTimer.ViewModel;
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

namespace pomodoroTimer.View
{
    /// <summary>
    /// SignUpView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUpView : Window
    {
        public SignUpView()
        {
            InitializeComponent();

            // LoginView screen을 종료 시켜줌
            SignUpViewModel vm = new SignUpViewModel();
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void UserPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).UserPassword = ((PasswordBox)sender).SecurePassword; }
        }


        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).ConfirmPassword = ((PasswordBox)sender).SecurePassword; }
        }
    }
}

using MySql.Data.MySqlClient;
using pomodoroTimer.Model;
using pomodoroTimer.View;
using PomodoroTimer.Common.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pomodoroTimer.ViewModel
{
    public class SignUpViewModel : MainViewModel
    {


        #region Field



        #endregion

        #region Property

        // CloseAction 속성을 실행 시 SignUpView screen 종료
        public Action CloseAction { get; set; }

        public string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        private string _userId;


        public string UserPassword
        {
            get
            {
                return _userPassword;
            }
            set
            {
                _userPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }

        private string _userPassword;


        public string ConfirmPassword
        {
            get
            { 
                return _confirmPassword; 
            }
            set 
            { 
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        private string _confirmPassword;


        #endregion

        #region Command

        public ICommand CreateBtnCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CreateAccount();
                }, delegate () { return true; });
            }
        }


        #endregion

        #region Event Handler

        #endregion

        #region Constructor

        public SignUpViewModel()
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

        // 아이디 중복 체크 필요
        
        private void CreateAccount()
        {
            if (UserPassword != ConfirmPassword)
            {
                MessageBox.Show("Password와 Confirm Password가 같지 않습니다.");
                return;
            }

            bool SingUpNull = string.IsNullOrEmpty(UserId);
            SingUpNull = string.IsNullOrEmpty(UserPassword);
            SingUpNull = string.IsNullOrEmpty(ConfirmPassword);

            if (SingUpNull == true)
            {
                MessageBox.Show("전부 입력해주세요.");
                return;
            }


            using (MySqlConnection mySqlConnection = new MySqlConnection(SqlServerAuth.connection))
            {
                string loginSql = "INSERT INTO user_auth(User_Id, User_Password) VALUES(@User_Id, @User_Password)";

                try
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand(loginSql, mySqlConnection);
                    mySqlCmd.Parameters.AddWithValue("@User_Id", UserId);
                    mySqlCmd.Parameters.AddWithValue("@User_Password", UserPassword);
                    mySqlCmd.ExecuteReader();

                    if (mySqlCmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("계정이 생성되었습니다.");
                        CloseAction();

                        LoginView loginView = new LoginView();
                        loginView.ShowDialog();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            UserId = null;
            UserPassword = null;
            ConfirmPassword = null;
        }

        #endregion

        #region Event Method

        #endregion


    }
}

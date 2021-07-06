using PomodoroTimer.Common.Lib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using pomodoroTimer.Model;

namespace pomodoroTimer.ViewModel
{
    public class LoginViewModel : MainViewModel
    {

        #region Field



        #endregion

        #region Property

        // CloseAction 속성을 실행 시 LoginView screen 종료
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


        public int UserAuthIndex
        {
            get 
            { 
                return _userAuthIndex; 
            }
            set 
            {
                _userAuthIndex = value; 
                OnPropertyChanged(nameof(UserAuthIndex)); 
            }
        }

        private int _userAuthIndex;

        #endregion

        #region Command


        public ICommand LoginCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Login();
                }, delegate () { return true; });
            }
        }

        #endregion

        #region Event Handler

        #endregion

        #region Constructor

        public LoginViewModel()
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

        private void Login()
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(SqlServerAuth.connection))
            {
                string loginSql = "SELECT * FROM user_auth WHERE User_Id=@User_Id AND User_Password=@UserPassword";

                try
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand(loginSql, mySqlConnection);  
                    mySqlCmd.Parameters.AddWithValue("@User_Id", UserId);
                    mySqlCmd.Parameters.AddWithValue("@UserPassword", UserPassword);
                    MySqlDataReader reader = mySqlCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (UserPassword == reader[2].ToString())
                        {
                            UserAuthIndex = (int)reader[0];
                            MessageBox.Show("성공했닭");

                            CloseAction();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
             }
        }


        #endregion

        #region Event Method



        #endregion

    }
}

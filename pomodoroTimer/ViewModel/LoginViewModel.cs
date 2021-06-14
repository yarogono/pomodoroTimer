using DataManager.Common.Lib;
using MySql.Data.MySqlClient;
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
    class LoginViewModel : ViewModelBase
    {

        #region Field

        private string _userName;

        private string _password;

        #endregion

        #region Property


        public string Username
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(nameof(Username)); }
        }



        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }


        private string _test;

        public string Test
        {
            get { return _test; }
            set { _test = value; OnPropertyChanged(nameof(Test)); }
        }


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
            string connection = "Server = wow2020.iptime.org; Port = 10005; Database = datamanager; Uid = root; Pwd = xovudtjdeo";

            using (MySqlConnection mySqlConnection = new MySqlConnection(connection))
            {
                try//예외 처리
                {
                    mySqlConnection.Open();
                    string sql = "Select User_ID, Password from user_auth";

                    //ExecuteReader를 이용하여
                    //연결 모드로 데이타 가져오기
                    MySqlCommand cmd = new MySqlCommand(sql, mySqlConnection);
                    MySqlDataReader table = cmd.ExecuteReader();

                    while (table.Read())
                    {
                        Console.WriteLine("{0} {1}", table["User_ID"], table["Password"]); 
                    }
                    table.Close();
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

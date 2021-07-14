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
using pomodoroTimer.View;
using System.Security;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace pomodoroTimer.ViewModel
{
    public class LoginViewModel : MainViewModel
    {

        #region Field

        private string password = string.Empty;

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


        //public string UserPassword
        //{
        //    get 
        //    { 
        //        return _userPassword; 
        //    }
        //    set 
        //    {
        //        _userPassword = value; 
        //        OnPropertyChanged(nameof(UserPassword)); 
        //    }
        //}

        //private string _userPassword;

        public SecureString UserPassword { private get; set; }

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


        public ICommand SignUpBtnCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    SignUpBtnClick();
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

        //private byte[] GetPasswordHash(string username, string password, string salt)
        //{
        //    // get salted byte[] buffer, containing username, password and some (constant) salt
        //    byte[] buffer;
        //    using (MemoryStream stream = new MemoryStream())
        //    using (StreamWriter writer = new StreamWriter(stream))
        //    {
        //        writer.Write(salt);
        //        writer.Write(username);
        //        writer.Write(password);
        //        writer.Flush();

        //        buffer = stream.ToArray();
        //    }

        //    // create a hash
        //    SHA1 sha1 = SHA1.Create();
        //    return sha1.ComputeHash(buffer);
        //}




        #endregion

        #region Command Method

        private void Login()
        {
            password = "";

        //https://stackoverflow.com/questions/30618564/securestring-password-stored-in-database
        //    위 링크 참조해서 해결 중

           IntPtr valuePtr = IntPtr.Zero;

            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(UserPassword);

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                //the user id is the salt. 
                //So 2 users with same password have different hashes. 
                //For example if someone knows his own hash he can't see who has same password

                //string input = userInput + userId;
                Byte[] result = hash.ComputeHash(enc.GetBytes(Marshal.PtrToStringUni(valuePtr)));

                foreach (Byte b in result)
                    password += b.ToString("x2"); //You could also use other encodingslike BASE64 
            }

            using (MySqlConnection mySqlConnection = new MySqlConnection(SqlServerAuth.connection))
            {
                string loginSql = "SELECT * FROM user_auth WHERE User_Id=@User_Id";


                try
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand(loginSql, mySqlConnection);
                    mySqlCmd.Parameters.AddWithValue("@User_Id", UserId);
                    MySqlDataReader reader = mySqlCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (password == reader[2].ToString())
                        {
                            UserAuthIndex = (int)reader[0];
                            MessageBox.Show("로그인 성공");

                            CloseAction();
                        }
                        else
                        {
                            MessageBox.Show("로그인 실패");
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }


        private void SignUpBtnClick()
        {
            CloseAction();

            SignUpView signUpView = new SignUpView();
            signUpView.ShowDialog();
        }

        #endregion

        #region Event Method



        #endregion

    }
}

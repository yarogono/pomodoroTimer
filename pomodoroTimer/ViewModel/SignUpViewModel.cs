using MySql.Data.MySqlClient;
using pomodoroTimer.Model;
using pomodoroTimer.View;
using PomodoroTimer.Common.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pomodoroTimer.ViewModel
{
    public class SignUpViewModel : MainViewModel
    {


        #region Field

        private string _userPassword = string.Empty;

        private string _confirmPassword = string.Empty;

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

        public SecureString ConfirmPassword { private get; set; }

        //public string ConfirmPassword
        //{
        //    get
        //    { 
        //        return _confirmPassword; 
        //    }
        //    set 
        //    { 
        //        _confirmPassword = value;
        //        OnPropertyChanged(nameof(ConfirmPassword));
        //    }
        //}
        //private string _confirmPassword;


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

        private string passwordToHash(SecureString password)
        {
            string hashPasssword = string.Empty;

            IntPtr valuePtr = IntPtr.Zero;

            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(password);

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                Byte[] result = hash.ComputeHash(enc.GetBytes(Marshal.PtrToStringUni(valuePtr)));

                foreach (Byte b in result)
                    hashPasssword += b.ToString("x2"); //You could also use other encodingslike BASE64 

                return hashPasssword;
            }
        }

        #endregion

        #region Command Method

        // 아이디 중복 체크 필요
        
        private void CreateAccount()
        {
         
            bool SingUpNull = string.IsNullOrEmpty(UserId);

            if (SingUpNull == true)
            {
                MessageBox.Show("전부 입력해주세요.");
                return;
            }

            _userPassword = "";
            _confirmPassword = "";


            _userPassword = passwordToHash(UserPassword);
            _confirmPassword = passwordToHash(ConfirmPassword);


            if (_userPassword != _confirmPassword)
            {
                MessageBox.Show("Password와 Confirm Password가 같지 않습니다.");
                return;
            }

            //https://ellapresso.tistory.com/48 링크 참고해서 데이터베이스에 중복되면 넣지 않도록 하기
            
            using (MySqlConnection mySqlConnection = new MySqlConnection(SqlServerAuth.connection))
            {
                string IdDuplicateTest =
                                       $"INSERT INTO user_auth " +
                                       $"(User_Id, User_Password) " +
                                       $"SELECT @User_Id, @UserPassword " +
                                       $"FROM DUAL " +
                                       $"WHERE NOT EXISTS(SELECT User_Id FROM user_auth WHERE User_Id = '{UserId}')";

                try
                {
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommand = new MySqlCommand(IdDuplicateTest, mySqlConnection);
                    mySqlCommand.Parameters.AddWithValue("@User_Id", UserId);
                    mySqlCommand.Parameters.AddWithValue("@UserPassword", _userPassword);

                    if (mySqlCommand.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("중복된 아이디입니다");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("계정이 생성되었습니다.");
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

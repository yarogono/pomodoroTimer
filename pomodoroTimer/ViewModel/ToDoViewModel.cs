using MySql.Data.MySqlClient;
using PomodoroTimer.Common.Lib;
using pomodoroTimer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



// To-Do List에 해당하는 내용을 전부 입력 후 세이브 버튼을 누르면 저장 되는 기능으로 만들기
// 전부 작성 후 Ctrl + S를 눌러야 저장되는 기능과 비슷하게

namespace pomodoroTimer.ViewModel
{
    public class ToDoViewModel : MainViewModel
    {

        #region Field

        #endregion

        #region Property


        public string ToDoText
        {
            get 
            { 
                return _toDoText; 
            }
            set 
            { 
                _toDoText = value;
                OnPropertyChanged(nameof(ToDoText));
            }
        }

        private string _toDoText;



        public DataTable ListBoxItem
        {
            get 
            { 
                return _listBoxItem; 
            }
            set 
            {
                _listBoxItem = value;
                OnPropertyChanged(nameof(ListBoxItem));
            }
        }

        private DataTable _listBoxItem;



        public DataRowView SelectedListBoxItem
        {
            get
            {
                return _selectedListBoxItem;
            }
            set
            {
                _selectedListBoxItem = value;
                OnPropertyChanged(nameof(SelectedListBoxItem));
            }
        }

        private DataRowView _selectedListBoxItem;


        #endregion

        #region Command


        public ICommand TextAddCommand
        {
            get
            {   
                return new DelegateCommand(() =>
                {
                    _toDoAdd();
                }, delegate () { return true; });
            }
        }


        public ICommand DeleteItemCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _deleteItem();
                }, delegate () { return true; });
            }
        }



        public ICommand UpdateItemCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    _updateItem();
                }, delegate () { return true; });
            }
        }


        #endregion

        #region Event Handler

        #endregion

        #region Constructor

        public ToDoViewModel()
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

            _searchToDo();
        }

        #endregion

        #region User Method

        #endregion

        #region Command Method

        private void _toDoAdd()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            
            Console.WriteLine(loginViewModel.UserAuthIndex);


            if (ToDoText == null)
            {
                MessageBox.Show("할일을 적어주세요.");
                return;
            }

            using (MySqlConnection mySqlConnection = new MySqlConnection(SqlServerAuth.connection))
            {

                string insertQueryToDo = $"INSERT INTO to_do_list(To_Do) VALUES('{ToDoText}')";
                try//예외 처리
                {

                    // Maria DB 연결
                    mySqlConnection.Open();
                    MySqlCommand mySqlCommandToDo = new MySqlCommand(insertQueryToDo, mySqlConnection);

                    //만약에 내가처리한 쿼리가 DataBase에 인서트가 안되면 실패 메시지 출력
                    if (mySqlCommandToDo.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("ToDo 인서트 실패");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("실패");
                    MessageBox.Show(ex.ToString());
                }
            }

            _searchToDo();

            ToDoText = null;
        }


        private void _searchToDo()
        {
            DataSet dt = new DataSet();
            using (MySqlConnection mySqlConnection = new MySqlConnection(SqlServerAuth.connection))
            {
                string searchSql = "Select * From to_do_list";

                try//예외 처리
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(searchSql, mySqlConnection);
                    adapter.Fill(dt, "to_do_list");


                    ListBoxItem = dt.Tables["to_do_list"];

                }
                catch (Exception ex)
                {
                    MessageBox.Show("실패");
                    MessageBox.Show(ex.ToString());
                }
            }   
        }



        private void _deleteItem()
        {

            if (SelectedListBoxItem == null)
            {
                MessageBox.Show("삭제 할 리스트를 선택해주세요.");
                return;
            }

            using (MySqlConnection mySqlConnection = new MySqlConnection(SqlServerAuth.connection))
            {
                string deleteSql = $"DELETE FROM to_do_list WHERE To_Do_Index = " +
                    $"'{SelectedListBoxItem[0]}'";

                try
                {
                    MySqlCommand cmd = new MySqlCommand(deleteSql, mySqlConnection);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("실패");
                    MessageBox.Show(ex.ToString());
                }
            }
            _searchToDo();
        }



        private void _updateItem()
        {

            if (SelectedListBoxItem == null)
            {
                MessageBox.Show("테스트");
                return;
            }

            using (MySqlConnection mySqlConnection = new MySqlConnection(SqlServerAuth.connection))
            {
                string updateSql = $"UPDATE to_do_list SET To_Do = '{SelectedListBoxItem[1]}' " +
                    $"WHERE To_Do_Index = '{SelectedListBoxItem[0]}'";

                try//예외 처리
                {
                    MySqlCommand cmd = new MySqlCommand(updateSql, mySqlConnection);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("실패");
                    MessageBox.Show(ex.ToString());
                }
            }
            _searchToDo();
        }


        #endregion

        #region Event Method


        #endregion



    }
}

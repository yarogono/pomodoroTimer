using MySql.Data.MySqlClient;
using PomodoroTimer.Common.Lib;
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

            if (ToDoText == null)
            {
                MessageBox.Show("할일을 적어주세요.");
                return;
            }


            string connection = "Server = wow2020.iptime.org; Port = 10005; Database = datamanager; Uid = root; Pwd = xovudtjdeo";

            using (MySqlConnection mySqlConnection = new MySqlConnection(connection))
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
            string connection = "Server = wow2020.iptime.org; Port = 10005; Database = datamanager; Uid = root; Pwd = xovudtjdeo";
            DataSet dt = new DataSet();

            using (MySqlConnection mySqlConnection = new MySqlConnection(connection))
            {
                try//예외 처리
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand("Select * From to_do_list", mySqlConnection);
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

            string connection = "Server = wow2020.iptime.org; Port = 10005; Database = datamanager; Uid = root; Pwd = xovudtjdeo";

            using (MySqlConnection mySqlConnection = new MySqlConnection(connection))
            {
                try//예외 처리
                {

                    MySqlCommand cmd = new MySqlCommand($"DELETE FROM to_do_list WHERE To_Do = '{SelectedListBoxItem[1]}'", mySqlConnection);
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

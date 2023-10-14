using OOO_Sport_Products.Classes;
using OOO_Sport_Products.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOO_Sport_Products
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Подключение к БД
            Classes.Helper.DB = new Model.DBSportProducts(); 
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Text;
            StringBuilder sb = new StringBuilder();
            //Обработка пустоты
            if (login == "") {
                sb.Append("Вы не введи логин.\n");
            }
            if (password == "") {
                sb.Append("Вы не ввели пароль.\n");
            }
            if (sb.Length > 0) {
                MessageBox.Show(sb.ToString());
                return;
            }
            ////Работа с БД
            //List<Model.User> users = new List<Model.User>();
            ////Все записи БД
            //users = Classes.Helper.DB.Users.ToList();
            ////Получить первую запись таблицы
            //Model.User user = users.Where(u => u.UserLogin == login && u.UserPassword == password).FirstOrDefault();
            Classes.Helper.User = Classes.Helper.DB.Users.ToList().Where(u => u.UserLogin == login && u.UserPassword == password).FirstOrDefault();
            string userName = Helper.User.UserFullName;
            int userRoleId = Helper.User.UserRole;
            string userRoleName = Helper.User.Role.RoleName;
            if (Helper.User != null)
            {
                MessageBox.Show(userName + "\n" + userRoleId + "\n" + userRoleName);
            }
            else {
                MessageBox.Show("Вы не зарегистрированны в системе.");
            }
            //Доступ по навигационному свуйству в полю связвнной таблицы
            //string userRoleName = user.Role.RoleName;
            //MessageBox.Show(userName + " " + userRoleId + " " + userRoleName);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}

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

namespace ServiceRequestApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();

            nameText.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (logText.Text == "" || passText.Password == "")
            {
                MessageBox.Show("Введите логин или пароль",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (App.db.User.FirstOrDefault(p => p.Login == logText.Text) != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                User user = new User();
                user.Name = nameText.Text;
                user.Surname = fioText.Text;
                user.Middlename = surText.Text;
                user.Telephone = telText.Text;
                user.Email = emailText.Text;
                user.Login = logText.Text;

                var pswrd = md5.hashPassword(passText.Password);

                user.Password = pswrd;
                user.RoleId = 2;
                
                App.db.User.Add(user);
                App.db.SaveChanges();

                MessageBox.Show("Пользователь успешно добавлен",
                    "Ура", MessageBoxButton.OK, MessageBoxImage.Information);

                FrameObj.frameMain.Navigate(new ViewUsersPage());
            }
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            nameText.Text = "";
            fioText.Text = "";
            surText.Text = "";
            telText.Text = "";
            emailText.Text = "";
            logText.Text = "";
            passText.Password = "";
        }

        private void nameText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                fioText.Focus();
            }
        }

        private void fioText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                surText.Focus();
            }
        }

        private void surText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                telText.Focus();
            }
        }

        private void telText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                emailText.Focus();
            }
        }

        private void emailText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logText.Focus();
            }
        }

        private void logText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                passText.Focus();
            }
        }

        private void passText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnAdd_Click(sender, e);
            }
        }
    }
}

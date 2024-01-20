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
    /// Логика взаимодействия для AutorizationPage.xaml
    /// </summary>
    public partial class AutorizationPage : Page
    {
        public AutorizationPage()
        {
            InitializeComponent();

            logText.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var pswrd = md5.hashPassword(passText.Password);
            App.currentUser = App.db.User.FirstOrDefault(i => i.Login == logText.Text && i.Password == pswrd);

            if (logText.Text == "" || passText.Password == "")
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (App.currentUser != null)
            {
                FrameObj.frameMain.Navigate(new HomePage());
                logText.Text = "";
                passText.Password = "";
                //MessageBox.Show("Успешная авторизация!", "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль! Повторите попытку!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            logText.Text = "";
            passText.Password = "";
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
                btnLogin_Click(sender, e);
            }
        }
    }
}

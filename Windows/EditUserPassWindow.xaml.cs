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
using System.Windows.Shapes;

namespace ServiceRequestApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditUserPassWindow.xaml
    /// </summary>
    public partial class EditUserPassWindow : Window
    {
        User userSelect;

        public EditUserPassWindow()
        {
            InitializeComponent();
        }

        public EditUserPassWindow(int index)
        {
            InitializeComponent();

            User use = App.db.User.Where(p => p.IdUser == index).FirstOrDefault();

            userSelect = use;
        }

        private void passsave_Click(object sender, RoutedEventArgs e)
        {
            var newpswrd = md5.hashPassword(newpass.Password);

            if (newpass.Password == "")
            {
                MessageBox.Show("Логин и пароль должены быть обязательно, иначе как пользователь будет входить?",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                userSelect.Password = newpswrd;
                App.db.SaveChanges();
                MessageBox.Show("Пароль успешно изменен!", "У тебя получилось!", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void newpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                passsave_Click(sender, e);
            }
        }
    }
}

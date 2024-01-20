using ServiceRequestApp.Windows;
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
    /// Логика взаимодействия для EditHomePage.xaml
    /// </summary>
    public partial class EditHomePage : Page
    {
        public EditHomePage()
        {
            InitializeComponent();

            famtext.Text = App.currentUser.Surname;
            nametext.Text = App.currentUser.Name;
            surnametext.Text = App.currentUser.Middlename;
            teltext.Text = App.currentUser.Telephone;
            mailtext.Text = App.currentUser.Email;
            logintext.Text = App.currentUser.Login;
        }

        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            if (logintext.Text != null)
            {
                App.currentUser.Surname = famtext.Text;
                App.currentUser.Name = nametext.Text;
                App.currentUser.Middlename = surnametext.Text;
                App.currentUser.Telephone = teltext.Text;
                App.currentUser.Email = mailtext.Text;
                App.currentUser.Login = logintext.Text;
                App.db.SaveChanges();
                MessageBox.Show("Данные успешно обновлены!", "У тебя получилось!",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                FrameObj.frameMain.Navigate(new HomePage());
            }
            else
            {
                MessageBox.Show("Логин должен быть обязательно, иначе как ты входить будешь?",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btneditpass_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditPassWindow();
            window.ShowDialog();
        }
    }
}

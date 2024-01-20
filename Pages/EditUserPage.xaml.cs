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
    /// Логика взаимодействия для EditUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        User userSelect;

        public EditUserPage()
        {
            InitializeComponent();
        }

        public EditUserPage(int index)
        {
            InitializeComponent();

            User use = App.db.User.Where(p => p.IdUser == index).FirstOrDefault();

            userSelect = use;
            nameText.Text = use.Name;
            fioText.Text = use.Surname; ;
            surText.Text = use.Middlename;
            telText.Text = use.Telephone;
            emailText.Text = use.Email;
            logText.Text = use.Login;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (logText.Text == "")
            {
                MessageBox.Show("Логин и пароль должены быть обязательно, иначе как пользователь будет входить?",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                userSelect.Name = nameText.Text;
                userSelect.Surname = fioText.Text;
                userSelect.Middlename = surText.Text;
                userSelect.Telephone = telText.Text;
                userSelect.Email = emailText.Text;
                userSelect.Login = logText.Text;

                App.db.SaveChanges();

                MessageBox.Show("Данные успешно обновлены!",
                    "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);

                FrameObj.frameMain.Navigate(new ViewUsersPage());
            }
        }

        private void btnEditUserPass_Click(object sender, RoutedEventArgs e)
        {
            int result = userSelect.IdUser;
            var window = new EditUserPassWindow(result);
            window.ShowDialog();
        }
    }
}

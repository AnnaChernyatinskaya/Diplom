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
    /// Логика взаимодействия для ViewUsersPage.xaml
    /// </summary>
    public partial class ViewUsersPage : Page
    {
        public ViewUsersPage()
        {
            InitializeComponent();

            dataGridUsers.ItemsSource = App.db.User.Where(u => u.IdUser > 1).OrderBy(p => p.IdUser).ToList();
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите пользователя, которого хотите отредактировать!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var row = dataGridUsers.SelectedItem as User;
                int result = row.IdUser;
                FrameObj.frameMain.Navigate(new EditUserPage(result));
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.frameMain.Navigate(new AddUserPage());
        }
    }
}

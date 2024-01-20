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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();

            famtext.Text = App.currentUser.Surname;
            nametext.Text = App.currentUser.Name;
            surnametext.Text = App.currentUser.Middlename;
            teltext.Text = App.currentUser.Telephone;
            mailtext.Text = App.currentUser.Email;
            logintext.Text = App.currentUser.Login;
        }

        private void btnedit_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.frameMain.Navigate(new EditHomePage());
        }
    }
}

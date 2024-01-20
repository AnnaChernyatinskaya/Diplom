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
    /// Логика взаимодействия для AddRequestPage.xaml
    /// </summary>
    public partial class AddRequestPage : Page
    {
        DateTime date = DateTime.Now;

        public AddRequestPage()
        {
            InitializeComponent();

            foreach (string row in App.db.Tema.OrderBy(p => p.IdTema).Select(p => p.NameTema))
                temaText.Items.Add(row);
            foreach (string row in App.db.Importance.OrderBy(p => p.IdImportance).Select(p => p.NameImportance))
                importanceText.Items.Add(row);

            userNameText.Text += " " + App.currentUser.Surname + " " + App.currentUser.Name + " " + App.currentUser.Middlename;

            string now = date.Date.ToShortDateString();
            dateText.Text = now;
        }

        private void btnAddReq_Click(object sender, RoutedEventArgs e)
        {
            if (temaText.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тему заявки!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (importanceText.SelectedIndex == -1)
            {
                MessageBox.Show("Укажите важность заявки!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (dateFinish.Text == "")
            {
                MessageBox.Show("Укажите до какого числа планируется закрыть заявку!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (discripText.Text == "")
            {
                MessageBox.Show("Введите описание",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Request req = new Request();
                req.UserId = App.currentUser.IdUser;
                req.TemaId = temaText.SelectedIndex + 1;
                req.ImportanceId = importanceText.SelectedIndex + 1;
                req.StatusId = 1;
                req.DateStart = date;
                req.DateFinish = (DateTime)dateFinish.SelectedDate;
                req.Description = discripText.Text;
                App.db.Request.Add(req);
                App.db.SaveChanges();

                MessageBox.Show("Заявка успешно добавлена",
                    "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);

                FrameObj.frameMain.Navigate(new RequestPage());
            }
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            temaText.Text = "";
            importanceText.Text = "";
            dateFinish.Text = "";
            discripText.Text = "";

            dateFinish.DisplayDate = date;
        }
    }
}

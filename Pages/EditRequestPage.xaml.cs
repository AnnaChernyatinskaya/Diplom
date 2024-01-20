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
    /// Логика взаимодействия для EditRequestPage.xaml
    /// </summary>
    public partial class EditRequestPage : Page
    {
        Request updateRequest;

        public EditRequestPage()
        {
            InitializeComponent();
        }

        public EditRequestPage(int index)
        {
            InitializeComponent();

            foreach (string row in App.db.Tema.OrderBy(p => p.IdTema).Select(p => p.NameTema))
                temaText.Items.Add(row);
            foreach (string row in App.db.Importance.OrderBy(p => p.IdImportance).Select(p => p.NameImportance))
                importanceText.Items.Add(row);

            Request req = App.db.Request.Where(p => p.IdRequest == index).FirstOrDefault();

            this.Title += req.IdRequest;

            updateRequest = req;
            App.db.Entry(req).Reference(t => t.User).Load();
            App.db.Entry(req).Reference(t => t.Tema).Load();
            App.db.Entry(req).Reference(t => t.Importance).Load();
            App.db.Entry(req).Reference(r => r.Status).Load();
            userNameText.Text += req.User.Surname + " " + req.User.Name + " " + req.User.Middlename;
            statusText.Text += req.Status.NameStatus;
            temaText.SelectedIndex = req.TemaId - 1;
            //temaText.Text = req.Tema.NameTema;
            importanceText.SelectedIndex = req.ImportanceId - 1;
            //importanceText.Text = req.Importance.NameImportance;
            dateText.Text = req.DateStart.Date.ToShortDateString();
            dateFinish.Text = req.DateFinish.Date.ToShortDateString();
            discripText.Text = req.Description;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (temaText.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тему заявки!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (importanceText.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите важность заявки!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                updateRequest.TemaId = temaText.SelectedIndex + 1;
                updateRequest.ImportanceId = importanceText.SelectedIndex + 1;

                App.db.SaveChanges();

                MessageBox.Show("Заявка успешно отредактирована!",
                    "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);

                int result = updateRequest.IdRequest;
                FrameObj.frameMain.Navigate(new ViewRequestPage(result));
            }
        }
    }
}

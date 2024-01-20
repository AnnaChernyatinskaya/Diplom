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
    /// Логика взаимодействия для CloseRequestWindow.xaml
    /// </summary>
    public partial class CloseRequestWindow : Window
    {
        Request updateRequest;

        DateTime date = DateTime.Now;

        public CloseRequestWindow()
        {
            InitializeComponent();
        }

        public CloseRequestWindow(int index)
        {
            InitializeComponent();
            responseText.Focus();

            Request req = App.db.Request.Where(p => p.IdRequest == index).FirstOrDefault();
            updateRequest = req;

            idText.Text += req.IdRequest;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (responseText.Text != null)
            {
                updateRequest.StatusId = 3;
                updateRequest.Response = responseText.Text;

                if (date <= updateRequest.DateFinish)
                {
                    updateRequest.DoneDuring = true;
                }
                else
                {
                    updateRequest.DoneDuring = false;
                }

                App.db.SaveChanges();

                MessageBox.Show("Статус заявки: Закрыта!", "Ура!",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

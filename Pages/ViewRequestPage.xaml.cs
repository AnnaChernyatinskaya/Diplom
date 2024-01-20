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
    /// Логика взаимодействия для ViewRequestPage.xaml
    /// </summary>
    public partial class ViewRequestPage : Page
    {
        Request updateRequest;
        //DateTime date = DateTime.Now;

        public ViewRequestPage()
        {
            InitializeComponent();
        }

        public ViewRequestPage(int index)
        {
            InitializeComponent();

            if (App.currentUser.RoleId == 2)
            {
                btnStartReq.Visibility = Visibility.Collapsed;
                btnFinishReq.Visibility = Visibility.Collapsed;
                btnEditReq.Visibility = Visibility.Collapsed;
            }


            Request req = App.db.Request.Where(p => p.IdRequest == index).FirstOrDefault();

            this.Title += req.IdRequest;

            if (req.StatusId == 2)
            {
                btnStartReq.IsEnabled = false;
                //btnStartReq.Background = Brushes.Gray;
            }

            updateRequest = req;
            App.db.Entry(req).Reference(t => t.User).Load();
            App.db.Entry(req).Reference(t => t.Tema).Load();
            App.db.Entry(req).Reference(t => t.Importance).Load();
            App.db.Entry(req).Reference(r => r.Status).Load();
            userNameText.Text += req.User.Surname + " " + req.User.Name + " " + req.User.Middlename;
            statusText.Text += req.Status.NameStatus;
            temaText.Text = req.Tema.NameTema;
            importanceText.Text = req.Importance.NameImportance;
            dateText.Text = req.DateStart.Date.ToShortDateString();
            dateFinish.Text = req.DateFinish.Date.ToShortDateString();
            discripText.Text = req.Description;
        }

        private void btnStartReq_Click(object sender, RoutedEventArgs e)
        {
            updateRequest.StatusId = 2;
            App.db.SaveChanges();

            MessageBox.Show("Статус заявки изменен!", "Ура!",
                    MessageBoxButton.OK, MessageBoxImage.Information);

            FrameObj.frameMain.Navigate(new RequestPage());
        }

        private void btnFinishReq_Click(object sender, RoutedEventArgs e)
        {
            int result = updateRequest.IdRequest;

            var window = new CloseRequestWindow(result);
            window.ShowDialog();

            if (updateRequest.StatusId == 3)
            {
                FrameObj.frameMain.Navigate(new OtchetPage(result));
            }
            else
            {
                FrameObj.frameMain.Navigate(new ViewRequestPage(result));
            }
        }

        private void btnEditReq_Click(object sender, RoutedEventArgs e)
        {
            int result = updateRequest.IdRequest;
            FrameObj.frameMain.Navigate(new EditRequestPage(result));
        }
    }
}

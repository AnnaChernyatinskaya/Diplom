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

using word = Microsoft.Office.Interop.Word;

namespace ServiceRequestApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для OtchetPage.xaml
    /// </summary>
    public partial class OtchetPage : Page
    {
        Request updateRequest;

        public OtchetPage()
        {
            InitializeComponent();
        }

        public OtchetPage(int index)
        {
            InitializeComponent();

            if (App.currentUser.RoleId == 1)
            {
                btnOtchet.Visibility = Visibility.Collapsed;
            }

            Request req = App.db.Request.Where(p => p.IdRequest == index).FirstOrDefault();

            this.Title += req.IdRequest;

            updateRequest = req;
            App.db.Entry(req).Reference(t => t.User).Load();
            App.db.Entry(req).Reference(t => t.Tema).Load();
            App.db.Entry(req).Reference(t => t.Importance).Load();
            App.db.Entry(req).Reference(r => r.Status).Load();

            headerText.Text += req.IdRequest;
            user.Text += req.User.Surname + " " + req.User.Name + " " + req.User.Middlename;
            tema.Text += req.Tema.NameTema;
            import.Text += req.Importance.NameImportance;
            dataS.Text += req.DateStart.Date.ToShortDateString();
            dataF.Text += req.DateFinish.Date.ToShortDateString();
            discrip.Text += req.Description;
            response.Text += req.Response;

            if (req.DoneDuring == true)
            {
                admin.Text += "В срок";
            }
            else
            {
                admin.Text += "С опозданием!";
            }
        }

        private void btnOtchet_Click(object sender, RoutedEventArgs e)
        {
            var application = new word.Application();
            word.Document document = application.Documents.Add();
            word.Paragraph para = document.Paragraphs.Add();

            //para.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
            para.Range.Bold = 14;

            para.Range.Text = headerText.Text;
            para.Range.InsertParagraphAfter();

            para.Range.Font.Name = "Courier New";

            para.Range.Text = user.Text;
            para.Range.InsertParagraphAfter();
            para.Range.Text = tema.Text;
            para.Range.InsertParagraphAfter();
            para.Range.Text = import.Text;
            para.Range.InsertParagraphAfter();
            para.Range.InsertParagraphAfter();
            para.Range.Text = dataS.Text;
            para.Range.InsertParagraphAfter();
            para.Range.Text = dataF.Text;
            para.Range.InsertParagraphAfter();
            para.Range.Text = discrip.Text;
            para.Range.InsertParagraphAfter();
            para.Range.Text = response.Text;
            para.Range.InsertParagraphAfter();
            para.Range.Text = admin.Text;

            application.Visible = true;

            document.SaveAs2();

            MessageBox.Show("Отчет успешно сохранен!", "Ура!",
                    MessageBoxButton.OK, MessageBoxImage.Information);

            FrameObj.frameMain.Navigate(new RequestPage());
        }
    }
}

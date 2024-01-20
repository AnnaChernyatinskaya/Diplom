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
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        public RequestPage()
        {
            InitializeComponent();

            comboBoxSortByTema.SelectedIndex = 0; // При инициализации поля для поиска и сортировки очищаются 
            comboBoxSortByImport.SelectedIndex = 0;
            textBoxSearch.Text = "";
            finishReq.IsChecked = false;

            send.Text += App.db.Request.Count(p => p.StatusId == 1);
            work.Text += App.db.Request.Count(p => p.StatusId == 2);
            close.Text += App.db.Request.Count(p => p.StatusId == 3);

            if (App.currentUser.RoleId == 1) // Проверка находится ли в системе администратор
            {
                this.Title = "Просмотр заявок";
                btnAddReq.Visibility = Visibility.Collapsed;
                dataGridRequests.ItemsSource = App.db.Request.OrderBy(p => p.StatusId).ToList(); // Вывод списка заявок
            }
            else if (App.currentUser.RoleId == 2) // Проверка находится ли в системе пользователь
            {
                this.Title = "Мои заявки";
                btnAddReq.Visibility = Visibility.Visible;

                finishReq.Visibility = Visibility.Collapsed;
                send.Visibility = Visibility.Collapsed;
                work.Visibility = Visibility.Collapsed;
                close.Visibility = Visibility.Collapsed;
                dataGridRequests.ItemsSource = App.db.Request.Where(p => p.UserId == App.currentUser.IdUser).OrderBy(p => p.StatusId).ToList(); // Вывод списка заявок с условием

                id.Visibility = Visibility.Collapsed;
            }

            UpdateRequest(); // Обновление стрнаницы заявок
        }

        private void btnAddReq_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.frameMain.Navigate(new AddRequestPage()); // Переход на страницу добавления заявки
        }

        private void btnViewReq_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridRequests.SelectedIndex != -1) // Проверка выбрана ли хоть одна заявка из списка
            {
                var row = dataGridRequests.SelectedItem as Request; // Переменная для хранения выбранной заявки
                int result = row.IdRequest;
                if (row.StatusId == 3) // Проверка является ли заявка завершенной
                {
                    FrameObj.frameMain.Navigate(new OtchetPage(result)); // Переход на страницу просмотра отчета по заявке
                }
                else
                {
                    if (App.currentUser.IdUser == 1)
                    {
                        FrameObj.frameMain.Navigate(new ViewRequestPage(result)); // Переход на страницу предпросмотра заявки
                    }
                    else
                    {
                        MessageBox.Show("Пользователь может посмотреть лишь завершенные заявки!", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error); //Вывод ошибки пользователю
                    }
                }
            }
            // Если ни одна заявка не выбрана из списка:
            else
            {
                MessageBox.Show("Выберите заявку, которую хотите просмотреть", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error); //Вывод ошибки пользователю
            }
        }

        private void comboBoxSortByImport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxSortByImport.SelectedIndex != 0) // Если выбрана тема
            {
                comboBoxSortByTema.SelectedIndex = 0; // Поле сортировки по категории важности очищается
                textBoxSearch.Text = "";
                finishReq.IsChecked = false;
            }

            UpdateRequest();
        }

        private void comboBoxSortByTema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxSortByTema.SelectedIndex != 0)
            {
                comboBoxSortByImport.SelectedIndex = 0;
                textBoxSearch.Text = "";
                finishReq.IsChecked = false;
            }

            UpdateRequest();
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            finishReq.IsChecked = false;
            comboBoxSortByImport.SelectedIndex = 0;
            comboBoxSortByTema.SelectedIndex = 0;

            if (textBoxSearch.Text == "")
            {
                dataGridRequests.ItemsSource = App.db.Request.OrderBy(p => p.StatusId).ToList();
            }

            UpdateRequest();
        }

        private void resetToDefault_Click(object sender, RoutedEventArgs e)
        {
            FrameObj.frameMain.Navigate(new RequestPage());
        }

        private void UpdateRequest()
        {
            if (App.currentUser.RoleId == 1)
            {
                if (comboBoxSortByTema.SelectedIndex == 1)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.TemaId == 1).ToList();
                else if (comboBoxSortByTema.SelectedIndex == 2)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.TemaId == 2).ToList();
                else if (comboBoxSortByTema.SelectedIndex == 3)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.TemaId == 3).ToList();
                else if (comboBoxSortByTema.SelectedIndex == 4)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.TemaId == 4).ToList();

                if (comboBoxSortByImport.SelectedIndex == 1)
                    dataGridRequests.ItemsSource = App.db.Request.OrderBy(p => p.ImportanceId).ToList();
                else if (comboBoxSortByImport.SelectedIndex == 2)
                    dataGridRequests.ItemsSource = App.db.Request.OrderByDescending(p => p.ImportanceId).ToList();

                if (textBoxSearch.Text != "")
                {
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.IdRequest.ToString() == textBoxSearch.Text).ToList();
                }
            }
            else
            {
                if (comboBoxSortByTema.SelectedIndex == 1)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.UserId == App.currentUser.IdUser && p.TemaId == 1).ToList();
                else if (comboBoxSortByTema.SelectedIndex == 2)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.UserId == App.currentUser.IdUser && p.TemaId == 2).ToList();
                else if (comboBoxSortByTema.SelectedIndex == 3)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.UserId == App.currentUser.IdUser && p.TemaId == 3).ToList();
                else if (comboBoxSortByTema.SelectedIndex == 4)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.UserId == App.currentUser.IdUser && p.TemaId == 4).ToList();

                if (comboBoxSortByImport.SelectedIndex == 1)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.UserId == App.currentUser.IdUser).OrderBy(p => p.ImportanceId).ToList();
                else if (comboBoxSortByImport.SelectedIndex == 2)
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.UserId == App.currentUser.IdUser).OrderByDescending(p => p.ImportanceId).ToList();
                
                if (textBoxSearch.Text != "")
                {
                    dataGridRequests.ItemsSource = App.db.Request.Where(p => p.UserId == App.currentUser.IdUser &&
                    p.IdRequest.ToString() == textBoxSearch.Text).ToList();
                }
            }
        }

        private void finishReq_Checked(object sender, RoutedEventArgs e)
        {
            comboBoxSortByImport.SelectedIndex = 0;
            comboBoxSortByTema.SelectedIndex = 0;
            textBoxSearch.Text = "";

            dataGridRequests.ItemsSource = App.db.Request.Where(p => p.StatusId < 3).OrderBy(p => p.DateFinish).ToList();
        }

        private void finishReq_Unchecked(object sender, RoutedEventArgs e)
        {
            dataGridRequests.ItemsSource = App.db.Request.OrderBy(p => p.StatusId).ToList(); 
        }
    }
}

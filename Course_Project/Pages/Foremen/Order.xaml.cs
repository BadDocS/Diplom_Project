using Course_Project.Info;
using Course_Project.Pages.Directories;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;


namespace Course_Project.Pages.Foremen
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public Order()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            GridList.ItemsSource = OdbConnectHelper.entObj.Orders.Where(o => o.Work_cod == Authorization.user.id_area && o.Month == DateTime.Today.Month && o.Year == DateTime.Today.Year).ToList();
         
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Materials_ref mat = new Materials_ref();
            mat.ShowDialog();
            Update();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (GridList.SelectedItem != null)
            {
                OdbConnectHelper.entObj.Orders.Remove(GridList.SelectedItem as Orders);
                OdbConnectHelper.entObj.SaveChanges();
                MessageBox.Show("Удаление завершено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            GridList.ItemsSource = OdbConnectHelper.entObj.Orders.Where(o => o.Work_cod == Authorization.user.id_area).ToList();
        }

        private void GridList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridList.SelectedItem != null)
                btnDelete.IsEnabled = true;
        }

        private void GridList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Materials_ref mat = new Materials_ref();
            mat.ShowDialog();
            Update();
        }

        private void btnTemp_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new Foremen.Templates());
            Update();

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }
    }
}

using Course_Project.Info;
using Course_Project.Pages.Directories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Course_Project.Pages.Foremen
{
    /// <summary>
    /// Логика взаимодействия для Templates.xaml
    /// </summary>
    public partial class Templates : Page
    {
        public Templates()
        {
            InitializeComponent();
            LbTemp.ItemsSource = OdbConnectHelper.entObj.Templates.Where(t => t.id_area == Authorization.user.id_area).ToList();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (LbTemp.SelectedItem != null)
            {
                Adding_material.template = (LbTemp.SelectedItem as Info.Templates);
                Adding_material.temp = true;
                new Materials_ref().ShowDialog();
                int si = (LbTemp.SelectedItem as Info.Templates).id;
                GridList.ItemsSource = OdbConnectHelper.entObj.TOrders.Where(t => t.Temp_num == si).ToList();

            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Create_template newTemp = new Create_template();
            if (newTemp.ShowDialog() == true)
            {
                LbTemp.ItemsSource = OdbConnectHelper.entObj.Templates.Where
                    (t => t.id_area == Authorization.user.id_area).ToList();
            }
        }

        private void btnDel_r_Click(object sender, RoutedEventArgs e)
        {
            if (GridList.SelectedItem != null)
            {
                OdbConnectHelper.entObj.TOrders.Remove(GridList.SelectedItem as TOrders);
                OdbConnectHelper.entObj.SaveChanges();
                int si = (LbTemp.SelectedItem as Info.Templates).id;
                GridList.ItemsSource = OdbConnectHelper.entObj.TOrders.Where
                    (t => t.Temp_num == si).ToList();
                MessageBox.Show("Удаление завершено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnDel_t_Click(object sender, RoutedEventArgs e)
        {
            if (LbTemp.SelectedItem != null)
            {
                Info.Templates hur = LbTemp.SelectedItem as Info.Templates;
                foreach (TOrders tor in OdbConnectHelper.entObj.TOrders.Where(t => t.Temp_num == hur.id))
                {
                    OdbConnectHelper.entObj.TOrders.Remove(tor);

                }
                OdbConnectHelper.entObj.SaveChanges();
                OdbConnectHelper.entObj.Templates.Remove(hur);
                OdbConnectHelper.entObj.SaveChanges();
                int si = (LbTemp.SelectedItem as Info.Templates).id;
                LbTemp.ItemsSource = OdbConnectHelper.entObj.Templates.Where
                    (t => t.id_area == Authorization.user.id_area).ToList();
                GridList.ItemsSource = OdbConnectHelper.entObj.Templates.Where
                    (t => t.id == si).ToList();
            }
        }

        private void LbTemp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbTemp.SelectedItem == null)
                return;
            btnAccept.IsEnabled = true;
            btnAdd.IsEnabled = true;
            int si = (LbTemp.SelectedItem as Info.Templates).id;
            GridList.ItemsSource = OdbConnectHelper.entObj.TOrders.Where(t => t.Temp_num == si).ToList();

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (LbTemp.SelectedItem == null)
                return;
            List<Orders> or = new List<Orders>();
            int si = (LbTemp.SelectedItem as Info.Templates).id;
            foreach (var torders in OdbConnectHelper.entObj.TOrders.Where(t => t.Temp_num == si))
            {
                int a = (int)Authorization.user.id_area;
                if (OdbConnectHelper.entObj.Orders.Where(o => o.Material_num == torders.Material_num && o.Work_cod == a).Count() > 0)
                {
                    OdbConnectHelper.entObj.Orders.Where(o => o.Material_num == torders.Material_num && o.Work_cod == a).First().Quantity += torders.Quantity;
                }
                else
                {
                    or.Add(
                    new Orders
                    {
                        Material_num = torders.Material_num,
                        Quantity = torders.Quantity,
                        Month = DateTime.Today.Month,
                        Year = DateTime.Today.Year,
                        Work_cod = Authorization.user.id_area
                    });
                }
            }if (or.Count() > 0)
            OdbConnectHelper.entObj.Orders.AddRange(or);
            OdbConnectHelper.entObj.SaveChanges();
            FrameApp.frmObj.GoBack();
        }

        private void GridList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Materials_ref mat = new Materials_ref();
            mat.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new Authorization());
        }
    }
}

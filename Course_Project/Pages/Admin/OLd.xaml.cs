using Course_Project.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Course_Project.Pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для OLd.xaml
    /// </summary>
    public partial class OLd : Page
    {

        public OLd()
        {
            InitializeComponent();
            //LbArea.DisplayMemberPath = "Title";
            List <int> Year = new List<int>();
            List <Month> months = new List<Month>();
            int i = DateTime.Today.Month - 1;
            while (i != DateTime.Today.Month)
            {
                if (i == 0)
                    i = 12;
                months.Add(OdbConnectHelper.entObj.Month.First(m => m.id == i));
                i--;

            }
            LbArea.ItemsSource = months;

        }

        private void LbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int si = (LbArea.SelectedItem as Month).id;
            GridList.ItemsSource = OdbConnectHelper.entObj.Order_view.Where(t => t.Month == si).ToList();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new Authorization());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }
    }
}

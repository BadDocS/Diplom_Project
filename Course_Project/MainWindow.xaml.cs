using Course_Project.Info;
using System;
using System.Linq;
using System.Windows;

namespace Course_Project
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            OdbConnectHelper.entObj = new Info.ТрансмашEntities1();
            FrameApp.frmObj = FrmMain;
            FrameApp.frmObj.Navigate(new Pages.Authorization());
            foreach (Orders od in OdbConnectHelper.entObj.Orders.Where(o => o.Month == DateTime.Today.Month && o.Year < DateTime.Today.Year))
            {
                OdbConnectHelper.entObj.Orders.Remove(od);
                
            }
            OdbConnectHelper.entObj.SaveChanges();
        }
    }
}

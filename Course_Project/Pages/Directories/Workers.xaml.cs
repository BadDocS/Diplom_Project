using Course_Project.Info;
using System.Linq;
using System.Windows;


namespace Course_Project.Pages.Directories
{
    /// <summary>
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Window
    {
        public Workers()
        {
            InitializeComponent();

            GridList.ItemsSource = OdbConnectHelper.entObj.Workers.Where(o => o.Area_work == Workshops.work_Ar.Workshop_code).ToList();
        }
    }
}

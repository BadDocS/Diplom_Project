using Course_Project.Info;
using System.Windows;

namespace Course_Project.Pages.Directories
{
    /// <summary>
    /// Логика взаимодействия для Workshops.xaml
    /// </summary>
    public partial class Workshops : Window
    {
        public static Work_Areas work_Ar { get; set; }
        public Workshops()
        {
            InitializeComponent();
            TbBoss.Text = work_Ar.Supervisor;
            TbTitle.Text = work_Ar.Title;
            TbType.Text = work_Ar.Type_work;
        }

        private void BtnWorkers_Click(object sender, RoutedEventArgs e)
        {
            Workers Ws = new Workers();
            Ws.ShowDialog();
        }
    }
}

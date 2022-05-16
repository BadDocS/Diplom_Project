using Course_Project.Info;
using System.Windows;


namespace Course_Project.Pages.Foremen
{
    /// <summary>
    /// Логика взаимодействия для Create_template.xaml
    /// </summary>
    public partial class Create_template : Window
    {
        public Create_template()
        {
            InitializeComponent();
            tbTitle.MaxLength = 20;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (tbTitle.Text.Length > 0)
            {
                Info.Templates temp = new Info.Templates
                {
                    Title = tbTitle.Text,
                    Work_Areas = Authorization.user.Work_Areas
                };
                OdbConnectHelper.entObj.Templates.Add(temp);
                OdbConnectHelper.entObj.SaveChanges();
                this.DialogResult = true;
            }
        }
    }
}

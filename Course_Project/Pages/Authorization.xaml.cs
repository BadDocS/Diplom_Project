using Course_Project.Info;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Course_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public static User user;
        public Authorization()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbLog.Text.Length < 1 || tbLog.Text == "Введите логин")
            {
                MessageBox.Show("Не введен логин", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbLog.Focus();
                return;
            }
            if (Pb1.Password.Length < 1)
            {
                MessageBox.Show("Не введен пароль", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                Pb1.Focus();
                return;
            }
            if (OdbConnectHelper.entObj.User.Count(x => x.Login == tbLog.Text && x.Password == Pb1.Password) == 1)
            {
                user = OdbConnectHelper.entObj.User.Where(x => x.Login == tbLog.Text && x.Password == Pb1.Password).FirstOrDefault();
                MessageBox.Show($"Здравствуйте {user.Login}!",
               "Уведомление",
               MessageBoxButton.OK,
               MessageBoxImage.Information);
                if (user.id_area != null)
                   FrameApp.frmObj.Navigate(new Foremen.Order());
                else FrameApp.frmObj.Navigate(new Purchases());
            }
            else
            {

                MessageBox.Show("Неверный логин или пароль", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

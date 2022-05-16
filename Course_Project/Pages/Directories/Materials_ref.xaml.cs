using Course_Project.Info;
using Course_Project.Pages.Foremen;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Course_Project.Pages.Directories
{
    /// <summary>
    /// Логика взаимодействия для Materials_ref.xaml
    /// </summary>
    public partial class Materials_ref : Window
    {
        public Materials_ref()
        {
            InitializeComponent();
            GridList.ItemsSource = OdbConnectHelper.entObj.Materials.ToList();
        }

        private void GridList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridList.SelectedItem != null)
            {
                Adding_material.mat = GridList.SelectedItem as Materials;
                Adding_material adm = new Adding_material();
                adm.ShowDialog();
            }
        }
        private void TbSort_TextChanged(object sender, TextChangedEventArgs e)
        {
            GridList.ItemsSource = OdbConnectHelper.entObj.Materials.Where(t => t.Title.Contains(TbSort.Text)).ToList();
        }
    }
}

using Course_Project.Info;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Course_Project.Pages.Foremen
{
    /// <summary>
    /// Логика взаимодействия для Adding_material.xaml
    /// </summary>
    public partial class Adding_material : Window
    {
        public Adding_material()
        {
            InitializeComponent();
            Txbkolvo.MaxLength = 5;            
            CmbMat.DisplayMemberPath = "Title";
            CmbMat.ItemsSource = OdbConnectHelper.entObj.Materials.ToList();
            CmbMat.SelectedItem = mat;
        }
        public static Info.Templates template;
        public static bool temp = false;
        public static Materials mat { get; set; }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (temp)
            {               
                if (OdbConnectHelper.entObj.TOrders.Where(o => o.Material_num == mat.idMat && o.Temp_num == template.id).Count() > 0)
                {
                    OdbConnectHelper.entObj.TOrders.Where(o => o.Material_num == mat.idMat && o.Temp_num == template.id).First().Quantity += int.Parse(Txbkolvo.Text);
                }
                else
                {
                    TOrders TOrdObj = new TOrders()
                    {
                        Quantity = int.Parse(Txbkolvo.Text),
                        Materials = mat,
                        Templates = template
                    };

                    OdbConnectHelper.entObj.TOrders.Add(TOrdObj);
                }   
                OdbConnectHelper.entObj.SaveChanges();
                temp = false;
            }
            else
            {
                int a = (int)Authorization.user.id_area;

                if (OdbConnectHelper.entObj.Orders.Where(o => o.Material_num == mat.idMat && o.Work_cod == a).Count() > 0)
                {
                    OdbConnectHelper.entObj.Orders.Where(o => o.Material_num == mat.idMat && o.Work_cod == a).First().Quantity += int.Parse(Txbkolvo.Text);
                }
                else
                {
                    Orders OrdObj = new Orders()
                    {
                        Quantity = int.Parse(Txbkolvo.Text),
                        Materials = mat,
                        Work_cod = Authorization.user.id_area,
                        Month = DateTime.Today.Month,
                        Year = DateTime.Today.Year
                    };                   
                    OdbConnectHelper.entObj.Orders.Add(OrdObj);
                }               
                OdbConnectHelper.entObj.SaveChanges();
            }
            MessageBox.Show("Запись добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);            
            this.DialogResult = true;
        }
        private bool cansave()
        {
            bool t = true;
            if (CmbMat.SelectedItem != null & Txbkolvo.Text.Length > 0)
            {
                t = true;
            }
            else
            {
                t = false;
            }
            return t;
        }

        private void Txbkolvo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            switch (e.Text)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "BACKSPACE":
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void Txbkolvo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cansave() == true)
            {
                btnAccept.IsEnabled = true;
            }               
            else btnAccept.IsEnabled = false;
        }

        private void CmbMat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbMat.SelectedItem != null)
            {
                Txbkolvo.Clear();
                string s = (CmbMat.SelectedItem as Info.Materials).Title;
                ed_izm.Content = OdbConnectHelper.entObj.Materials.Where(m => m.Title == s).FirstOrDefault().Unit_m;
            }

        }
    }
}

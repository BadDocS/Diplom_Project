using Course_Project.Info;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Course_Project.Pages.Directories;
using Course_Project.Pages.Admin;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace Course_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для Purchases.xaml
    /// </summary>
    public partial class Purchases : Page
    {
        bool Gen = false;
        bool Fin = false;
        public Purchases()
        {
            InitializeComponent();
            LbArea.ItemsSource = OdbConnectHelper.entObj.Work_Areas.ToList();
            LbMonth.Text = DateTime.Today.ToString("MMMM");
            
        }
        private void LbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbArea.SelectedItem != null)
            {
                btnArchive.IsEnabled = true;
                btnPrint.IsEnabled = true;
                btnExcel.IsEnabled = true;
                btnWord.IsEnabled = true;
                int si = (LbArea.SelectedItem as Work_Areas).Workshop_code;
                GridList.ItemsSource = OdbConnectHelper.entObj.Order_view.Where(t => t.Work_cod == si && t.Month == DateTime.Today.Month && t.Year == DateTime.Today.Year).ToList();
                TbSort.Text = "";
                Fin = false;
                Gen = false;
            }            
        }

        private void tbFilt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() == true)
            {
                p.PrintVisual(GridList, "Печать");
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void btnWord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage();
            ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Заказ");
            sheet.Columns[1].Width = 14.5;
            sheet.Columns[2].Width = 21;
            sheet.Columns[3].Width = 19;
            sheet.Columns[4].Width = 12;
            int Str = 1;
            sheet.Cells[Str, 1].Value = "Код материала";
            sheet.Cells[Str, 2].Value = "Наименование";
            sheet.Cells[Str, 3].Value = "Единица измерения";
            sheet.Cells[Str, 4].Value = "Количество";
            sheet.Cells[Str, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            sheet.Cells[Str, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            sheet.Cells[Str, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            sheet.Cells[Str, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            Str++;
            if (Gen)
            {
                List<General_order> Gen_ord = OdbConnectHelper.entObj.General_order.Where(o => o.Month == DateTime.Today.Month &&
                o.Year == DateTime.Today.Year).ToList();
                foreach (General_order o in Gen_ord)
                {
                    sheet.Cells[Str, 1].Value = o.Material_num;
                    sheet.Cells[Str, 2].Value = o.Title;
                    sheet.Cells[Str, 3].Value = o.Unit_m;
                    sheet.Cells[Str, 4].Value = o.Quantity;
                    sheet.Cells[Str, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[Str, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[Str, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[Str, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    Str++;
                }
            }
            else if (Fin)
            {
                List<Purchase_view> Pur_view = OdbConnectHelper.entObj.Purchase_view.Where(o => o.Month == DateTime.Today.Month &&
                o.Year == DateTime.Today.Year).ToList();
                foreach (Purchase_view o in Pur_view)
                {
                    sheet.Cells[Str, 1].Value = o.Material_num;
                    sheet.Cells[Str, 2].Value = o.Title;
                    sheet.Cells[Str, 3].Value = o.Unit_m;
                    sheet.Cells[Str, 4].Value = o.Quantity;
                    sheet.Cells[Str, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[Str, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[Str, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[Str, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    Str++;
                }
            }
            else
            {
                int si = (LbArea.SelectedItem as Work_Areas).Workshop_code;
                List<Order_view> Ord_view = OdbConnectHelper.entObj.Order_view.Where(t => t.Work_cod == si &&
                t.Month == DateTime.Today.Month && t.Year == DateTime.Today.Year).ToList();
                foreach (Order_view o in Ord_view)
                {
                    sheet.Cells[Str, 1].Value = o.Material_num;
                    sheet.Cells[Str, 2].Value = o.Title;
                    sheet.Cells[Str, 3].Value = o.Unit_m;
                    sheet.Cells[Str, 4].Value = o.Quantity;
                    sheet.Cells[Str, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[Str, 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[Str, 3].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    sheet.Cells[Str, 4].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    Str++;
                }
            }
            string Path = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
                Path = saveFileDialog.FileName;
            else
                return;
            if (File.Exists(Path))
            {
                try
                {
                    FileStream FS = File.Open(Path, FileMode.Open);
                    FS.Close();
                }
                catch
                {
                    MessageBox.Show("Файл запущен на компьютере. Пожалуйста выключите его",
                        "Файл недоступен",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
            }
            File.WriteAllBytes(Path, package.GetAsByteArray());
            Process.Start(Path);

        }

        private void GridList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new Materials_ref().ShowDialog();
        }

        private void LbArea_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Workshops.work_Ar = LbArea.SelectedItem as Work_Areas;
            new Workshops().ShowDialog();
        }

        private void btnFinOrd_Click(object sender, RoutedEventArgs e)
        {
            if (LbArea.SelectedItem != null)
            {
                LbArea.SelectedItem = null;
            }
            else
            {
                btnArchive.IsEnabled = true;
                btnPrint.IsEnabled = true;
                btnExcel.IsEnabled = true;
                btnWord.IsEnabled = true;
            }
            GridList.ItemsSource = OdbConnectHelper.entObj.Purchase_view.Where(o => o.Month == DateTime.Today.Month && 
            o.Year == DateTime.Today.Year).ToList();
            Fin = true;
            Gen = false;

        }

        private void btnGenOrd_Click(object sender, RoutedEventArgs e)
        {
            if (LbArea.SelectedItem != null)
            {
                LbArea.SelectedItem = null;
            }
            else
            {
                btnPrint.IsEnabled = true;
                btnExcel.IsEnabled = true;
                btnWord.IsEnabled = true;
            }
            GridList.ItemsSource = OdbConnectHelper.entObj.General_order.Where(o => o.Month == DateTime.Today.Month &&
            o.Year == DateTime.Today.Year).ToList();
            Fin = false;
            Gen = true;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }
        private void btnArchive_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new OLd());
        }

        private void TbSort_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LbArea.SelectedItem == null)
            {
                if (Gen)
                {
                    GridList.ItemsSource = OdbConnectHelper.entObj.General_order.Where(t => t.Title.Contains(TbSort.Text) &&
                    t.Month == DateTime.Today.Month && t.Year == DateTime.Today.Year).ToList();
                }
                else if (Fin)
                {
                    GridList.ItemsSource = OdbConnectHelper.entObj.Purchase_view.Where(t => t.Title.Contains(TbSort.Text) &&
                    t.Month == DateTime.Today.Month && t.Year == DateTime.Today.Year).ToList();
                }
            }
            else
            {
                int si = (LbArea.SelectedItem as Work_Areas).Workshop_code;
                GridList.ItemsSource = OdbConnectHelper.entObj.Order_view.Where(t => t.Title.Contains(TbSort.Text) && t.Work_cod == si &&
                t.Month == DateTime.Today.Month && t.Year == DateTime.Today.Year).ToList();
            }    
        }
    }
}

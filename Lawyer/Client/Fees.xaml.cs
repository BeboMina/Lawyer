using Lawyer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lawyer.Client
{
    /// <summary>
    /// Interaction logic for Fees.xaml
    /// </summary>
    public partial class Fees : Page
    {
        testEntities Context = new testEntities();
        List<Models.Fils_Fees> fils_Feess;

        public Fees()
        {
            InitializeComponent();

            fils_Feess = Context.Fils_Fees.ToList();
            GridView_Bills.ItemsSource = fils_Feess;
        }

        private void AddPaidBtn_Click(object sender, RoutedEventArgs e)
        {
            Client.AddFees addFees = new AddFees();
            addFees.ShowDialog();
        }

        private void SearchClientsTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GridView_Client_Paid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridView_Client_Paid.SelectedItem == null)
                return;

            Client.DisplayFees displayFees = new DisplayFees();
            displayFees.ShowDialog();
        }

        private void AddBillBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
                open.Multiselect = true;
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string message = "تاكيد حفظ الملفات";
                    string title = "حفظ";
                    MessageBoxButton buttons = MessageBoxButton.YesNo;
                    MessageBoxResult result = System.Windows.MessageBox.Show(message, title, buttons);
                    if (result == MessageBoxResult.Yes)
                    {
                        Models.Fils_Fees fils_Fees = new Models.Fils_Fees();
                        foreach (var item in open.FileNames)
                        {
                            fils_Fees.Data = File.ReadAllBytes(item);
                            fils_Fees.Title = System.IO.Path.GetFileNameWithoutExtension(item);
                            fils_Fees.Extantion = System.IO.Path.GetExtension(item);
                            Context.Fils_Fees.Add(fils_Fees);
                            Context.SaveChanges();
                        }
                        fils_Feess = Context.Fils_Fees.ToList();
                        GridView_Bills.ItemsSource = fils_Feess;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void RemoveBillBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (GridView_Bills.SelectedItem != null)
                {
                    string message = "تاكيد حزف الملفات";
                    string title = "حفظ";
                    MessageBoxButton buttons = MessageBoxButton.YesNo;
                    MessageBoxResult result = System.Windows.MessageBox.Show(message, title, buttons);
                    if (result == MessageBoxResult.Yes)
                    {
                        Models.Fils_Fees fils_Fees = new Models.Fils_Fees();
                        fils_Fees = fils_Feess.ElementAt(GridView_Bills.SelectedIndex);
                        Context.Fils_Fees.Remove(fils_Fees);
                        Context.SaveChanges();
                        fils_Feess = Context.Fils_Fees.ToList();
                        GridView_Bills.ItemsSource = fils_Feess;
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("اختار الملق اولا");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void SearchBillsTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SearchBillsTxt.Text == "")
                {
                    fils_Feess = Context.Fils_Fees.ToList();
                }
                else
                {
                    fils_Feess = Context.Fils_Fees.Where(F => F.Title.Contains(SearchBillsTxt.Text)).ToList();
                }
                GridView_Bills.ItemsSource = fils_Feess;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void GridView_Bills_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridView_Bills.SelectedItem == null)
                return;

            try
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    Models.Fils_Fees fils_Fees = new Models.Fils_Fees();
                    fils_Fees = fils_Feess.ElementAt(GridView_Bills.SelectedIndex);
                    string folder = folderBrowser.SelectedPath;
                    string name = fils_Fees.Title + fils_Fees.Extantion;
                    byte[] data = fils_Fees.Data;
                    File.WriteAllBytes(folder + "\\" + name, data);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}

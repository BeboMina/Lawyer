using Lawyer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace Lawyer.Proxy
{
    /// <summary>
    /// Interaction logic for DisplayProxy.xaml
    /// </summary>
    public partial class DisplayProxy : Window
    {
        testEntities Context = new testEntities();
        bool Add = false;
        byte[] data;
        string Name;
        string Ex;
        //List<NameEx> NameExt = new List<NameEx>();
        List<Models.FilesProcuration> NameExt1 = new List<Models.FilesProcuration>();
        long id;
        public DisplayProxy(Models.Procuration procuration)
        {
            InitializeComponent();
            id = procuration.ID;
            Models.Client client = Context.Clients.FirstOrDefault(C => C.IDProcuration == procuration.ID);
            NameExt1 = Context.FilesProcurations.Where(F => F.IDProcuration == procuration.ID).ToList();
            Client_Name.Text = client.Name;
            Client_Code.Text = client.ID;
            if(procuration.StardDate==null)
            {
                Date_Start.Text = "";
            }
            else
            {
                Date_Start.SelectedDate = procuration.StardDate;
            }
            if (procuration.EndDate == null)
            {
                Date_End.Text = "";
            }
            else
            {
                Date_End.SelectedDate = procuration.EndDate;
            }
            if(procuration.certified==true)
            {
                Button1.IsChecked=true;
            }
            else if(procuration.certified==false)
            {
                Button2.IsChecked = true;
            }
            if(NameExt1.Count!=0)
            {
                Grid_Files.ItemsSource = NameExt1;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Save_File_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                Add = true;
                NameExt1.Clear();
                Models.FilesProcuration nameEx = new FilesProcuration();
                nameEx.Title = System.IO.Path.GetFileNameWithoutExtension(open.FileName);
                nameEx.Extantion = System.IO.Path.GetExtension(open.FileName);
                nameEx.Date = File.ReadAllBytes(open.FileName);
                NameExt1.Add(nameEx);
                Name = System.IO.Path.GetFileNameWithoutExtension(open.FileName);
                Ex = System.IO.Path.GetExtension(open.FileName);
                data = File.ReadAllBytes(open.FileName);
                Grid_Files.ItemsSource = NameExt1;
                Grid_Files.Items.Refresh();
            }
        }
        private void Save_Date_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string message = "تاكيد حفظ بيانات التوكيل";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = System.Windows.MessageBox.Show(message, title, buttons);
                Models.Procuration procuration = Context.Procurations.FirstOrDefault(P => P.ID == id);
                if (Date_Start.Text != "")
                {
                    
                    procuration.StardDate = Date_Start.SelectedDate.Value;
                }
                else
                {
                    procuration.StardDate =null;
                }
                if (Date_End.Text != "")
                {
                    procuration.EndDate = Date_End.SelectedDate.Value;
                }
                else
                {
                    procuration.EndDate = null;
                }
                if (Button1.IsChecked == true)
                {
                    procuration.certified = true;
                }
                else if (Button2.IsChecked == true)
                {
                    procuration.certified = false;
                }
                else
                {
                    System.Windows.MessageBox.Show("التوكيل موثق ام غير موثق");
                    return;
                }
                if (result == MessageBoxResult.Yes)
                {
                    Context.SaveChanges();
                    if (Add)
                    {
                        Models.FilesProcuration filesProcuration1 = Context.FilesProcurations.FirstOrDefault(F => F.IDProcuration == id);
                        Models.FilesProcuration filesProcuration = new Models.FilesProcuration();
                        if (filesProcuration1==null)
                        {
                            filesProcuration.Date = data;
                            filesProcuration.Title = Name;
                            filesProcuration.Extantion = Ex;
                            filesProcuration.IDProcuration = procuration.ID;
                            Context.FilesProcurations.Add(filesProcuration);
                            Context.SaveChanges();
                        }
                        else
                        {
                            filesProcuration1.Date = data;
                            filesProcuration1.Title = Name;
                            filesProcuration1.Extantion = Ex;
                            Context.SaveChanges();
                        }
                        
                    }
                    Close();
                    MainWindow parent = (MainWindow)App.Current.MainWindow;
                    parent.main.Navigate(new Proxies());
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void Grid_Files_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Grid_Files.SelectedItem == null)
                return;

            try
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Models.FilesProcuration fils = (Models.FilesProcuration)Grid_Files.SelectedItem;
                    string folder = folderBrowser.SelectedPath;
                    string name = fils.Title + fils.Extantion;
                    byte[] data = fils.Date;
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

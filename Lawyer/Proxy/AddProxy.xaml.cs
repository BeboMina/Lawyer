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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lawyer.Proxy
{
    /// <summary>
    /// Interaction logic for AddProxy.xaml
    /// </summary>
    public partial class AddProxy : Window
    {
        testEntities Context = new testEntities();
        List<String> NameClient;
        List<String> IDClient;
        Models.Procuration procuration = new Models.Procuration();
        Models.Client client = new Models.Client();
        byte[] data;
        string Name;
        string Ex;
        int index;
        bool add = false;
        string action;
        public string NameFile { get; set; }
        public Models.Procuration procuration1{ get; set; }
        public byte[] data1 { get; set; }
        public string name1 { get; set; }
        public string ex1 { get; set; }
        string EX;
        NameEx NameExt = new NameEx();
        public AddProxy(string act)
        {
            InitializeComponent();
            action = act;
            NameClient = Context.Clients.Select(C => C.Name).ToList();
            IDClient = Context.Clients.Select(C => C.ID).ToList();
            Com_Name_Client.ItemsSource = NameClient;
            bool add = false;
            

            if(action == "update")
            {
                GboxHeader.Text = "تعديل توكيل";
            }
        }

        private void Client_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Close();
        }

        private void Save_Date_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string message = "تاكيد حفظ بيانات التوكيل";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = System.Windows.MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {
                    procuration.StardDate= Convert.ToDateTime(Date_Start.SelectedDate.Value);
                    procuration.EndDate= Convert.ToDateTime(Date_End.SelectedDate.Value);
                    if(Button1.IsChecked==true)
                    {
                        procuration.certified = true;
                    }
                    else if(Button2.IsChecked==true)
                    {
                        procuration.certified = false;
                    }
                    else
                    {
                        MessageBox.Show("التوكيل موثق ام غير موثق");
                        return;
                    }
                    if (action != "client_Add")
                    {
                        Context.Procurations.Add(procuration);
                        Context.SaveChanges();
                        String x = IDClient[index];
                        client = Context.Clients.FirstOrDefault(C => C.ID == x);
                        client.IDProcuration = procuration.ID;
                        Context.SaveChanges();
                        if (add)
                        {
                            Models.FilesProcuration filesProcuration = new Models.FilesProcuration();
                            filesProcuration.Date = data;
                            filesProcuration.Title = Name;
                            filesProcuration.Extantion = Ex;
                            filesProcuration.IDProcuration = procuration.ID;
                            Context.FilesProcurations.Add(filesProcuration);
                            Context.SaveChanges();
                        }
                        Close();
                        MainWindow parent = (MainWindow)App.Current.MainWindow;
                        parent.main.Navigate(new Proxies());
                    }
                    else
                    {
                        procuration1 = procuration;
                        if(add)
                        {
                            NameFile = NameExt.Name;
                            data1 = data;
                            name1 = Name;
                            ex1 = Ex;
                        }
                        else
                        {
                            NameFile = "";
                        }
                        
                    }
                    Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void Com_Name_Client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = Com_Name_Client.SelectedIndex;
        }

        private void Save_File_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                add = true;
                NameExt.Name = System.IO.Path.GetFileName(open.FileName);
                Name = System.IO.Path.GetFileNameWithoutExtension(open.FileName);
                Ex = System.IO.Path.GetExtension(open.FileName);
                data = File.ReadAllBytes(open.FileName);
                //Grid_Files;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    class NameEx
    {
        public String Name { get; set; }
    }
}

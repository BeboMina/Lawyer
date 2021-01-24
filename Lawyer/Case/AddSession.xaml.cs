using System;
using System.Collections.Generic;
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
using System.Windows.Forms;
using Lawyer.Models;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for AddSession.xaml
    /// </summary>
    public partial class AddSession : Window
    {
        testEntities Context = new testEntities();
        List<long> Num_Cases;
        long ID_Case;
        List<byte[]> data=new List<byte[]>();
        
        List<String> Name=new List<string>();
        List<String> Ex= new List<string>();
        List<NameEx> names = new List<NameEx>();
        
        bool add = false;
        
        public AddSession()
        {
            InitializeComponent();
            Num_Cases = Context.Cases.Select(C => C.ID).ToList();
            Com_Num_Case.ItemsSource = Num_Cases;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Com_Num_Case_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ID_Case =Convert.ToInt64(Com_Num_Case.SelectedItem);
            var IDCli = Context.Client_Case.FirstOrDefault(C=>C.IDCase==ID_Case);
            string ID = IDCli.IDClient;
            Models.Client nameClient = Context.Clients.FirstOrDefault(C => C.ID == ID);
            Client_Name.Text = nameClient.Name;
            ID_Client.Text = nameClient.ID;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string message = "تاكيد حفظ بيانات الجلسة";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = System.Windows.MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {
                    Models.Session session = new Models.Session();
                    session.Timer = Time_Session.Text;
                    session.date =Convert.ToDateTime(Data_Session.SelectedDate.Value);
                    session.NextDate = Convert.ToDateTime(NextData_Session.SelectedDate.Value);
                    session.Jadge = jadge.Text;
                    session.IDCase = ID_Case;
                    Context.Sessions.Add(session);
                    Context.SaveChanges();
                    if(add)
                    {
                        Models.FilesCas filesCas = new Models.FilesCas();
                        for(int i=0;i<Name.Count;i++)
                        {
                            filesCas.Data = data[i];
                            filesCas.Title = Name[i];
                            filesCas.Extantion = Ex[i];
                            filesCas.IDSessios = session.ID;
                            Context.FilesCases.Add(filesCas);
                            Context.SaveChanges();
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

        private void Add_File_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
            open.Multiselect = true;
            if(open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                add = true;
                
                foreach (var f in open.FileNames)
                {
                    NameEx NameExt = new NameEx();
                    NameExt.Name=System.IO.Path.GetFileName(f);
                    
                    names.Add(NameExt);
                    
                    Name.Add(System.IO.Path.GetFileNameWithoutExtension(f));
                    Ex.Add(System.IO.Path.GetExtension(f));
                    data.Add(File.ReadAllBytes(f));
                }
                
                Files_Session.ItemsSource = names;
                Files_Session.Items.Refresh();
            }
        }

        private void Deleat_Files_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ind = -1;
                ind = Files_Session.SelectedIndex;
                if (Name.Count > 0&&ind>-1)
                {
                    
                    Name.RemoveAt(ind);
                    Ex.RemoveAt(ind);
                    data.RemoveAt(ind);
                    names.RemoveAt(ind);
                    Files_Session.Items.Refresh();
                }
            }catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
    class NameEx
    {
        public String Name { get; set; }
    }
}

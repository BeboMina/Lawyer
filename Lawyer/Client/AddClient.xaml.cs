using Lawyer.Case;
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
using System.Data.SqlClient;
using System.Data;
using Lawyer.Models;
using System.IO;

namespace Lawyer
{
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        Models.Client client = new Models.Client();
        int invNo ;
        List<string> NamesClient;
        testEntities Context = new testEntities();
        List<Models.Client> clients;
        string action;
        List<Models.Case> cases = new List<Models.Case>();
        string NameFile="";
        string Name1;
        string Ex;
        byte[] data;
        Models.Procuration procuration;
        public AddClient(string act)
        {
            InitializeComponent();
            action = act;
            
            try 
            {
                clients = Context.Clients.ToList();
                
                if ( action == "اضافة")
                {
                    invNo = Convert.ToInt32(Context.Clients.Max(C => C.IDmax));
                    if (invNo == 0)
                    {
                        invNo = 2004;
                    }
                    invNo++;
                    string NewID = (invNo).ToString();
                    NewID = "FA" + NewID;
                    client.ID = NewID;
                    ID_Client.Text = NewID;
                    
                }
                else
                {
                    NamesClient = Context.Clients.Select(C => C.Name).ToList();
                    Name_client_combo.ItemsSource = NamesClient;
                    Name_client_combo.SelectedIndex = 0;
                    int index = 0;
                    change_data(index);
                    Add_Case.Visibility = Visibility.Collapsed;
                    Add_Proxy.Visibility = Visibility.Collapsed;


                }
                ID_Client.IsReadOnly = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddCasebtn_Click(object sender, RoutedEventArgs e)
        {

            if (action == "اضافة")
            {
                if (Name_Client.Text.TrimEnd() == "" || Name_Client.Text == null)
                {
                    MessageBox.Show("ادخل اسم العميل فى البداية");
                }
                else
                {
                    AddCase addCase = new AddCase("Client_Add");
                    addCase.Client_Name.Visibility = Visibility.Collapsed;
                    addCase.Client_Name_Txt.Visibility = Visibility.Visible;
                    addCase.Client_Name_Txt.Text = Name_Client.Text;
                    addCase.IDcl = ID_Client.Text;
                    addCase.ShowDialog();
                    if(addCase.cases!=null)
                        cases.Add(addCase.cases);
                }
            }
            else
            {
                AddCase addCase = new AddCase("Client_Edit");
                addCase.Client_Name.Visibility = Visibility.Collapsed;
                addCase.Client_Name_Txt.Visibility = Visibility.Visible;
                addCase.Client_Name_Txt.Text = client.Name;
                addCase.IDcl = ID_Client.Text;
                addCase.ShowDialog();
            }
            
        }

        private void ID_Client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Save_Client(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name_Client.Text) && action == "اضافة")
            {
                MessageBox.Show("يجب ادخال اسم العميل");
                return;
            }
            
            try
            {

                string message = "تاكيد حفظ بيانات العميل";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {
                    
                    client.IDmax = invNo;
                    client.PersonalID = PersonalId_Client.Text;
                    client.Email = Email_Client.Text;
                    client.Phone = Phone_Client.Text;
                    client.Notes = new TextRange(Notes_Client.Document.ContentStart, Notes_Client.Document.ContentEnd).Text;
                    client.Address = Address_Client.Text;
                    if(action!= "اضافة")
                    {
                        Models.Client cl = Context.Clients.FirstOrDefault(C => C.ID == client.ID);
                        cl.Notes = client.Notes;
                        cl.PersonalID = client.PersonalID;
                        cl.Email = client.Email;
                        cl.Address = client.Address;
                        cl.Phone = client.Phone;
                        Context.SaveChanges();
                    }
                    if (action == "اضافة")
                    {
                        if (procuration != null)
                        {
                            Context.Procurations.Add(procuration);
                            Context.SaveChanges();
                            client.IDProcuration = procuration.ID;
                        }
                        if(NameFile!=""&&NameFile!=null)
                        {
                            Models.FilesProcuration filesProcuration = new Models.FilesProcuration();
                            filesProcuration.Date=data;
                            filesProcuration.Title = Name1;
                            filesProcuration.Extantion = Ex;
                            filesProcuration.IDProcuration = procuration.ID;
                            Context.FilesProcurations.Add(filesProcuration);
                            
                        }
                        client.Name = Name_Client.Text;
                        Context.Clients.Add(client);
                        
                    }
                    
                    if(cases.Count!=0)
                    {
                        foreach(var item in cases)
                        {
                            Context.Cases.Add(item);
                            Context.SaveChanges();
                            Models.Client_Case client_Case = new Client_Case();
                            client_Case.IDCase = item.ID;
                            client_Case.IDClient = client.ID;
                            item.Client_Case.Add(client_Case);
                            
                        }
                        
                    }
                    Context.SaveChanges();
                    Close();
                    MainWindow parent = (MainWindow)App.Current.MainWindow;
                    parent.main.Navigate(new Clients());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Name_client_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (action == "تعديل")
            {
                
                int index = Name_client_combo.SelectedIndex;
                change_data(index);
            }
        }
        private void change_data(int index)
        {
            client.ID = clients[index].ID;
            client.Name = clients[index].Name;
            ID_Client.Text = clients[index].ID;
            PersonalId_Client.Text = clients[index].PersonalID;
            Email_Client.Text = clients[index].Email;
            Phone_Client.Text = clients[index].Phone;
            Address_Client.Text = clients[index].Address;
            invNo = Convert.ToInt32(clients[index].IDmax);
            new TextRange(Notes_Client.Document.ContentStart, Notes_Client.Document.ContentEnd).Text = clients[index].Notes;
        }

        private void AddProxyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (action == "اضافة")
            {
                if (Name_Client.Text.TrimEnd() == "" || Name_Client.Text == null)
                {
                    MessageBox.Show("ادخل اسم العميل فى البداية");
                }
                else
                {
                    Proxy.AddProxy addProxy = new Proxy.AddProxy("client_Add");
                    addProxy.Com_Name_Client.Visibility = Visibility.Collapsed;
                    addProxy.Text_Name_Client.Visibility = Visibility.Visible;
                    addProxy.Text_Name_Client.Text = Name_Client.Text;
                    addProxy.Code_Client.Text = ID_Client.Text;
                    addProxy.ShowDialog();
                    NameFile = addProxy.NameFile;
                    procuration = addProxy.procuration1;
                    if(NameFile!="")
                    {
                        Ex = addProxy.ex1;
                        data = addProxy.data1;
                        Name1 = addProxy.name1;
                    }
                }
            }
            else
            {
                Proxy.AddProxy addProxy = new Proxy.AddProxy("client_Edit");
                addProxy.Com_Name_Client.Visibility = Visibility.Collapsed;
                addProxy.Text_Name_Client.Visibility = Visibility.Visible;
                addProxy.Text_Name_Client.Text = client.Name;
                addProxy.ShowDialog();
            }
        }
    }
}

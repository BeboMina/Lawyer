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
            AddCase addCase = new AddCase();
            addCase.ShowDialog();
        }

        private void ID_Client_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Save_Client(object sender, RoutedEventArgs e)
        {
            try
            {

                string message = "تاكيد حفظ بيانات العميل";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {
                    if(action == "اضافة")
                    {
                        client.Name = Name_Client.Text;
                    }
                    client.IDmax = invNo;
                    client.PersonalID = PersonalId_Client.Text;
                    client.Email = Email_Client.Text;
                    client.Phone = Phone_Client.Text;
                    client.Notes = new TextRange(Notes_Client.Document.ContentStart, Notes_Client.Document.ContentEnd).Text;
                    client.Address = Address_Client.Text;
                    if(action!= "اضافة")
                    {
                        MessageBox.Show(client.ID);
                        Models.Client cl = Context.Clients.FirstOrDefault(C => C.ID == client.ID);
                        Context.Clients.Remove(cl);
                        Context.SaveChanges();
                    }
                    Context.Clients.Add(client);
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
            Proxy.AddProxy addProxy = new Proxy.AddProxy("client");
            addProxy.ShowDialog();

        }
    }
}

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
        testEntities1 Context = new testEntities1();
        List<Models.Client> clients;
        public AddClient()
        {
            InitializeComponent();
            
            try 
            {
                clients = Context.Clients.ToList();
                if ( GboxHeader.Text== "اضافة عميل")
                {
                    MessageBox.Show(GboxHeader.Text);
                    MessageBox.Show("asdasd");
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
                    ID_Client.IsReadOnly = true;
                }
                else
                {
                    MessageBox.Show(GboxHeader.Text);
                    MessageBox.Show("asdasd");
                    NamesClient = Context.Clients.Select(C => C.Name).ToList();
                    Name_client_combo.ItemsSource = NamesClient;
                    Name_client_combo.SelectedIndex = 0;
                }
            }catch(Exception ex)
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
            addCase.Topmost = true;
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
                    client.Name = Name_Client.Text;
                    client.IDmax = invNo;
                    client.PersonalID = PersonalId_Client.Text;
                    client.Email = Email_Client.Text;
                    client.Phone = Phone_Client.Text;
                    client.Notes = new TextRange(Notes_Client.Document.ContentStart, Notes_Client.Document.ContentEnd).Text;
                    client.Address = address.Text;
                    Context.Clients.Add(client);
                    Context.SaveChanges();
                    Close();
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
            if (GboxHeader.Text != "اضافة عميل")
            {
                int index = Name_client_combo.SelectedIndex;
                ID_Client.Text = clients[index].ID;
                PersonalId_Client.Text = clients[index].PersonalID;
                Email_Client.Text = clients[index].Email;
                Phone_Client.Text = clients[index].Phone;
                invNo = Convert.ToInt32(clients[index].IDmax);
                new TextRange(Notes_Client.Document.ContentStart, Notes_Client.Document.ContentEnd).Text = clients[index].Notes;
            }
        }
    }
}

using Lawyer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lawyer
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    ///
    
    public partial class Clients : Page
    {
        testEntities Context = new testEntities();
        public Clients()
        {
            
            InitializeComponent();
            try
            {

                List<Models.Client> clients = Context.Clients.ToList();

                GridView_Client.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient("اضافة");
            addClient.ShowDialog();
        }

        private void UpdateClientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient("تعديل");
            addClient.GboxHeader.Text = "تعديل عميل";
            addClient.Name_Client.Visibility = Visibility.Collapsed;
            addClient.Name_client_combo.Visibility = Visibility.Visible;
            addClient.ShowDialog();
        }

        private void GridView_Client_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Models.Client client = (Models.Client)GridView_Client.SelectedItem;
            if (client == null)
                return;
            
            Client.DisplayClient displayClient = new Client.DisplayClient();
            displayClient.ID_Client.Text = client.ID;
            displayClient.Name_Client.Text = client.Name;
            displayClient.Phone_Client.Text = client.Phone;
            displayClient.PersonalId_Client.Text = client.PersonalID;
            displayClient.Address_Client.Text = client.Address;
            displayClient.Email_Client.Text = client.Email;
            new TextRange(displayClient.Notes_Client.Document.ContentStart, displayClient.Notes_Client.Document.ContentEnd).Text = client.Notes;
            Models.Procuration procuration = Context.Procurations.FirstOrDefault(p => p.ID == client.IDProcuration);
            if (procuration!=null)
            {
                displayClient.Start_Date.Text = Convert.ToDateTime(procuration.StardDate).ToString("yyyy / MM / dd");
                displayClient.End_Date.Text = Convert.ToDateTime(procuration.EndDate).ToString("yyyy / MM / dd");
                displayClient.Certified.Text = procuration.certified.Value ? "موثق" : "غير موثق";
            }
           
            displayClient.ShowDialog();
        }
        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Models.Client> clients;
                if (SearchTxt.Text == "")
                {
                    clients = Context.Clients.ToList();

                }
                else
                {
                    clients = Context.Clients.Where(C => C.Name.Contains(SearchTxt.Text)).ToList();
                }
                GridView_Client.ItemsSource = clients;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }
    }
}

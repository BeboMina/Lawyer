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
        testEntities1 Context = new testEntities1();
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

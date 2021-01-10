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
        string connetionString = null;
        SqlCommand command;
        SqlConnection cnn;
        SqlDataReader dataReader = null;
        SqlDataAdapter dataAdapter;
        public Clients()
        {
            InitializeComponent();
            try 
            {
                connetionString = "Data Source=DESKTOP-AQLH556\\SQLEXPRESS;Initial Catalog=test;Integrated Security=SSPI;";
                cnn = new SqlConnection(connetionString);
                string query = "SELECT * from Client";
                cnn.Open();
                command = new SqlCommand(query, cnn);
                dataAdapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable("Client");
                dataAdapter.Fill(dt);
                GridView_Client.ItemsSource = dt.DefaultView;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
        }

        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.ShowDialog();
        }

        private void UpdateClientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.GboxHeader.Text = "تعديل عميل";
            addClient.Name_Client.Visibility = Visibility.Collapsed;
            addClient.Name_client_combo.Visibility = Visibility.Visible;
            addClient.ShowDialog();
        }

        private void GridView_Client_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // bebo, you should adapt this to work cause I don't know for sure how to use mvc 
            //Models.Client client = (Models.Client)GridView_Client.SelectedItem;
            //if (client == null)
            //    return;

            Client.DisplayClient displayClient = new Client.DisplayClient();
            displayClient.ShowDialog();
        }
    }
}

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


namespace Lawyer
{
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        String ID, Name, PersonalID, Email, Phone, Notes;
        string connetionString = null;
        SqlCommand command;
        SqlConnection cnn;
        SqlDataReader dataReader=null;
        int invNo = 10;
        public AddClient()
        {
            InitializeComponent();
            
            try 
            {

                connetionString = "Data Source=.;Initial Catalog=test;Integrated Security=SSPI;";
                cnn = new SqlConnection(connetionString);
                string query = "SELECT MAX (IDmax) from Client";
                cnn.Open();
                command = new SqlCommand(query, cnn);
                if (Convert.IsDBNull(command.ExecuteScalar()))
                {
                    invNo = 2004;
                }
                else
                {
                    invNo = Convert.ToInt32(command.ExecuteScalar());
                    
                }
                invNo++;
                string NewID = (invNo).ToString();
                NewID = "FA" + NewID;
                ID = NewID;
                ID_Client.Text = NewID;
                ID_Client.IsReadOnly = true;
                cnn.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                cnn.Close();
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
                    Name = Name_Client.Text;
                    PersonalID = PersonalId_Client.Text;
                    Email = Email_Client.Text;
                    Phone = Phone_Client.Text;
                    Notes = new TextRange(Notes_Client.Document.ContentStart, Notes_Client.Document.ContentEnd).Text;
                    string saveStaff = "Insert into Client (ID,IDmax,Name,Email,Phone,PersonalID,Address,IDProcuration,Notes) " +
                           " values (@ID,@IDmax,@Name,@Email,@Phone,@PersonalID,@Address,@IDProcuration,@Notes);";
                    SqlCommand command = new SqlCommand(saveStaff, cnn);
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@IDmax", invNo);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@PersonalID", PersonalID);
                    command.Parameters.AddWithValue("@Address", DBNull.Value);
                    command.Parameters.AddWithValue("@IDProcuration", 11);
                    command.Parameters.AddWithValue("@Notes", Notes);
                    cnn.Open();
                    command.ExecuteNonQuery();
                    cnn.Close();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cnn.Close();
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

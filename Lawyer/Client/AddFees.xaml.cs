using Lawyer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lawyer.Client
{
    /// <summary>
    /// Interaction logic for AddFees.xaml
    /// </summary>
    public partial class AddFees : Window
    {
        testEntities Context = new testEntities();
        List<Models.Client> clients = new List<Models.Client>();
        List<string> Client_Names=new List<string>();
        int index = -1;
        public AddFees()
        {
            InitializeComponent();
            clients = Context.Clients.ToList();
            foreach(var item in clients)
            {
                Client_Names.Add(item.Name);
            }
            Client_Name.ItemsSource = Client_Names;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string message = "تاكيد حفظ البيانات ";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {
                    if(index == -1)
                    {
                        MessageBox.Show("اختر العاميل اولا ");
                        return;
                    }
                    else if(Fee_Date==null)
                    {
                        MessageBox.Show("ادخل التاريخ ");
                        return;
                    }
                    else if(Paid_Amount.Text=="")
                    {
                        MessageBox.Show("ادخل المبلغ ");
                        return;
                    }
                    else
                    {
                        Models.Fee fee = new Fee();
                        fee.Date = Convert.ToDateTime(Fee_Date.SelectedDate.Value);
                        fee.Quantity = Convert.ToInt64(Paid_Amount.Text);
                        fee.Notes= new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
                        fee.IDClient = clients[index].ID;
                        Context.Fees.Add(fee);
                        Context.SaveChanges();
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Client_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = Client_Name.SelectedIndex;
        }

        private void Paid_Amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

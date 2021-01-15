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
using Lawyer.Models;
namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for AddCase.xaml
    /// </summary>
    /// 
    public partial class AddCase : Window
    {
        testEntities Context = new testEntities();
        List<string> Names;
        Models.Jadge jadge = new Models.Jadge();
        List<Models.Client> clients;
        Models.Case Case = new Models.Case();
        Models.Client_Case Client_Case = new Client_Case();
        int index = 0;
        public AddCase()
        {
            InitializeComponent();
            try
            {
                clients = Context.Clients.ToList();
                Names = Context.Clients.Select(C => C.Name).ToList();
                Client_Name.ItemsSource = Names;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string message = "تاكيد حفظ بيانات الدعوة";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {
                    Case.ID =Convert.ToInt64(Number_Case.Text);
                    Case.Circle = circle.Text;
                    Case.Notes= new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
                    Client_Case.IDCase = Case.ID;
                    Client_Case.IDClient = clients[index].ID;
                    Case.Type = Tybe_case.Text;
                    Case.Client_Case.Add(Client_Case);
                    Context.Cases.Add(Case);
                    Context.SaveChanges();
                    Close();
                    MainWindow parent = (MainWindow)App.Current.MainWindow;
                    parent.main.Navigate(new Cases());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Client_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index =Client_Name.SelectedIndex;
        }
    }
}

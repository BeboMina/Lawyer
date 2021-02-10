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
        int index = -1;
        string Action;
        public Models.Case cases { get; set; }
        public String IDcl { get; set; }
        public AddCase(string act)
        {
            InitializeComponent();
            Action = act;
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
            if(string.IsNullOrWhiteSpace(Number_Case.Text) || (Action == "case" && index == -1))
            {
                MessageBox.Show("يجب اختيار عميل واضافة رقم الدعوى");
                return;
            }
            
            try
            {

                string message = "تاكيد حفظ بيانات الدعوة";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {
                    Case.Lock = false;
                    if (Action == "Client_Add")
                    {
                        Case.ID = Convert.ToInt64(Number_Case.Text);
                        Case.Circle = circle.Text;
                        Case.Notes = new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
                        Client_Case.IDCase = Case.ID;
                        Client_Case.IDClient = IDcl;
                        Case.Type = Tybe_case.Text;
                        cases = Case;
                        Close();
                    }
                    else if (Action=="Case")
                    {
                        Case.ID = Convert.ToInt64(Number_Case.Text);
                        Case.Circle = circle.Text;
                        Case.Notes = new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
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
                    else if(Action== "Client_Edit")
                    {
                        Case.ID = Convert.ToInt64(Number_Case.Text);
                        Case.Circle = circle.Text;
                        Case.Notes = new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
                        Client_Case.IDCase = Case.ID;
                        Client_Case.IDClient = IDcl;
                        Case.Type = Tybe_case.Text;
                        Case.Client_Case.Add(Client_Case);
                        Context.Cases.Add(Case);
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
            index =Client_Name.SelectedIndex;
        }

        private void Number_Case_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

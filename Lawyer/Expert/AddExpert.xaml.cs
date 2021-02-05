using Lawyer.Models;
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

namespace Lawyer.Expert
{
    /// <summary>
    /// Interaction logic for AddExpert.xaml
    /// </summary>
    public partial class AddExpert : Window
    {
        testEntities Context = new testEntities();
        Models.EXpert eXpert = new EXpert();
        List<long> Case_IDs = new List<long>();
        List<Models.Case> cases = new List<Models.Case>();
        long ID=-1;
        string act;
        public AddExpert(string action)
        {
            InitializeComponent();
            act = action;
            if(action == "update")
            {
                GboxHeader.Text = "تعديل الخبير";
                Case_Number_combo.Visibility = Visibility.Collapsed;
                Case_Number.Visibility = Visibility.Visible;
            }
            else
            {
                Case_IDs = Context.Cases.Select(C => C.ID).ToList();
                Case_Number_combo.ItemsSource = Case_IDs;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Case_Number_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ID =(long) Case_Number_combo.SelectedItem;
        }

        private void Save_Expert(object sender, RoutedEventArgs e)
        {
            try
            {
                string message = "تاكيد حفظ بيانات الخبير";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if(ID==-1)
                {
                    MessageBox.Show("اختر رقم الدعوة اولا");
                    return;
                }
                if (Date_1.SelectedDate != null)
                {
                    eXpert.Date1 = Date_1.SelectedDate.Value;
                }
                if (Date_2.SelectedDate != null)
                {
                    eXpert.Date2 = Date_2.SelectedDate.Value;
                }
                if(Date_3.SelectedDate!=null)
                {
                    eXpert.Date3 = Date_3.SelectedDate.Value;
                }
                if (result == MessageBoxResult.Yes)
                {
                    eXpert.Name = Name_Expert.Text;
                    eXpert.Address = Address_Expert.Text;
                    eXpert.Email = Email_Expert.Text;
                    eXpert.Phone = Phone_Expert.Text;
                    eXpert.Notes = new TextRange(Notes_Expert.Document.ContentStart, Notes_Expert.Document.ContentEnd).Text;
                    if(act== "add")
                    {
                        Context.EXperts.Add(eXpert);
                    }
                    Context.SaveChanges();
                    Close();
                }
                //MainWindow parent = (MainWindow)App.Current.MainWindow;
                //parent.main.Navigate(new DisplayStaff(stuff));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Case_Type_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

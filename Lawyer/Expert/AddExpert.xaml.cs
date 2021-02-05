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
        List<Models.Resumption> resumptions = new List<Resumption>();
        long ID=-1;
        string act;
        int index = 0;
        List<string> Degree = new List<string> { "ابتدائية", "استئناف", "نقض" };
        long Id_Expert;
        public AddExpert(string action,long id)
        {
            InitializeComponent();
            act = action;
            if(action == "update")
            {
                Id_Expert = id;
                GboxHeader.Text = "تعديل الخبير";
                Case_Number_combo.Visibility = Visibility.Collapsed;
                Case_Number.Visibility = Visibility.Visible;
                Case_Type_combo.Visibility = Visibility.Collapsed;
                Case_Type.Visibility = Visibility.Visible;
            }
            else
            {
                Case_Type_combo.SelectedIndex = index;
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
                if(ID==-1&&act!= "update")
                {
                    MessageBox.Show("اختر رقم الدعوة اولا");
                    return;
                }
                
                if (result == MessageBoxResult.Yes)
                {
                    if (Date_1.SelectedDate != null)
                    {
                        eXpert.Date1 = Date_1.SelectedDate.Value;
                    }
                    if (Date_2.SelectedDate != null)
                    {
                        eXpert.Date2 = Date_2.SelectedDate.Value;
                    }
                    if (Date_3.SelectedDate != null)
                    {
                        eXpert.Date3 = Date_3.SelectedDate.Value;
                    }
                    eXpert.Case_ID = ID;
                    eXpert.Case_Degree = Degree[index];
                    eXpert.Name = Name_Expert.Text;
                    eXpert.Address = Address_Expert.Text;
                    eXpert.Email = Email_Expert.Text;
                    eXpert.Phone = Phone_Expert.Text;
                    eXpert.Notes = new TextRange(Notes_Expert.Document.ContentStart, Notes_Expert.Document.ContentEnd).Text;
                    if(act== "add")
                    {
                        Context.EXperts.Add(eXpert);
                    }
                    else
                    {
                        Models.EXpert eXpert1 = Context.EXperts.FirstOrDefault(E => E.ID == Id_Expert);
                        if (Date_1.SelectedDate != null)
                        {
                            eXpert1.Date1 = Date_1.SelectedDate.Value;
                        }
                        else
                        {
                            eXpert1.Date1 = null;
                        }
                        if (Date_2.SelectedDate != null)
                        {
                            eXpert1.Date2 = Date_2.SelectedDate.Value;
                        }
                        else
                        {
                            eXpert1.Date2 = null;
                        }
                        if (Date_3.SelectedDate != null)
                        {
                            eXpert1.Date3 = Date_3.SelectedDate.Value;
                        }
                        else
                        {
                            eXpert1.Date3 = null;
                        }
                        eXpert1.Case_ID = ID;
                        eXpert1.Case_Degree = Degree[index];
                        eXpert1.Name = Name_Expert.Text;
                        eXpert1.Address = Address_Expert.Text;
                        eXpert1.Email = Email_Expert.Text;
                        eXpert1.Phone = Phone_Expert.Text;
                        eXpert1.Notes = new TextRange(Notes_Expert.Document.ContentStart, Notes_Expert.Document.ContentEnd).Text;

                    }
                    Context.SaveChanges();
                    Close();
                }
                MainWindow parent = (MainWindow)App.Current.MainWindow;
                parent.main.Navigate(new Experts());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Case_Type_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = Case_Type_combo.SelectedIndex;
            if (Case_Type_combo.SelectedIndex==0)
            {
                Case_IDs = Context.Cases.Select(C => C.ID).ToList();
                Case_Number_combo.ItemsSource = Case_IDs;
            }
            else if(Case_Type_combo.SelectedIndex == 1)
            {
                Case_IDs = Context.vetoes.Select(C => C.ID_veto).ToList();
                Case_Number_combo.ItemsSource = Case_IDs;
            }
            else if(Case_Type_combo.SelectedIndex == 2)
            {
                Case_IDs = Context.Resumptions.Select(C => C.ID_Resumption).ToList();
                Case_Number_combo.ItemsSource = Case_IDs;
            }
        }
    }
}

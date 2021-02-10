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

namespace Lawyer.Staff
{
    /// <summary>
    /// Interaction logic for AddLawyer.xaml
    /// </summary>
    /// 
    public partial class AddLawyer : Window
    {
        testEntities Context = new testEntities();
        public string act;
        int index = 0;
        List<Models.Stuff> stuffs = new List<Stuff>();
        List<string> Client_Names=new List<string>();
        public AddLawyer(string action)
        {
            InitializeComponent();
            try
            {
                act = action;
                if (action == "update")
                {
                    GboxHeader.Text = "تعديل موظف";
                    Name_Lawyer.Visibility = Visibility.Collapsed;
                    Name_Lawyer_combo.Visibility = Visibility.Visible;

                    stuffs = Context.Stuffs.ToList();
                    Client_Names = Context.Stuffs.Select(S => S.Name).ToList();
                    Name_Lawyer_combo.ItemsSource = Client_Names;
                    Name_Lawyer_combo.SelectedIndex = 0;
                    FillText();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillText()
        {
            Name_Lawyer.Text = stuffs[index].Name;
            Phone_Lawyer.Text = stuffs[index].phone;
            Email_Lawyer.Text = stuffs[index].Email;
            Salary_Lawyer.Text = stuffs[index].Salar.ToString();
            Address_Lawyer.Text = stuffs[index].Address;
            new TextRange(Notes_Lawyer.Document.ContentStart, Notes_Lawyer.Document.ContentEnd).Text = stuffs[index].Notes;
        }

        private void Name_Lawyer_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = Name_Lawyer_combo.SelectedIndex;
            FillText();
        }

        private void Lawyer_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Stuff stuff = new Stuff();
                string message = "تاكيد حفظ بيانات الموظف";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {

                    stuff.Name = Name_Lawyer.Text;
                    stuff.phone = Phone_Lawyer.Text;
                    if(Salary_Lawyer.Text!=null&& Salary_Lawyer.Text!="")
                    {
                        stuff.Salar = Convert.ToDouble(Salary_Lawyer.Text);
                    }
                    else if(Salary_Lawyer.Text == "")
                    {
                        stuff.Salar = 0;
                    }
                    stuff.Address = Address_Lawyer.Text;
                    stuff.Notes= new TextRange(Notes_Lawyer.Document.ContentStart, Notes_Lawyer.Document.ContentEnd).Text;
                    stuff.Email = Email_Lawyer.Text;
                    if (act == "add")
                    {
                        Context.Stuffs.Add(stuff);
                        Context.SaveChanges();
                        Close();
                    }
                    if (act == "update")
                    {
                        long x = stuffs[index].ID;
                        Models.Stuff stuff1 = Context.Stuffs.FirstOrDefault(S=>S.ID==x);
                        if(stuff1!=null)
                        {
                            //stuff1.Name = stuff.Name;
                            stuff1.phone = stuff.phone;
                            stuff1.Email = stuff.Email;
                            stuff1.Salar = stuff.Salar;
                            stuff1.Notes = stuff.Notes;
                            Context.SaveChanges();
                            Close();
                        }
                    }
                }
                MainWindow parent = (MainWindow)App.Current.MainWindow;
                parent.main.Navigate(new Staff());
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

        private void Salary_Lawyer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

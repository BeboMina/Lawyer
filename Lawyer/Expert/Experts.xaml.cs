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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lawyer.Expert
{
    /// <summary>
    /// Interaction logic for Experts.xaml
    /// </summary>
    public partial class Experts : Page
    {
        testEntities Context = new testEntities();
        List<Models.EXpert> eXperts = new List<EXpert>();
        public Experts()
        {
            InitializeComponent();
            eXperts = Context.EXperts.ToList();
            GridView_Expert.ItemsSource = eXperts;
        }

        private void AddExpertBtn_Click(object sender, RoutedEventArgs e)
        {
            Expert.AddExpert addExpert = new AddExpert("add",-1);
            addExpert.ShowDialog();
        }

        private void GridView_Expert_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Models.EXpert eXpert = (Models.EXpert)GridView_Expert.SelectedItem;
            if(eXpert==null)
            {
                return;
            }
            Expert.AddExpert addExpert = new AddExpert("update",eXpert.ID);
            addExpert.Name_Expert.Text = eXpert.Name;
            addExpert.Case_Number.Text = eXpert.Case_ID.ToString();
            addExpert.Case_Type.Text = eXpert.Case_Degree.ToString();
            addExpert.Phone_Expert.Text = eXpert.Phone;
            addExpert.Email_Expert.Text = eXpert.Email;
            new TextRange(addExpert.Notes_Expert.Document.ContentStart, addExpert.Notes_Expert.Document.ContentEnd).Text = eXpert.Notes ;
            addExpert.Address_Expert.Text = eXpert.Address;
            if(eXpert.Date1!=null)
            {
                addExpert.Date_1.SelectedDate = eXpert.Date1.Value;
            }
            if(eXpert.Date2 != null)
            {
                addExpert.Date_2.SelectedDate = eXpert.Date2.Value;
            }
            if (eXpert.Date3 != null)
            {
                addExpert.Date_3.SelectedDate = eXpert.Date3.Value;
            }
            addExpert.ShowDialog();
        }
    }
}

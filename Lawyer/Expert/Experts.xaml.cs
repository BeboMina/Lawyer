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
        List<dbExpert> dbExperts = new List<dbExpert>();
        public Experts()
        {
            InitializeComponent();
            eXperts = Context.EXperts.ToList();
            foreach(var e in eXperts)
            {
                dbExpert dbExpert = new dbExpert();
                dbExpert.Address =e.Address ;
                dbExpert.Case_Degree = e.Case_Degree;
                dbExpert.Case_ID = e.Case_ID;
                dbExpert.Date1 = e.Date1;
                dbExpert.Date2 = e.Date2;
                dbExpert.Date3 = e.Date3;
                dbExpert.Email = e.Email;
                dbExpert.ID = e.ID;
                dbExpert.Name = e.Name;
                dbExpert.Notes=e.Notes;
                dbExpert.Phone = e.Phone;

                if(e.Case_Degree== "ابتدائية")
                {
                    Models.Case @case= Context.Cases.FirstOrDefault(C => C.ID==e.Case_ID);
                    dbExpert.Case_Number = @case.Case_Namber;
                }
                else if(e.Case_Degree == "استئناف")
                {
                    Models.veto veto  = Context.vetoes.FirstOrDefault(V => V.ID_veto==e.Case_ID);
                    dbExpert.Case_Number =veto.Veto_Number;
                }
                else if (e.Case_Degree == "استئناف")
                {
                    Models.Resumption resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Resumption == e.Case_ID);
                    dbExpert.Case_Number = resumption.Resumption_Number;
                }
                dbExperts.Add(dbExpert);
            }
            GridView_Expert.ItemsSource = dbExperts;
        }

        private void AddExpertBtn_Click(object sender, RoutedEventArgs e)
        {
            Expert.AddExpert addExpert = new AddExpert("add",-1);
            addExpert.ShowDialog();
        }

        private void GridView_Expert_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            dbExpert eXpert = (dbExpert)GridView_Expert.SelectedItem;
            if(eXpert==null)
            {
                return;
            }
            Expert.AddExpert addExpert = new AddExpert("update",eXpert.ID);
            addExpert.Name_Expert.Text = eXpert.Name;
            addExpert.Case_Number.Text = eXpert.Case_Number;
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
    public class dbExpert : Models.EXpert
    {
        public string Case_Number { get; set; }
    }
}

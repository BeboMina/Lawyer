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

namespace Lawyer.Staff
{
    /// <summary>
    /// Interaction logic for Staff.xaml
    /// </summary>
    public partial class Staff : Page
    {
        testEntities Context = new testEntities();
        List<Models.Stuff> Stuffs = new List<Stuff>();
        public Staff()
        {
            InitializeComponent();
            Stuffs = Context.Stuffs.ToList();
            GridView_Staff.ItemsSource = Stuffs;
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Models.Stuff> stuffs1;
                if (SearchTxt.Text == "")
                {
                    stuffs1 = Context.Stuffs.ToList();

                }
                else
                {
                    stuffs1 = Context.Stuffs.Where(C => C.Name.Contains(SearchTxt.Text)).ToList();
                }
                GridView_Staff.ItemsSource = stuffs1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void AddLawyerBtn_Click(object sender, RoutedEventArgs e)
        {
            AddLawyer addLawyer = new AddLawyer("add");
            addLawyer.ShowDialog();
        }

        private void UpdateLawyerBtn_Click(object sender, RoutedEventArgs e)
        {
            AddLawyer addLawyer = new AddLawyer("update");
            addLawyer.ShowDialog();
        }

        private void GridView_Staff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridView_Staff.SelectedItem == null)
                return;
            Models.Stuff stuff = (Models.Stuff)GridView_Staff.SelectedItem;
            DisplayStaff displayStaff = new DisplayStaff(stuff);
            displayStaff.ShowDialog();
        }
    }
}

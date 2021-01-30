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
        public Staff()
        {
            InitializeComponent();
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

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

            DisplayStaff displayStaff = new DisplayStaff();
            displayStaff.ShowDialog();
        }
    }
}

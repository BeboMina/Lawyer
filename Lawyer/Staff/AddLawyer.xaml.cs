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

namespace Lawyer.Staff
{
    /// <summary>
    /// Interaction logic for AddLawyer.xaml
    /// </summary>
    public partial class AddLawyer : Window
    {
        public AddLawyer(string action)
        {
            InitializeComponent();

            if(action == "update")
            {
                GboxHeader.Text = "تعديل موظف";
                Name_Lawyer.Visibility = Visibility.Collapsed;
                Name_Lawyer_combo.Visibility = Visibility.Visible;
            }
        }

        private void Name_Lawyer_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Lawyer_Save(object sender, RoutedEventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

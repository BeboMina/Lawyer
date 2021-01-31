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
        public AddExpert(string action)
        {
            InitializeComponent();

            if(action == "update")
            {
                GboxHeader.Text = "تعديل الخبير";
                Case_Number_combo.Visibility = Visibility.Collapsed;
                Case_Number.Visibility = Visibility.Visible;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Case_Number_combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Save_Expert(object sender, RoutedEventArgs e)
        {

        }
    }
}

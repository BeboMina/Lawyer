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

namespace Lawyer.Client
{
    /// <summary>
    /// Interaction logic for AddFees.xaml
    /// </summary>
    public partial class AddFees : Window
    {
        public AddFees()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Client_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

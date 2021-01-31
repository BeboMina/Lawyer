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
        public Experts()
        {
            InitializeComponent();
        }

        private void AddExpertBtn_Click(object sender, RoutedEventArgs e)
        {
            Expert.AddExpert addExpert = new AddExpert("add");
            addExpert.ShowDialog();
        }

        private void GridView_Expert_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Expert.AddExpert addExpert = new AddExpert("update");
            addExpert.ShowDialog();
        }
    }
}

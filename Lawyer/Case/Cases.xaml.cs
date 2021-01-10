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
using Lawyer.Models;
namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for Cases.xaml
    /// </summary>
    public partial class Cases : Page
    {
        testEntities1 Context = new testEntities1();
        List<Models.Case> cases;
        public Cases()
        {
            InitializeComponent();
            cases = Context.Cases.ToList();
            DataGrid_Cases.ItemsSource = cases;
        }

        private void AddCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCase addCase = new AddCase();
            addCase.ShowDialog();
        }

        private void AddProxyBtn_Click(object sender, RoutedEventArgs e)
        {
            AddSession addSession = new AddSession();
            addSession.ShowDialog();
        }

        private void DataGrid_Cases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Models.Case @case = (Models.Case)DataGrid_Cases.SelectedItem;
            if (@case == null)
                return;

            Case.DisplayCase displayCase = new DisplayCase();
            displayCase.ShowDialog();
        }
    }
}

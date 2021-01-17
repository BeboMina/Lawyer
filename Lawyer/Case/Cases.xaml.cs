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
        testEntities Context = new testEntities();
        List<Models.View_1> view_1s;
        public Cases()
        {
            InitializeComponent();
            view_1s = Context.View_1.ToList();
            DataGrid_Cases.ItemsSource = view_1s;
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
            Models.View_1 @case = (Models.View_1)DataGrid_Cases.SelectedItem;
            if (@case == null)
                return;
            Case.DisplayCase displayCase = new DisplayCase();
            Models.Case case1 = Context.Cases.FirstOrDefault(C => C.ID == @case.ID);
            displayCase.CLient_Name.Text = @case.Name;
            displayCase.Case_Number.Text = @case.ID.ToString();
            displayCase.Case_Type.Text= @case.Type;
            displayCase.C_Case.Text = case1.Circle;
            displayCase.ShowDialog();
        }
    }
}

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

namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for Cases.xaml
    /// </summary>
    public partial class Cases : Page
    {
        public Cases()
        {
            InitializeComponent();
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
    }
}

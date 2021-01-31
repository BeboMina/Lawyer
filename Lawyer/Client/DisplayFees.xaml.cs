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
using System.Windows.Shapes;

namespace Lawyer.Client
{
    /// <summary>
    /// Interaction logic for DisplayFees.xaml
    /// </summary>
    public partial class DisplayFees : Window
    {
        testEntities Context = new testEntities();
        List<Models.Fee> fees = new List<Fee>();
        public DisplayFees(string Id)
        {
            InitializeComponent();
            fees = Context.Fees.Where(F=>F.IDClient==Id).ToList();
            PaidGrid.ItemsSource = fees;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

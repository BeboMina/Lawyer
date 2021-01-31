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
    /// Interaction logic for DisplayStaff.xaml
    /// </summary>
    public partial class DisplayStaff : Window
    {
        public DisplayStaff()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask();
            addTask.ShowDialog();
        }
    }
}

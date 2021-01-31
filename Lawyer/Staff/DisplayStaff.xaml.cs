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

namespace Lawyer.Staff
{
    /// <summary>
    /// Interaction logic for DisplayStaff.xaml
    /// </summary>
    public partial class DisplayStaff : Window
    {
        testEntities Context = new testEntities();
        Models.Stuff stuff = new Stuff();
        List<Models.Task> tasks = new List<Models.Task>();
        public DisplayStaff(Models.Stuff stuff1)
        {
            InitializeComponent();
            try
            {
                stuff = stuff1;
                tasks = Context.Tasks.Where(T => T.Stuff_ID == stuff.ID).ToList();
                Name_Lawyer.Text = stuff.Name;
                GridView_Session.ItemsSource = tasks;
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask(stuff);
            addTask.ShowDialog();
        }
    }
}

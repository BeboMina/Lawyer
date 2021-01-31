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
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        testEntities Context = new testEntities();
        Models.Stuff stuff = new Stuff();
        List<Models.Task> tasks = new List<Models.Task>();
        public AddTask(Models.Stuff stuff1)
        {
            InitializeComponent();
            stuff = stuff1;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Task(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Task task = new Models.Task();
                string message = "تاكيد حفظ بيانات الموظف";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if(Task_Start.SelectedDate==null)
                {
                    MessageBox.Show("اختار تاريخ بداء المهمة");
                    return;
                }
                if(Task_End.SelectedDate==null)
                {
                    MessageBox.Show("اختار تاريخ الانتهاء من المهم");
                    return;
                }
                if (result == MessageBoxResult.Yes)
                {
                    task.Start_Date = Convert.ToDateTime(Task_Start.SelectedDate.Value);
                    task.End_Data= Convert.ToDateTime(Task_End.SelectedDate.Value);
                    task.Notes= new TextRange(Task_Lawyer.Document.ContentStart, Task_Lawyer.Document.ContentEnd).Text;
                    task.Stuff_ID = stuff.ID;
                    Context.Tasks.Add(task);
                    Context.SaveChanges();
                    Close();
                }
                //MainWindow parent = (MainWindow)App.Current.MainWindow;
                //parent.main.Navigate(new DisplayStaff(stuff));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

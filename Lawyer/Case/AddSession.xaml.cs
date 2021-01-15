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
using Lawyer.Models;

namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for AddSession.xaml
    /// </summary>
    public partial class AddSession : Window
    {
        testEntities Context = new testEntities();
        List<long> Num_Cases;
        long ID_Case;
        
        public AddSession()
        {
            InitializeComponent();
            Num_Cases = Context.Cases.Select(C => C.ID).ToList();
            Com_Num_Case.ItemsSource = Num_Cases;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Com_Num_Case_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ID_Case =Convert.ToInt64(Com_Num_Case.SelectedItem);
            var IDCli = Context.Client_Case.Where(Ci=>Ci.IDCase==ID_Case).Select(C=>C.IDClient).ToList();
            string ID = IDCli[0];
            Models.Client nameClient = Context.Clients.FirstOrDefault(C => C.ID == ID);
            Client_Name.Text = nameClient.Name;
            ID_Client.Text = nameClient.ID;
        }
    }
}

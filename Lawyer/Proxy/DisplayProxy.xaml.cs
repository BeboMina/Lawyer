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

namespace Lawyer.Proxy
{
    /// <summary>
    /// Interaction logic for DisplayProxy.xaml
    /// </summary>
    public partial class DisplayProxy : Window
    {
        testEntities Context = new testEntities();
        public DisplayProxy(Models.Procuration procuration)
        {
            InitializeComponent();
            Models.Client client = Context.Clients.FirstOrDefault(C => C.IDProcuration == procuration.ID);
            Models.FilesProcuration filesProcuration = Context.FilesProcurations.FirstOrDefault(F => F.IDProcuration == procuration.ID);
            Client_Name.Text = client.Name;
            Client_Code.Text = client.ID;
            if(procuration.StardDate==null)
            {
                Date_Start.Text = "";
            }
            else
            {
                Date_Start.Text = procuration.StardDate.Value.ToShortDateString();
            }
            if (procuration.EndDate == null)
            {
                Date_End.Text = "";
            }
            else
            {
                Date_End.Text = procuration.EndDate.Value.ToShortDateString();
            }
            if(procuration.certified==true)
            {
                Done.Text = "موثق";
            }
            else
            {
                Done.Text = "غير موثق";
            }
            if(filesProcuration!=null)
            {
                List<Name_File> name_Files = new List<Name_File>();
                Name_File Name_File = new Name_File();
                Name_File.Name = filesProcuration.Title + filesProcuration.Extantion;
                name_Files.Add(Name_File);
                Grid_Files.ItemsSource = name_Files;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    public class Name_File
    {
        public string Name { get; set; }
    }
}

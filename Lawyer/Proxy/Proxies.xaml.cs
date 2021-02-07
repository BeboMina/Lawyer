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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lawyer.Proxy
{
    /// <summary>
    /// Interaction logic for Proxies.xaml
    /// </summary>
    public partial class Proxies : Page
    {
        testEntities Context = new testEntities();
        List<Models.ViewClient_Pro> viewClient_Pros;
        List<Fill_Data> fill_Datas = new List<Fill_Data>();
        public Proxies()
        {
            InitializeComponent();
            viewClient_Pros= Context.ViewClient_Pro.ToList();
            foreach(var item in viewClient_Pros)
            {
                Fill_Data fill_Data = new Fill_Data();
                Models.Client Cli_ID = Context.Clients.FirstOrDefault(C => C.IDProcuration == item.ID); 
                fill_Data.ID = item.ID;
                fill_Data.Client_ID = Cli_ID.ID;
                fill_Data.Client_Name = Cli_ID.Name;
                fill_Data.StardDate = (item.StardDate!= null) ? item.StardDate.Value.ToShortDateString(): "";
                fill_Data.EndDate = (item.EndDate != null)? item.EndDate.Value.ToShortDateString():"";
                if(item.certified==true)
                {
                    fill_Data.certified = "موثق";
                }
                else
                {
                    fill_Data.certified = "غير موثق";
                }
                fill_Datas.Add(fill_Data);
            }
            DataGrid_Proxies.ItemsSource = fill_Datas;

        }

        private void AddProxyBtn_Click(object sender, RoutedEventArgs e)
        {
            Proxy.AddProxy addProxy = new AddProxy("proxy");
            addProxy.ShowDialog();
        }

        private void DataGrid_Proxies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGrid_Proxies.SelectedItem == null)
                return;

            Fill_Data fill_Data= (Fill_Data)DataGrid_Proxies.SelectedItem;
            Models.Procuration procuration = Context.Procurations.FirstOrDefault(P=>P.ID== fill_Data.ID);
            Proxy.DisplayProxy displayProxy = new DisplayProxy(procuration);
            displayProxy.ShowDialog();
        }
    }
    public class Fill_Data
    {
        public long ID  { get; set; }
        public string Client_ID { get; set; }
        public string  Client_Name{ get; set; }
        public string StardDate { get; set; }
        public string EndDate { get; set; }
        public string certified { get; set; }
    }
}

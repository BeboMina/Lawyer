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
        public Proxies()
        {
            InitializeComponent();
            viewClient_Pros= Context.ViewClient_Pro.ToList();
            DataGrid_Proxies.ItemsSource = viewClient_Pros;
        }

        private void AddProxyBtn_Click(object sender, RoutedEventArgs e)
        {
            Proxy.AddProxy addProxy = new AddProxy("proxy");
            addProxy.ShowDialog();
        }

        private void DataGrid_Proxies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Proxy.DisplayProxy displayProxy = new DisplayProxy();
            displayProxy.ShowDialog();
        }
    }
}

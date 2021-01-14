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
    /// Interaction logic for AddProxy.xaml
    /// </summary>
    public partial class AddProxy : Window
    {
        string action;

        public AddProxy(string act)
        {
            InitializeComponent();

            action = act;

            if(action == "update")
            {
                GboxHeader.Text = "تعديل توكيل";
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

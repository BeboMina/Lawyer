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

namespace Lawyer
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MainWindow main;
        public bool login = false;

        public Login(MainWindow window)
        {
            InitializeComponent();

            main = window;
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            login = true;
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if(main == null)
            {
                this.Close();
            }
            else
            {
                this.Close();
                main.Close();
            }
        }
    }
}

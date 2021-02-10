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

namespace Lawyer
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MainWindow main;
        testEntities Context = new testEntities();
        List<Models.Register> registers = new List<Register>();
        public Login(MainWindow window)
        {
            InitializeComponent();

            main = window;
            registers = Context.Registers.ToList();
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {

            if(Button2.IsChecked==true)
            {
                if(UserName.Text != registers[1].UserName || Password.Password != registers[1].password)
                {
                    MessageBox.Show("Wrong User Name or Password!");
                    return;
                }
            }
            else if(Button1.IsChecked==true)
            {
                if (UserName.Text != registers[0].UserName || Password.Password != registers[0].password)
                {
                    MessageBox.Show("Wrong User Name or Password!");
                    return;
                }

                main.isUser = true;
            }
            else
            {
                MessageBox.Show(" admin او user اختار اذ كنت");
                return;
            }

            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            main.Close();
        }
    }
}

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

namespace Lawyer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_Clients.Visibility = Visibility.Collapsed;
                tt_Cases.Visibility = Visibility.Collapsed;
                tt_Lawyers.Visibility = Visibility.Collapsed;
                tt_proxies.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_Clients.Visibility = Visibility.Visible;
                tt_Cases.Visibility = Visibility.Visible;
                tt_Lawyers.Visibility = Visibility.Visible;
                tt_proxies.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            BG.Opacity = 1;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            BG.Opacity = 0.3;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tg_Btn.IsChecked = false;
            main.Content = null;
            TitleGrid.Visibility = Visibility.Visible;

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemClients":
                    main.Navigate(new Clients());
                    TitleTxt.Text = "العملاء";
                    break;

                case "ItemCases":
                    main.Navigate(new Case.Cases());
                    TitleTxt.Text = "الدعاوي";
                    break;

                case "ItemLawyers":
                    //usc = new Clients();
                    //gridMain.Children.Add(usc);
                    TitleTxt.Text = "المحامين";
                    break;

                case "ItemProxies":
                    //usc = new Clients();
                    //gridMain.Children.Add(usc);
                    TitleTxt.Text = "التوكيلات";
                    break;

                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = SystemParameters.WorkArea.Left;
            this.Top = SystemParameters.WorkArea.Top;
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width;
        }
    }
}

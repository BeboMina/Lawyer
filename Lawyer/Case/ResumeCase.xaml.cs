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

namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for ResumeCase.xaml
    /// </summary>
    public partial class ResumeCase : Window
    {
        string type;

        public ResumeCase(string t)
        {
            InitializeComponent();

            type = t;

            if(type == "نقض")
            {
                GboxHeader.Text = "نقض";
                Veto_Number_TxtBlock.Text = "رقم النقض";
                Add_Veto.Content = "اضافة نقض";
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Veto_Click(object sender, RoutedEventArgs e)
        {
            if(((Button)sender).Content.ToString() == "اضافة")
            {
                Res_Num.IsReadOnly = false;
                Res_Num.Focus();
                Add_Veto.Content = "حفظ";
                Notes.IsReadOnly = false;
                Notes.Background = Brushes.White;
            }
            else
            {
                Res_Num.IsReadOnly = true;
                Add_Veto.Visibility = Visibility.Collapsed;
                Notes.IsReadOnly = true;
                Notes.Background = (Brush)(new BrushConverter().ConvertFrom("#FFC5CBF9"));
            }

        }
    }
}

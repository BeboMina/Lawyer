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

namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for Judgement.xaml
    /// </summary>
    public partial class Judgement : Window
    {
        List<string> Number_of_Cases = new List<string>();
        List<long> ID_of_Cases = new List<long>();
        testEntities Context = new testEntities();
        int index = -1;
        long NumOFCase=0;
        
        public Judgement(string Number_of_case,long ID)
        {
            InitializeComponent();
            long numberofcase = ID;
            ID_of_Cases.Add(numberofcase);
            Number_of_Cases.Add(Number_of_case);
            Models.veto veto = Context.vetoes.FirstOrDefault(V => V.ID_Case == numberofcase);
            Models.Resumption resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Case == numberofcase);
            if(veto!=null)
            {
                Number_of_Cases.Add(veto.Veto_Number);
                ID_of_Cases.Add(veto.ID_veto);
            }
            if(resumption!=null)
            {
                Number_of_Cases.Add(resumption.Resumption_Number);
                ID_of_Cases.Add(resumption.ID_Resumption);
            }
            Number_Case.ItemsSource = Number_of_Cases;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (index == -1)
                {
                    MessageBox.Show("اختر رقم الدعوة");
                }
                else if (data_Case.Text.Length == 0)
                {
                    MessageBox.Show("ادخل تاريخ الحكم  ");
                }
                else if (Done.IsChecked == false && Dont_Done.IsChecked == false)
                {
                    MessageBox.Show("هل تم الاعلان ام لم يتم ");
                }
                else
                {
                    string message = "تاكيد حفظ بيانات الحكم";
                    string title = "حفظ";
                    MessageBoxButton buttons = MessageBoxButton.YesNo;
                    MessageBoxResult result = System.Windows.MessageBox.Show(message, title, buttons);
                    if (result == MessageBoxResult.Yes)
                    {
                        Models.Jadge jadge = new Jadge();
                        jadge.Date = Convert.ToDateTime(data_Case.SelectedDate.Value);
                        jadge.Execute = false;
                        jadge.Notes = JadgNots.Text;
                        if (Done.IsChecked == true)
                        {
                            jadge.Done = true;
                        }
                        else 
                        {
                            jadge.Done = false;
                        }
                       
                        Context.Jadges.Add(jadge);
                        Context.SaveChanges();
                        Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID == NumOFCase);
                        Models.veto veto = Context.vetoes.FirstOrDefault(V => V.ID_veto == NumOFCase);
                        Models.Resumption resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Resumption == NumOFCase);
                        if (index==0&&@case != null)
                        {
                            @case.ID_jadge = jadge.ID;
                            Context.SaveChanges();
                            
                        }
                        else if (index==1&&veto != null)
                        {
                            veto.ID_Jadge = jadge.ID;
                            Context.SaveChanges();
                            
                        }
                        else if (index==2&&resumption != null)
                        {
                            resumption.ID_Jadge = jadge.ID;
                            Context.SaveChanges();
                            
                        }
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Number_Case_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = Number_Case.SelectedIndex;
            NumOFCase = ID_of_Cases[index];
        }
    }
}

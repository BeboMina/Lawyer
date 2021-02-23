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
    /// Interaction logic for ExecuteJudgement.xaml
    /// </summary>
    public partial class ExecuteJudgement : Window
    {
        testEntities Context = new testEntities();
        long Case_ID;
        string Type;
        Models.Case @case;
        Models.veto veto;
        Models.Resumption resumption;
        bool donExexcute = false;
        bool update = false;
        long Id_exe;
        public ExecuteJudgement(long id,string type,long ID_Exe)
        {
            InitializeComponent();
            Type = type;
            Case_ID = id;
            if(Type== "ابتدائية")
            {
                @case = Context.Cases.FirstOrDefault(C => C.ID == Case_ID);
                Number_Case.Text = @case.Case_Namber;
                GetClientName(@case);
            }
            else if(Type == "استئناف")
            {
                veto = Context.vetoes.FirstOrDefault(V => V.ID_Case == Case_ID);
                Models.Case case2= Context.Cases.FirstOrDefault(C => C.ID == veto.ID_Case);
                Number_Case.Text =veto.Veto_Number;
                GetClientName(case2);
            }
            else if(Type == "نقض")
            {
                resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Case == Case_ID);
                Models.Case case2 = Context.Cases.FirstOrDefault(C => C.ID == resumption.ID_Case);
                Number_Case.Text = resumption.Resumption_Number;
                GetClientName(case2);
            }
            if(ID_Exe!=-1)
            {
                Id_exe = ID_Exe;
                update = true;
                Models.Execute execute = Context.Executes.FirstOrDefault(E => E.ID == ID_Exe);
                if(execute!=null)
                {
                    Number_Execute.Text = execute.Execute_Number;
                    Text_Execute.Text = execute.Notes;
                    Type_Execute.Text = execute.Execute_Type;
                    if(execute.Done_Date!=null)
                    {
                        date_Execute.SelectedDate = execute.Execut_Date.Value;
                    }
                    if(execute.Execut_Date!=null)
                    {
                        date_Inform.SelectedDate= execute.Done_Date.Value;
                    }
                    if(execute.Done==true)
                    {
                        Done.IsChecked = true;
                    }
                    else
                    {
                        Dont_Done.IsChecked = true;
                    }
                    donExexcute = execute.Execute1.Value;
                }
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Number_Execute.Text))
            {
                MessageBox.Show("يجب ادخال رقم التنفيذ");
                return;
            }

            try
            {
                string message = "تاكيد حفظ بيانات التنفيذ ";
                string title = "حفظ";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {
                    if (update == false)
                    {
                        Models.Execute execute = new Execute();
                        execute.Execute1 = donExexcute;
                        execute.Case_Type = Type;
                        execute.Execute_Number = Number_Execute.Text;
                        execute.Execute_Type = Type_Execute.Text;
                        execute.Notes = Text_Execute.Text;
                        if (date_Execute.SelectedDate != null)
                        {
                            execute.Execut_Date = date_Execute.SelectedDate.Value;
                        }
                        if (Done.IsChecked == true)
                        {
                            execute.Done = true;
                            if (date_Inform.SelectedDate == null)
                            {
                                MessageBox.Show("يجب ادخال تاريخ الاعلان");
                                return;
                            }
                            else
                            {
                                execute.Done_Date = date_Inform.SelectedDate.Value;
                            }
                        }
                        else
                        {
                            execute.Done = false;
                        }
                        Context.Executes.Add(execute);
                        Context.SaveChanges();
                        if (Type == "ابتدائية")
                        {
                            @case.ID_Execute = execute.ID;
                        }
                        else if (Type == "استئناف")
                        {
                            veto.ID_Execute = execute.ID;
                        }
                        else if (Type == "نقض")
                        {
                            resumption.ID_Execute = execute.ID;
                        }
                        Context.SaveChanges();
                        Close();
                    }
                    else
                    {
                        Models.Execute execute = Context.Executes.FirstOrDefault(E => E.ID == Id_exe);
                        if (execute != null)
                        {
                            execute.Execute1 = donExexcute;
                            execute.Case_Type = Type;
                            execute.Execute_Number = Number_Execute.Text;
                            execute.Execute_Type = Type_Execute.Text;
                            execute.Notes = Text_Execute.Text;
                            if (date_Execute.SelectedDate != null)
                            {
                                execute.Execut_Date = date_Execute.SelectedDate.Value;
                            }
                            if (Done.IsChecked == true)
                            {
                                execute.Done = true;
                                if (date_Inform.SelectedDate == null)
                                {
                                    MessageBox.Show("يجب ادخال تاريخ الاعلان");
                                    return;
                                }
                                else
                                {
                                    execute.Done_Date = date_Inform.SelectedDate.Value;
                                }
                            }
                            else
                            {
                                execute.Done = false;
                            }
                            Context.SaveChanges();
                            Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void GetClientName(Models.Case case1)
        {
            Models.Client_Case client_Case = Context.Client_Case.FirstOrDefault(C => C.IDCase == case1.ID);
            if(client_Case!=null)
            {
                Models.Client client = Context.Clients.FirstOrDefault(C => C.ID == client_Case.IDClient);
                Client_Name.Text = client.Name;
            }
        }
    }
}

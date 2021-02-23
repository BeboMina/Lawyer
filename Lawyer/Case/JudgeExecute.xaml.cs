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

namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for JudgeExecute.xaml
    /// </summary>
    public partial class JudgeExecute : Page
    {
        testEntities Context = new testEntities();
        List<Models.Jadge> jadges = new List<Jadge>();
        List<ViewJadge> viewJadges = new List<ViewJadge>();
        List<Models.Execute> executes = new List<Execute>();
        

        public JudgeExecute()
        {
            InitializeComponent();
            jadges = Context.Jadges.ToList();
            FillJudgeGrid();
            executes = Context.Executes.ToList();
            FillGridExecute();
        }

        public void FillJudgeGrid()
        {
             
            try
            {
                foreach (var item in jadges)
                {
                    string Num = "";
                    long ID = 0;
                    Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID_jadge == item.ID);
                    Models.veto veto = Context.vetoes.FirstOrDefault(V => V.ID_Jadge == item.ID);
                    Models.Resumption resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Jadge == item.ID);
                    if (@case != null)
                    {
                        Num = @case.Case_Namber;
                        ID = @case.ID;
                    }
                    else if (veto != null)
                    {
                        Num = veto.Veto_Number;
                        Models.Case @case1 = Context.Cases.FirstOrDefault(C => C.ID == veto.ID_Case);
                        ID = @case1.ID;
                    }
                    else if (resumption != null)
                    {
                        Num = resumption.Resumption_Number;
                        Models.Case @case1 = Context.Cases.FirstOrDefault(C => C.ID == resumption.ID_Case);
                        ID = @case1.ID;
                    }

                    Models.Client_Case client_Case = Context.Client_Case.FirstOrDefault(C => C.IDCase == ID);
                    if (client_Case != null)
                    {
                        Models.Client client = Context.Clients.FirstOrDefault(C => C.ID == client_Case.IDClient);
                        ViewJadge viewJadge = new ViewJadge();
                        viewJadge.ID = item.ID;
                        viewJadge.Judgement = item.Notes;
                        viewJadge.NameClient = client.Name;
                        viewJadge.NumberCase = Num;
                        viewJadge.Excute = item.Execute.Value ? "تم الاعلان" : " لم يتم الاعلان";
                        viewJadge.Date = (item.Date.Value != null) ? DateTime.Parse(item.Date.ToString()).ToString("dd/MM/yyyy") : "";
                        viewJadges.Add(viewJadge);
                    }
                    GridView_Judgement.ItemsSource = viewJadges;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void FillGridExecute()
        {
            
            try
            {
                List<ViewExecute> viewExecutes = new List<ViewExecute>();
                foreach (var item in executes)
                {
                    string Num = "";
                    long ID = 0;
                    
                    
                    if(item.Case_Type== "ابتدائية")
                    {
                        Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID_Execute == item.ID);
                        Num = @case.Case_Namber;
                        ID = @case.ID;
                    }
                    else if(item.Case_Type == "استئناف")
                    {
                        Models.veto veto = Context.vetoes.FirstOrDefault(V => V.ID_Execute == item.ID);
                        Num = veto.Veto_Number;
                        Models.Case @case1 = Context.Cases.FirstOrDefault(C => C.ID == veto.ID_Case);
                        ID = @case1.ID;
                    }
                    else if (item.Case_Type == "نقض")
                    {
                        Models.Resumption resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Execute == item.ID);
                        Num = resumption.Resumption_Number;
                        Models.Case @case1 = Context.Cases.FirstOrDefault(C => C.ID == resumption.ID_Case);
                        ID = @case1.ID;
                    }
                    Models.Client_Case client_Case = Context.Client_Case.FirstOrDefault(C => C.IDCase == ID);
                    if (client_Case != null)
                    {
                        Models.Client client = Context.Clients.FirstOrDefault(C => C.ID == client_Case.IDClient);
                        ViewExecute viewExecute = new ViewExecute();
                        viewExecute.ID = item.ID;
                        viewExecute.Case_ID = ID;
                        viewExecute.Execution = item.Notes;
                        viewExecute.NameClient = client.Name;
                        viewExecute.NumberCase = Num;
                        viewExecute.IsExecuted = item.Execute1.Value ? "تم التنفيذ" : " لم يتم التنفيذ";
                        viewExecute.Execute_Type = item.Execute_Type;
                        viewExecute.Case_Type = item.Case_Type;
                        viewExecute.IsInformed=item.Done.Value? "تم الاعلان" : " لم يتم الاعلان";
                        viewExecute.Execut_Number = item.Execute_Number;
                        viewExecutes.Add(viewExecute);
                    }
                }
                GridView_Execution.ItemsSource = viewExecutes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchCaseTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
           // string NumCase = SearchCaseTxt.Text;
           // viewJadges.Contains(NumCase);
        }

        private void GridView_Execution_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridView_Execution.SelectedItem == null)
                return;
            ViewExecute viewExecute1 = (ViewExecute)GridView_Execution.SelectedItem;
            Case.ExecuteJudgement executeJudgement = new ExecuteJudgement(viewExecute1.Case_ID, viewExecute1.Case_Type, viewExecute1.ID);
            executeJudgement.ShowDialog();
        }

        private void SearchExecuteTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SearchExecuteTxt.Text == "")
                {

                    executes = Context.Executes.ToList();
                    FillGridExecute();

                }
                else
                {
                    string Num = SearchExecuteTxt.Text;
                    executes = Context.Executes.Where(E => E.Execute_Number.Contains(Num)).ToList();
                    FillGridExecute();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    public class ViewJadge
    {
        public long ID { get; set; }
        public string NameClient { get; set; }
        public string NumberCase { get; set; }
        public string Judgement { get; set; }
        public string Excute { get; set; }
        public string Date { get; set; }
    }
    public class ViewExecute
    {
        public long ID { get; set; }
        public string NameClient { get; set; }
        public string NumberCase { get; set; }
        public string Execution { get; set; }
        public string Execute_Type { get; set; }
        public string Execut_Number { get; set; }
        public string IsExecuted { get; set; }
        public string IsInformed { get; set; }
        public string Case_Type { get; set; }
        public long Case_ID { get; set; }
    }
}

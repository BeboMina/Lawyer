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

        public JudgeExecute()
        {
            InitializeComponent();
        }

        public void FillJudgeGrid(string case_nymber = "")
        {
            if (case_nymber == "")
            {
                jadges = Context.Jadges.ToList();
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
                        viewJadge.Excute = item.Execute.Value ? "تم" : "لا يتم";
                        viewJadge.Date = (item.Date.Value != null) ? DateTime.Parse(item.Date.ToString()).ToString("dd/MM/yyyy") : "";
                        viewJadges.Add(viewJadge);
                    }
                    GridView_Judgement.ItemsSource = viewJadges;
                }
            }
            else
            {

            }
        }

        private void SearchCaseTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GridView_Execution_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridView_Execution.SelectedItem == null)
                return;

            Case.ExecuteJudgement executeJudgement = new ExecuteJudgement();
            executeJudgement.ShowDialog();
        }

        private void SearchExecuteTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

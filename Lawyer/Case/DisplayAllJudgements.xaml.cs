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
    /// Interaction logic for DisplayAllJudgements.xaml
    /// </summary>
    public partial class DisplayAllJudgements : Window
    {
        testEntities Context = new testEntities();
        List<Models.Jadge> jadges = new List<Jadge>();
        List<ViewJadge> viewJadges = new List<ViewJadge>();
        public DisplayAllJudgements()
        {
            InitializeComponent();
            jadges = Context.Jadges.ToList();
            foreach(var item in jadges)
            {
                long Num=0,ID=0;
                Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID_jadge == item.ID);
                Models.veto veto = Context.vetoes.FirstOrDefault(V => V.ID_Jadge == item.ID);
                Models.Resumption resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Jadge == item.ID);
                if(@case!=null)
                {
                    Num = @case.ID;
                    ID = @case.ID;
                }
                else if(veto != null)
                {
                    Num = veto.ID_veto;
                    Models.Case @case1 = Context.Cases.FirstOrDefault(C => C.ID == veto.ID_Case);
                    ID = @case1.ID;
                }
                else if(resumption!=null)
                {
                    Num = resumption.ID_Resumption;
                    Models.Case @case1 = Context.Cases.FirstOrDefault(C => C.ID == resumption.ID_Case);
                    ID = @case1.ID;
                }

                Models.Client_Case client_Case = Context.Client_Case.FirstOrDefault(C => C.IDCase == ID);
                if(client_Case!=null)
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
                JudgmentsGrid.ItemsSource = viewJadges;
            }
            
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    public class ViewJadge
    {
        public long ID { get; set; }
        public string NameClient { get; set; }
        public long NumberCase { get; set; }
        public string Judgement { get; set; }
        public string Excute { get; set; }
        public string Date { get; set; }
    }
}

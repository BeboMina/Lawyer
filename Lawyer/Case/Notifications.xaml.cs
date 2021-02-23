using Lawyer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : Window
    {
        public ObservableCollection<SessionNotification> sessionNotifications;
        
        public ObservableCollection<JudgementNotification> judgementNotifications;
        
        public ObservableCollection<ExecutionNotification> executionNotifications;

        testEntities Context = new testEntities();
        List<Models.Session> sessions = new List<Models.Session>();
        List<Models.Jadge> jadges = new List<Jadge>();
        List<Models.Execute> executes = new List<Execute>();
        string Now_Date = DateTime.Now.ToShortDateString();
        public Notifications()
        {
            InitializeComponent();

            sessionNotifications = new ObservableCollection<SessionNotification>();
            judgementNotifications = new ObservableCollection<JudgementNotification>();
            executionNotifications = new ObservableCollection<ExecutionNotification>();

            sessions = Context.Sessions.Where(S=>S.NextDate.Value.Year==DateTime.Now.Year).ToList();
            foreach(var item in sessions)
            {
                if (item.NextDate != null)
                {

                    string Next_Date = item.NextDate.Value.ToShortDateString();

                    if (Next_Date == Now_Date)
                    {

                        Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID == item.IDCase);
                        Models.veto veto = Context.vetoes.FirstOrDefault(V => V.ID_veto == item.IDCase);
                        Models.Resumption resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Resumption == item.IDCase);
                        if (@case != null)
                        {
                            FillListSession(item, @case.Circle, @case.ID,@case.Case_Namber);
                        }
                        else if (veto != null)
                        {
                            FillListSession(item, veto.Circle, (long)veto.ID_Case,veto.Veto_Number);
                        }
                        else if (resumption != null)
                        {
                            FillListSession(item, resumption.Circle, (long)resumption.ID_Case,resumption.Resumption_Number);
                        }
                    }
                }
            }
            jadges = Context.Jadges.Where(J => J.Execute == false).ToList();
            foreach(var item in jadges)
            {
                DateTime Date_Jadge = item.Date.Value.AddDays(14);
                int result = DateTime.Compare(Date_Jadge, DateTime.Now.Date);
                if (result<=0)
                {
                    
                    Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID_jadge == item.ID);
                    Models.veto veto = Context.vetoes.FirstOrDefault(V => V.ID_Jadge == item.ID);
                    Models.Resumption resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Jadge == item.ID);
                    if (@case != null)
                    {
                        FillJudgementData(@case.ID, "ابتدائية",item,@case.Case_Namber);
                    }
                    else if (veto != null)
                    {

                        @case = Context.Cases.FirstOrDefault(C => C.ID == veto.ID_Case);
                        FillJudgementData(@case.ID, "استئناف", item,veto.Veto_Number, veto.ID_veto);

                    }
                    else if (resumption != null)
                    {
                        @case = Context.Cases.FirstOrDefault(C => C.ID == resumption.ID_Case);
                        FillJudgementData(@case.ID, "نقض", item,resumption.Resumption_Number, resumption.ID_Resumption);
                    }
                }
            }
            executes = Context.Executes.Where(E => E.Done == true && E.Execute1 == false).ToList();
            foreach(var item in executes)
            {
                DateTime Date_Execute = item.Done_Date.Value.AddDays(14);
                int result = DateTime.Compare(Date_Execute, DateTime.Now.Date);
                if (result <= 0)
                {
                    if (item.Case_Type == "ابتدائية")
                    {
                        Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID_Execute == item.ID);
                        FillExecuteData(@case.ID, "ابتدائية", item, @case.Case_Namber);
                    }
                    else if (item.Case_Type == "استئناف")
                    {
                        Models.veto veto = Context.vetoes.FirstOrDefault(V => V.ID_Execute == item.ID);
                        Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID == veto.ID_Case);
                        FillExecuteData(@case.ID, "استئناف", item, veto.Veto_Number, veto.ID_veto);
                    }
                    else if (item.Case_Type == "نقض")
                    {
                        Models.Resumption resumption = Context.Resumptions.FirstOrDefault(R => R.ID_Execute == item.ID);
                        Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID == resumption.ID_Case);
                        FillExecuteData(@case.ID, "نقض", item, resumption.Resumption_Number, resumption.ID_Resumption);
                    }
                }
            }
            SessionsList.ItemsSource = sessionNotifications;
            JudgementList.ItemsSource = judgementNotifications;
            ExecutionList.ItemsSource = executionNotifications;

        }

        private void FillListSession(Models.Session sess,string circle,long Case_ID,string Case_Num)
        {
            SessionNotification sessionNotification = new SessionNotification();
            Models.Client_Case client_Case = Context.Client_Case.FirstOrDefault(C => C.IDCase == Case_ID);
            if (client_Case != null)
            {
                Models.Client client;
                client = Context.Clients.FirstOrDefault(C => C.ID == client_Case.IDClient);
                sessionNotification.Case_Number = Case_Num;
                sessionNotification.Circle = circle;
                sessionNotification.Client_Name = client.Name;
                sessionNotification.Court = sess.Jadge;
                sessionNotification.Next_Date = sess.NextDate.Value;
                sessionNotification.Time = sess.Timer;
                sessionNotifications.Add(sessionNotification);
            }
        }

        private void FillJudgementData(long ID_Case, string Type_Case ,Models.Jadge item,string Number_case, long Num = -1)
        {
            JudgementNotification judgementNotification = new JudgementNotification();
            Models.Client_Case client_Case = Context.Client_Case.FirstOrDefault(C => C.IDCase == ID_Case);
            Models.Client client;
            if (client_Case != null)
            {
                client = Context.Clients.FirstOrDefault(C => C.ID == client_Case.IDClient);
                judgementNotification.Client_Name = client.Name;
                judgementNotification.Case_Number = Number_case; //Num == -1 ? Num_Case : Num;
                judgementNotification.Case_ID = Num == -1 ? ID_Case : Num;
                judgementNotification.Date = item.Date.Value;
                judgementNotification.Is_Done = item.Done.Value;
                judgementNotification.Judgement = item.Notes;
                judgementNotification.Case_Judgement = ID_Case.ToString();

                judgementNotification.Case_Judgement += ' ' + Type_Case;
                judgementNotification.Case_Judgement += ' ' + item.ID.ToString();

                judgementNotifications.Add(judgementNotification);
            }

        }
        private void FillExecuteData(long ID_Case, string Type_Case, Models.Execute execute, string Number_case, long Num = -1)
        {
            ExecutionNotification executionNotification = new ExecutionNotification();
            Models.Client_Case client_Case = Context.Client_Case.FirstOrDefault(C => C.IDCase == ID_Case);
            Models.Client client;
            if (client_Case != null)
            {
                client = Context.Clients.FirstOrDefault(C => C.ID == client_Case.IDClient);
                executionNotification.Client_Name = client.Name;
                executionNotification.Case_Number = Number_case;
                executionNotification.Execute_Number = execute.Execute_Number;
                executionNotification.Execute_ID = execute.ID;
                if(execute.Execut_Date!=null)
                {
                    executionNotification.Date = execute.Execut_Date.Value.ToShortDateString();
                }
                else
                {
                    executionNotification.Date = "";
                }
                executionNotification.Is_Done = execute.Done.Value;
                executionNotification.Execution = execute.Notes;
                executionNotifications.Add(executionNotification);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Inform_Client_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            long execute_id = long.Parse(((CheckBox)sender).Tag.ToString());
            string message = "تاكيد البدء فى اجراءات الاعلان ";
            string title = "حفظ";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);
            if (result == MessageBoxResult.Yes)
            {
                Models.Execute execute = Context.Executes.FirstOrDefault(J => J.ID == execute_id);
                execute.Execute1 = true;
                ((CheckBox)sender).IsEnabled = false;
                Context.SaveChanges();

            }
            else
            {
                ((CheckBox)sender).IsChecked = false;
            }

        }

        public class SessionNotification
        {
            public string Client_Name { get; set; }
            public string Case_Number { get; set; }
            public string Circle { get; set; }
            public string Court { get; set; }
            public DateTime Next_Date { get; set; }
            public string Time { get; set; }
        }

        public class JudgementNotification
        {
            public long Case_ID { get; set; }
            public string Client_Name { get; set; }
            public string Case_Number { get; set; }
            public string Case_Judgement { get; set; }
            public DateTime Date { get; set; }
            public string Judgement { get; set; }
            public bool Is_Done { get; set; }
        }

        public class ExecutionNotification
        {
            public string Client_Name { get; set; }
            public string Case_Number { get; set; }
            public string Execute_Number { get; set; }
            public string Date { get; set; }
            public string Execution { get; set; }
            public long Execute_ID { get; set; }
            public bool Is_Done { get; set; }
        }

        private void Start_Execute_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            string Case_Judgement = ((CheckBox)sender).Tag.ToString();

            string[] case_judgement = Case_Judgement.Split();
            long case_id = long.Parse(case_judgement[0]);
            string type = case_judgement[1];
            long judgement_id = long.Parse(case_judgement[2]);

            string message = "تاكيد البدء فى اجراءات الاعلان ";
            string title = "حفظ";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);
            if (result == MessageBoxResult.Yes)
            {
                Models.Jadge jadge = Context.Jadges.FirstOrDefault(J => J.ID == judgement_id);
                jadge.Execute = true;
                Context.SaveChanges();

                ((CheckBox)sender).IsEnabled = false;
                Case.ExecuteJudgement executeJudgement = new ExecuteJudgement(case_id, type,-1);
                executeJudgement.ShowDialog();
            }
            else
            {
                ((CheckBox)sender).IsChecked = false;
            }
        }
    }
}

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
        
        public ObservableCollection<AnnounceNotification> announceNotifications;

        testEntities Context = new testEntities();
        List<Models.Session> sessions = new List<Models.Session>();
        List<Models.Jadge> jadges = new List<Jadge>();
        string Now_Date = DateTime.Now.ToShortDateString();
        public Notifications()
        {
            InitializeComponent();

            sessionNotifications = new ObservableCollection<SessionNotification>();
            announceNotifications = new ObservableCollection<AnnounceNotification>();


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
                        FillData(@case.ID, item);
                    }
                    else if (veto != null)
                    {

                        @case = Context.Cases.FirstOrDefault(C => C.ID == veto.ID_Case);
                        FillData(@case.ID, item, veto.ID_veto);

                    }
                    else if (resumption != null)
                    {
                        @case = Context.Cases.FirstOrDefault(C => C.ID == resumption.ID_Case);
                        FillData(@case.ID, item, resumption.ID_Resumption);
                    }
                }
            }
            SessionsList.ItemsSource = sessionNotifications;
            AnnouncesList.ItemsSource = announceNotifications;

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

        private void FillData(long Num_Case,Models.Jadge item,long Num=-1)
        {
            AnnounceNotification announceNotification = new AnnounceNotification();
            Models.Client_Case client_Case = Context.Client_Case.FirstOrDefault(C => C.IDCase == Num_Case);
            Models.Client client;
            if (client_Case != null)
            {
                client = Context.Clients.FirstOrDefault(C => C.ID == client_Case.IDClient);
                announceNotification.Client_Name = client.Name;
                announceNotification.Case_Number = Num == -1 ? Num_Case : Num;
                announceNotification.Date = item.Date.Value;
                announceNotification.Is_Done = item.Done.Value;
                announceNotification.Judgement = item.Notes;
                announceNotification.Judgement_ID = Convert.ToInt32( item.ID);
                announceNotifications.Add(announceNotification);
            }

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            int judgement_id = int.Parse(((CheckBox)sender).Tag.ToString());
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

        public class AnnounceNotification
        {
            public string Client_Name { get; set; }
            public long Case_Number { get; set; }
            public DateTime Date { get; set; }
            public string Judgement { get; set; }
            public int Remain_Days { get; set; }
            public bool Is_Done { get; set; }
            public int Judgement_ID { get; set; }
        }

        private void AllAnnouncesBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.DisplayAllJudgements displayAllJudgements = new DisplayAllJudgements();
            displayAllJudgements.ShowDialog();
        }
    }
}

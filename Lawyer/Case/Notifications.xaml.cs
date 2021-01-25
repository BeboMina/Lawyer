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
        string Now_Date = DateTime.Now.ToShortDateString();
        public Notifications()
        {
            InitializeComponent();

            sessionNotifications = new ObservableCollection<SessionNotification>();
            announceNotifications = new ObservableCollection<AnnounceNotification>();


            sessions = Context.Sessions.Where(S=>S.NextDate.Value.Year==DateTime.Now.Year).ToList();
            foreach(var sess in sessions)
            {
                string Next_Date = sess.NextDate.Value.ToShortDateString();
                
                if(Next_Date==Now_Date)
                {
                    SessionNotification sessionNotification = new SessionNotification();
                    Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID == sess.IDCase);
                    Models.Client_Case client_Case = Context.Client_Case.FirstOrDefault(C => C.IDCase == sess.IDCase);
                    Models.Client client;
                    if(client_Case!=null)
                    {
                        client = Context.Clients.FirstOrDefault(C => C.ID == client_Case.IDClient);
                        sessionNotification.Case_Number = sess.IDCase;
                        sessionNotification.Circle = @case.Circle;
                        sessionNotification.Client_Name = client.Name;
                        sessionNotification.Court = sess.Jadge;
                        sessionNotification.Next_Date = sess.NextDate.Value;
                        sessionNotification.Time = sess.Timer;
                        sessionNotifications.Add(sessionNotification);
                    }
                }
            }

            SessionsList.ItemsSource = sessionNotifications;
            AnnouncesList.ItemsSource = announceNotifications;

            // fill the lists with data like this
            //sessionNotifications.Add(new SessionNotification() { Case_Number = 24, Client_Name = "mina" });
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            int judgement_id = int.Parse(((CheckBox)sender).Tag.ToString());

            // do what you want on the judgement with that judgement id 
        }

        public class SessionNotification
        {
            public string Client_Name { get; set; }
            public long Case_Number { get; set; }
            public string Circle { get; set; }
            public string Court { get; set; }
            public DateTime Next_Date { get; set; }
            public string Time { get; set; }
        }

        public class AnnounceNotification
        {
            public string Client_Name { get; set; }
            public int Case_Number { get; set; }
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

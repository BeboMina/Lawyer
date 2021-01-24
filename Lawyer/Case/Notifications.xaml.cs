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

        public Notifications()
        {
            InitializeComponent();

            sessionNotifications = new ObservableCollection<SessionNotification>();
            announceNotifications = new ObservableCollection<AnnounceNotification>();

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
            public int Case_Number { get; set; }
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

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
    /// Interaction logic for DisplayCase.xaml
    /// </summary>
    public partial class DisplayCase : Window
    {
        public DisplayCase()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void JudgementBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.Judgement judgement = new Judgement(Case_Number.Text);
            judgement.ShowDialog();
        }

        private void ResumeBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.ResumeCase resumeCase = new ResumeCase("استئناف", Case_Number.Text);
            resumeCase.ShowDialog();
        }

        private void VetoBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.ResumeCase resumeCase = new ResumeCase("نقض", Case_Number.Text);
            resumeCase.ShowDialog();
        }

        private void GridView_Session_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridView_Session.SelectedItem == null)
                return;
            Models.Session session = (Models.Session) GridView_Session.SelectedItem;
            Case.DisplayFiles displayFiles = new DisplayFiles(session.ID);
            displayFiles.ShowDialog();
        }
    }
}

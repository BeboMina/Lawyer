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
    /// Interaction logic for ResumeCase.xaml
    /// </summary>
    public partial class ResumeCase : Window
    {
        string type;
        long case_num=-1;
        testEntities Context = new testEntities();
        Models.Case @case = new Models.Case();
        int degree = 0;

        public ResumeCase(string t, Models.Case Case)
        {
            InitializeComponent();
            @case = Case;
            type = t;
            Case_Number.Text = Case.Case_Namber;

            if (type == "نقض")
            {
               
                Models.Resumption resumption = Context.Resumptions.FirstOrDefault(C => C.ID_Case == Case.ID);
                if(resumption != null)
                    caseHasVeto(resumption.ID_Resumption, resumption.Circle, resumption.Notes,resumption.Resumption_Number);

                GboxHeader.Text = "نقض";
                Veto_Number_TxtBlock.Text = "رقم النقض";
                Add_Veto.Content = "اضافة نقض";
                degree = 3;
                
            }
            else
            {
                degree = 2;
                Models.veto veto = Context.vetoes.FirstOrDefault(C => C.ID_Case == Case.ID);
                if (veto != null)
                    caseHasVeto(veto.ID_veto, veto.Circle, veto.Notes,veto.Veto_Number);
            }

            load_gridview();
        }

        private void caseHasVeto(long num, string circle, string notes,string Case_Num)
        {
            Add_Veto.Visibility = Visibility.Collapsed;
            Add_SessionBtn.Visibility = Visibility.Visible;
            Res_Num.IsReadOnly = true;
            Veto_Re_Circle.IsReadOnly = true;
            Notes.IsReadOnly = true;
            Notes.Background = (Brush)(new BrushConverter().ConvertFrom("#FFC5CBF9"));

            Res_Num.Text = Case_Num;
            Veto_Re_Circle.Text = circle;
            new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text = notes;
            case_num = num;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Veto_Click(object sender, RoutedEventArgs e)
        {
            string message = "تاكيد حفظ البيانات";
            string title = "حفظ";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);
            if (result == MessageBoxResult.Yes)
            {

                string res_veto_num= Res_Num.Text;
                if (string.IsNullOrWhiteSpace(res_veto_num))
                {
                    MessageBox.Show("يجب اضافة رقم ال" + GboxHeader.Text);
                    return;
                }

                if (((Button)sender).Content.ToString() == "اضافة استئناف")
                {
                    Models.veto veto = new veto();
                    veto.ID_Case = @case.ID;
                    veto.Veto_Number = res_veto_num;
                    veto.Circle = Veto_Re_Circle.Text;
                    veto.Notes = new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
                    Context.vetoes.Add(veto);
                }
                else
                {
                    Models.Resumption resumption = new Resumption();
                    resumption.ID_Case = @case.ID;
                    resumption.Resumption_Number = res_veto_num;
                    resumption.Circle = Veto_Re_Circle.Text;
                    resumption.Notes = new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
                    Context.Resumptions.Add(resumption);
                }
                Context.SaveChanges();

                Case.ResumeCase resumeCase = new ResumeCase(type, @case);
                Close();
                resumeCase.ShowDialog();
            }
        }

        private void Add_SessionBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.AddSession addSession = new AddSession(type, case_num);
            addSession.Num_Case.Text = Case_Number.Text;
            addSession.Num_Veto.Text = Res_Num.Text;
            addSession.ShowDialog();

            load_gridview();
        }

        private void GridView_Session_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridView_Session.SelectedItem == null)
                return;
            Models.Session session = (Models.Session)GridView_Session.SelectedItem;
            Case.DisplayFiles displayFiles = new DisplayFiles(session.ID);
            //new TextRange(displayFiles.Notes_Session.Document.ContentStart, displayFiles.Notes_Session.Document.ContentEnd).Text = session.Notes;
            displayFiles.ShowDialog();
        }

        void load_gridview()
        {
            if (case_num != -1)
            {
                List<Models.Session> sessions = Context.Sessions.Where(S => S.IDCase == case_num && S.Case_Degree == degree).ToList();
                if (sessions.Count != 0)
                {
                    GridView_Session.ItemsSource = sessions;
                }
            }
        }
    }
}

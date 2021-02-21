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
    /// Interaction logic for DisplayCase.xaml
    /// </summary>
    public partial class DisplayCase : Window
    {
        testEntities Context = new testEntities();
        long ID_Case;
        Models.Case Case;
        public DisplayCase(long ID)
        {
            InitializeComponent();
            Case = Context.Cases.FirstOrDefault(C => C.ID == ID);
            ID_Case = ID;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void JudgementBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.Judgement judgement = new Judgement(Case_Number.Text,ID_Case);
            judgement.ShowDialog();
        }

        private void ResumeBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.ResumeCase resumeCase = new ResumeCase("استئناف", Case);
            resumeCase.ShowDialog();
        }

        private void VetoBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.ResumeCase resumeCase = new ResumeCase("نقض",Case);
            resumeCase.ShowDialog();
        }

        private void GridView_Session_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridView_Session.SelectedItem == null)
                return;
            Models.Session session = (Models.Session) GridView_Session.SelectedItem;
            Case.DisplayFiles displayFiles = new DisplayFiles(session.ID);
           // new TextRange(displayFiles.Notes_Session.Document.ContentStart, displayFiles.Notes_Session.Document.ContentEnd).Text = session.Notes==null? "": session.Notes;
            displayFiles.ShowDialog();
        }

        private void CloseCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID == ID_Case);
                if (@case.Lock==true)
                {
                    MessageBox.Show("الدعوى مغلقة");
                    return;
                }
                string message = "تاكيد غلق الدعوى";
                string title = "غلق";
                MessageBoxButton buttons = MessageBoxButton.YesNo;
                MessageBoxResult result = MessageBox.Show(message, title, buttons);
                if (result == MessageBoxResult.Yes)
                {
                    
                    @case.Lock = true;
                    Context.SaveChanges();
                    CloseCaseBtn.Content = "الدعوى مغلقة";
                    MessageBox.Show("تم غلق الدعوى");

                    MainWindow parent = (MainWindow)App.Current.MainWindow;
                    parent.main.Navigate(new Cases());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateNotesBtn_Click(object sender, RoutedEventArgs e)
        {
            if(UpdateNotesBtn.Content.ToString() == "تعديل")
            {
                Notes.IsReadOnly = false;
                Notes.Background = Brushes.White;
                Notes.Focus();
                UpdateNotesBtn.Content = "حفظ";
            }
            else
            {
                try
                {
                    Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID == ID_Case);
                    if (@case != null)
                    {
                        @case.Notes = new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
                        Context.SaveChanges();
                        MessageBox.Show("تم التعديل");
                    }

                    Notes.IsReadOnly = true;
                    Notes.Background = (Brush)(new BrushConverter().ConvertFrom("#FFC5CBF9"));
                    UpdateNotesBtn.Content = "تعديل";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

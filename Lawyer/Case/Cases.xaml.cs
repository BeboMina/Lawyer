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
using Lawyer.Models;
namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for Cases.xaml
    /// </summary>
    public partial class Cases : Page
    {
        testEntities Context = new testEntities();
        List<Models.View_1> view_1s;
        List<Case_Model> Case_Models = new List<Case_Model>();
        public Cases()
        {
            InitializeComponent();
            try
            {


                view_1s = Context.View_1.ToList();
                foreach (var item in view_1s)
                {
                    Case_Model case_Model = new Case_Model();
                    case_Model.ID_Case = item.ID;
                    case_Model.Client_Name = item.Name;
                    case_Model.Type_Case = item.Type;
                    Models.Case @case = Context.Cases.FirstOrDefault(C => C.ID == item.ID);
                    if (@case!=null) 
                    {
                        case_Model.Notes = @case.Notes;
                        case_Model.Lock = (@case.Lock == true) ? "الدعوى مغلقة" : " الدعوى مفتوحة";
                        Case_Models.Add(case_Model);


                    }
                }
                DataGrid_Cases.ItemsSource = Case_Models;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCase addCase = new AddCase("Case");
            addCase.ShowDialog();
        }

        private void AddProxyBtn_Click(object sender, RoutedEventArgs e)
        {
            
            AddSession addSession = new AddSession("case");
            addSession.ShowDialog();
        }

        private void DataGrid_Cases_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Case_Model @case = (Case_Model)DataGrid_Cases.SelectedItem;
            if (@case == null)
                return;
            Models.Case case1 = Context.Cases.FirstOrDefault(C => C.ID == @case.ID_Case);
            Case.DisplayCase displayCase = new DisplayCase(@case.ID_Case);
            displayCase.CloseCaseBtn.Content= (case1.Lock == true) ? "الدعوى مغلقة" : " اغلاق الدعوى";
            displayCase.CLient_Name.Text = @case.Client_Name;
            displayCase.Case_Number.Text = @case.ID_Case.ToString();
            displayCase.Case_Type.Text= @case.Type_Case;
            displayCase.C_Case.Text = case1.Circle;
            new TextRange(displayCase.Notes.Document.ContentStart, displayCase.Notes.Document.ContentEnd).Text = case1.Notes==null ? "": case1.Notes;
            List<Models.Session> sessions = Context.Sessions.Where(S => S.IDCase == @case.ID_Case&&S.Case_Degree==1).ToList();
            if(sessions.Count!=0)
            {
                displayCase.GridView_Session.ItemsSource = sessions;
            }
            displayCase.ShowDialog();
        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SearchTxt.Text == "")
                {
                    view_1s = Context.View_1.ToList();

                }
                else
                {
                    long id = Convert.ToInt64(SearchTxt.Text);
                    view_1s = Context.View_1.Where(V => V.ID==id).ToList();
                }
                DataGrid_Cases.ItemsSource = view_1s;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

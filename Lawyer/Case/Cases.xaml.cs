﻿using System;
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
        public Cases()
        {
            InitializeComponent();
            view_1s = Context.View_1.ToList();
            DataGrid_Cases.ItemsSource = view_1s;
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
            Models.View_1 @case = (Models.View_1)DataGrid_Cases.SelectedItem;
            if (@case == null)
                return;
            Case.DisplayCase displayCase = new DisplayCase();
            Models.Case case1 = Context.Cases.FirstOrDefault(C => C.ID == @case.ID);
            displayCase.CLient_Name.Text = @case.Name;
            displayCase.Case_Number.Text = @case.ID.ToString();
            displayCase.Case_Type.Text= @case.Type;
            displayCase.C_Case.Text = case1.Circle;
            List<Models.Session> sessions = Context.Sessions.Where(S => S.IDCase == @case.ID&&S.Case_Degree==1).ToList();
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

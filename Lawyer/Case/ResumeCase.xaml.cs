﻿using Lawyer.Models;
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
        long case_num;
        testEntities Context = new testEntities();

        public ResumeCase(string t, string num)
        {
            InitializeComponent();

            type = t;
            case_num = long.Parse(num);
            Case_Number.Text = num;

            if(type == "نقض")
            {
                Models.Resumption resumption = Context.Resumptions.FirstOrDefault(C => C.ID_Case == case_num);
                if(resumption != null)
                    caseHasVeto(resumption.ID_Resumption, resumption.Circle, resumption.Notes);

                GboxHeader.Text = "نقض";
                Veto_Number_TxtBlock.Text = "رقم النقض";
                Add_Veto.Content = "اضافة نقض";
            }
            else
            {
                Models.veto veto = Context.vetoes.FirstOrDefault(C => C.ID_Case == case_num);
                if (veto != null)
                    caseHasVeto(veto.ID_veto, veto.Circle, veto.Notes);
            }
        }

        private void caseHasVeto(long num, string circle, string notes)
        {
            Add_Veto.Visibility = Visibility.Collapsed;
            Add_SessionBtn.Visibility = Visibility.Visible;
            Res_Num.IsReadOnly = true;
            Veto_Re_Circle.IsReadOnly = true;
            Notes.IsReadOnly = true;
            Notes.Background = (Brush)(new BrushConverter().ConvertFrom("#FFC5CBF9"));

            Res_Num.Text = num.ToString();
            Veto_Re_Circle.Text = circle;
            new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text = notes;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Veto_Click(object sender, RoutedEventArgs e)
        {
            long res_veto_num;
            if(!long.TryParse(Res_Num.Text, out res_veto_num))
            {
                MessageBox.Show("يجب اضافة رقم ال" + GboxHeader.Text);
                return;
            }
            
            if (((Button)sender).Content.ToString() == "اضافة استئناف")
            {
                Models.veto veto = new veto();
                veto.ID_Case = case_num;
                veto.ID_veto = res_veto_num;
                veto.Circle = Veto_Re_Circle.Text;
                veto.Notes = new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
                Context.vetoes.Add(veto);
            }
            else
            {
                Models.Resumption resumption = new Resumption();
                resumption.ID_Case = case_num;
                resumption.ID_Resumption = res_veto_num;
                resumption.Circle = Veto_Re_Circle.Text;
                resumption.Notes = new TextRange(Notes.Document.ContentStart, Notes.Document.ContentEnd).Text;
                Context.Resumptions.Add(resumption);
            }
            Context.SaveChanges();

            //Add_Veto.Visibility = Visibility.Hidden;
            //Add_SessionBtn.Visibility = Visibility.Visible;
            Case.ResumeCase resumeCase = new ResumeCase(type, case_num.ToString());
            Close();
            resumeCase.ShowDialog();
        }
    }
}
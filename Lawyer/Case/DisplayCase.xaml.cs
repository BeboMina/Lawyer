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
            Case.Judgement judgement = new Judgement();
            judgement.ShowDialog();
        }

        private void ResumeBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.ResumeCase resumeCase = new ResumeCase("استئناف");
            resumeCase.ShowDialog();
        }

        private void VetoBtn_Click(object sender, RoutedEventArgs e)
        {
            Case.ResumeCase resumeCase = new ResumeCase("نقض");
            resumeCase.ShowDialog();
        }
    }
}

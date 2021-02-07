using Lawyer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lawyer.Case
{
    /// <summary>
    /// Interaction logic for DisplayFiles.xaml
    /// </summary>
    public partial class DisplayFiles : Window
    {
        testEntities Context = new testEntities();
        long Id_Session;
        public DisplayFiles(long id)
        {
            InitializeComponent();
            Id_Session = id;
            List<Models.FilesCas> filesCas = Context.FilesCases.Where(C => C.IDSessios == id).ToList();
            FilesGrid.ItemsSource = filesCas;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FilesGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FilesGrid.SelectedItem == null)
                return;

            try
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Models.FilesCas filesCas = (Models.FilesCas)FilesGrid.SelectedItem;
                    string folder = folderBrowser.SelectedPath;
                    string name = filesCas.Title + filesCas.Extantion;
                    byte[] data = filesCas.Data;
                    File.WriteAllBytes(folder + "\\" + name, data);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void UpdateNotesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateNotesBtn.Content.ToString() == "تعديل")
            {
                Notes_Session.IsReadOnly = false;
                Notes_Session.Background = Brushes.White;
                Notes_Session.Focus();
                UpdateNotesBtn.Content = "حفظ";
            }
            else
            {
                try
                {
                    Models.Session session = Context.Sessions.FirstOrDefault(S => S.ID == Id_Session);
                    if(session!=null)
                    {
                        session.Notes= new TextRange(Notes_Session.Document.ContentStart, Notes_Session.Document.ContentEnd).Text;
                        Context.SaveChanges();
                        System.Windows.MessageBox.Show("تم التعديل");

                    }
                    Notes_Session.IsReadOnly = true;
                    Notes_Session.Background = (Brush)(new BrushConverter().ConvertFrom("#FFC5CBF9"));
                    UpdateNotesBtn.Content = "تعديل";
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

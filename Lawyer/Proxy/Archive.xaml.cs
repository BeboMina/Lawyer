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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lawyer.Proxy
{
    /// <summary>
    /// Interaction logic for Archive.xaml
    /// </summary>
    public partial class Archive : Page
    {
        testEntities Context = new testEntities();
        List<Models.Files_Saved> files_Saveds;
        public Archive()
        {
            InitializeComponent();
            
            files_Saveds = Context.Files_Saved.ToList();
            GridView_Notes.ItemsSource = files_Saveds;
        }

        private void AddNoteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog();
                open.Multiselect = true;
                if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string message = "تاكيد حفظ الملفات";
                    string title = "حفظ";
                    MessageBoxButton buttons = MessageBoxButton.YesNo;
                    MessageBoxResult result = System.Windows.MessageBox.Show(message, title, buttons);
                    if (result == MessageBoxResult.Yes)
                    {
                        Models.Files_Saved files_Saved = new Models.Files_Saved();
                        foreach (var item in open.FileNames)
                        {
                            files_Saved.Data = File.ReadAllBytes(item);
                            files_Saved.Title = System.IO.Path.GetFileNameWithoutExtension(item);
                            files_Saved.Extantion = System.IO.Path.GetExtension(item);
                            Context.Files_Saved.Add(files_Saved);
                            Context.SaveChanges();
                        }
                        files_Saveds = Context.Files_Saved.ToList();
                        GridView_Notes.ItemsSource = files_Saveds;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void RemoveNoteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (GridView_Notes.SelectedItem != null)
                {
                    string message = "تاكيد حزف الملفات";
                    string title = "حفظ";
                    MessageBoxButton buttons = MessageBoxButton.YesNo;
                    MessageBoxResult result = System.Windows.MessageBox.Show(message, title, buttons);
                    if (result == MessageBoxResult.Yes)
                    {
                        Models.Files_Saved files_Saved = (Models.Files_Saved)GridView_Notes.SelectedItem;
                        Context.Files_Saved.Remove(files_Saved);
                        Context.SaveChanges();
                        files_Saveds = Context.Files_Saved.ToList();
                        GridView_Notes.ItemsSource = files_Saveds;
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("اختار الملف اولا");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void SearchNotesTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SearchNotesTxt.Text == "")
                {
                    files_Saveds = Context.Files_Saved.ToList();
                }
                else
                {
                    files_Saveds = Context.Files_Saved.Where(F => F.Title.Contains(SearchNotesTxt.Text)).ToList();
                }
                GridView_Notes.ItemsSource = files_Saveds;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void GridView_Notes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GridView_Notes.SelectedItem == null)
                return;

            try
            {
                
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    Models.Files_Saved files_Saved = (Models.Files_Saved)GridView_Notes.SelectedItem;
                    string folder = folderBrowser.SelectedPath;
                    string name = files_Saved.Title + files_Saved.Extantion;
                    byte[] data = files_Saved.Data;
                    File.WriteAllBytes(folder + "\\" + name, data);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}

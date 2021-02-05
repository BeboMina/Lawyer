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
        public DisplayFiles(long id)
        {
            InitializeComponent();
            List<Models.FilesCas> filesCas = Context.FilesCases.Where(C => C.IDSessios == id).ToList();
            FilesGrid.ItemsSource = filesCas;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FilesGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
    }
}

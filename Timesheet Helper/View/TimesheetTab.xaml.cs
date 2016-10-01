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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace TimesheetHelper.View
{
    /// <summary>
    /// Interaction logic for TimesheetTab.xaml
    /// </summary>
    public partial class TimesheetTab: UserControl
    {
        public TimesheetTab()
        {
            InitializeComponent();

            this.DataContext = TimesheetHelper.Settings.CurrentSettings;
            //this.DataContext = TimesheetHelper.Settings.InitialSettings();
        }

        private void btnNewFile_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "Select excel worksheet.";
            fileDialog.Filter = "Excel Worksheets|*.xls;*.xlsx";

            if (Directory.Exists(TimesheetHelper.Settings.CurrentSettings.FilePath))
            {
                fileDialog.InitialDirectory = TimesheetHelper.Settings.CurrentSettings.FilePath;
            }

            if (fileDialog.ShowDialog() == true)
            {
                string fullPath = fileDialog.FileName;
                System.Diagnostics.Debug.WriteLine(fullPath);
                TimesheetHelper.Settings.CurrentSettings.ExcelFile = fileDialog.SafeFileName;
                TimesheetHelper.Settings.CurrentSettings.FilePath = fullPath.Substring(0, fullPath.LastIndexOf('\\') + 1);
            }
        }
    }
}

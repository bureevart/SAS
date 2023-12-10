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

namespace SAS.Forms
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        MainWindow MainForm;
        public Settings(MainWindow main)
        {
            InitializeComponent();
            MainForm = main;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb) 
            {
                if (cb.IsChecked == true)
                {
                    MainForm.Main.NavigationUIVisibility = NavigationUIVisibility.Visible;
                }
                else if (cb.IsChecked == false)
                {
                    MainForm.Main.NavigationUIVisibility = NavigationUIVisibility.Hidden;
                }
            }
        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb)
            {
                if (cb.IsChecked == true)
                {
                    MainForm.Main.NavigationUIVisibility = NavigationUIVisibility.Visible;
                }
                else if (cb.IsChecked == false)
                {
                    MainForm.Main.NavigationUIVisibility = NavigationUIVisibility.Hidden;
                }
            }
        }

        private void ClearHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            History.EventsController.ClearData();
        }

        private void ChangeSoundStateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

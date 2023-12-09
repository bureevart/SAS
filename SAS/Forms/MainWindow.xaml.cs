using SAS.Forms;
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

namespace SAS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public History HistoryPage = new History();
        public Settings SettingsPage;
        public Forms.Panel PanelPage = new Forms.Panel();
        public Home HomePage = new Home();
        public MainWindow()
        {
            InitializeComponent();

            SettingsPage = new Settings(this);
            Main.Content = HomePage;

        }

        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            MainForm.Close();
        }

        private void OnSettingsButtonClick(object sender, RoutedEventArgs e)
        {
            
            Main.Content = SettingsPage;
        }

        private void OnHistoryButtonClick(object sender, RoutedEventArgs e)
        {
            Main.Content = HistoryPage;
        }

        private void OnPanelButtonClick(object sender, RoutedEventArgs e)
        {
            Main.Content = PanelPage;
        }

        private void OnHomeButtonClick(object sender, RoutedEventArgs e)
        {
            Main.Content = HomePage;
        }
    }
}

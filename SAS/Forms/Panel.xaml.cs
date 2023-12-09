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
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class Panel : Page
    {
        public Panel()
        {
            InitializeComponent();
        }

        private void OnInputCodeButton(object sender, RoutedEventArgs e)
        {
            WrongInputLabel.Content = string.Empty;
            var codestr = CodeInputTextBox.Text;

            if (codestr == string.Empty) { return; }

            if (int.TryParse(codestr, out var input))
            {

            }
            else
            {
                WrongInputLabel.Content = "Введите корректный код!";
            }
        }

        private void CodeInputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            int number;

            if (WrongInputLabel == null) return;
            WrongInputLabel.Content = string.Empty;
            if (!int.TryParse(textBox.Text, out number)){
                WrongInputLabel.Content = "Пожалуйста введите число!";
            }
        }

        private void OnOffAlarmButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

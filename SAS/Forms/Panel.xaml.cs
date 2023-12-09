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
using System.Windows.Threading;

namespace SAS.Forms
{
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class Panel : Page
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        public static List<Room> Rooms = new List<Room>();
        public static Room currentRoom;

        public Panel()
        {
            InitializeComponent();

            Rooms.Add(new Room(1004, 0, true, false, false));
            Rooms.Add(new Room(103, 0, true, false, false));
            Rooms.Add(new Room(8543, 0, true, false, false));

            _timer.Tick += new EventHandler(DispatcherTimer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void OnInputCodeButton(object sender, RoutedEventArgs e)
        {
            WrongInputLabel.Content = string.Empty;
            var codestr = CodeInputTextBox.Text;

            if (codestr == string.Empty) { return; }
            if (int.TryParse(codestr, out var input))
            {
                foreach (var room in Rooms)
                {
                    if (room.Code == input)
                    {
                        currentRoom = room;
                        break;
                    }
                }
            }
            if (currentRoom != null)
            {

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

        private async void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if(currentRoom == null) { return; }
            switch (currentRoom.SensorStatus)
            {
                case 0:
                    SensorEll.Fill = Brushes.White;
                    break;
                case 1:
                    SensorEll.Fill = Brushes.Green;
                    break;
                case 2:
                    SensorEll.Fill = Brushes.Red;
                    break;
            }
            if(currentRoom.PowerStatus) 
                PowerEll.Fill = Brushes.Green;
            else 
                PowerEll.Fill = Brushes.Red;
            if(currentRoom.NetworkStatus)
                NetworkEll.Fill = Brushes.Red;
            else NetworkEll.Fill = Brushes.Green;
            if (currentRoom.AlarmStatus)
                AlarmEll.Fill = Brushes.Red;
            else
                AlarmEll.Fill = Brushes.White;
        }

        private void SetAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRoom == null) { return; }

            currentRoom.SensorStatus = 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        public static Room? currentRoom;
        public enum SensorStatus
        { 
            off = 0,
            on = 1,
            alarm = 2
        }

        public enum PanelAlarmStatus
        {   
            on = 1,
            alarm = 2,
            off = 3
        }
        public bool SimulationStarted = false;
        
        public Panel()
        {
            InitializeComponent();

            Rooms.Add(new Room(1004, 0, true));
            Rooms.Add(new Room(103, 0, true));
            Rooms.Add(new Room(8543, 0, true));

            _timer.Tick += new EventHandler(DispatcherTimer_Tick);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void OnInputCodeButton_Click(object sender, RoutedEventArgs e)
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
        private async void OnTestButton_Click(object sender, RoutedEventArgs e){
            _ = Task.Run(() =>
            {
                SimulateController.Simulate(Rooms, DisplayLabel);
            });
            TestButton.IsEnabled = !SimulationStarted;
            await Task.Delay(5000);
            TestButton.IsEnabled = SimulationStarted;
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
            if (currentRoom == null) { return; }
            currentRoom.IsAlarmSet = !currentRoom.IsAlarmSet;
            int AlarmStatus;
            if(currentRoom.IsAlarmSet)
                AlarmStatus = (int)PanelAlarmStatus.on;
            else
                AlarmStatus = (int)PanelAlarmStatus.off;
            switch (AlarmStatus)
            {
                case (int)PanelAlarmStatus.on:
                    Blink((int)PanelAlarmStatus.on, PanelAlarmEll);
                    break;
                case (int)PanelAlarmStatus.off:
                    PanelAlarmEll.Fill = Brushes.Gray;
                    break;
            }
        }

        private async void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if(currentRoom == null) { return; }
            
            switch (currentRoom.SensorStatus)
            {
                case (int)SensorStatus.off:
                    SensorEll.Fill = Brushes.White;
                    break;
                case (int)SensorStatus.on:
                    SensorEll.Fill = Brushes.Green;
                    break;
                case (int)SensorStatus.alarm:
                    Blink(3, SensorEll);
                    Blink((int)PanelAlarmStatus.alarm, PanelAlarmEll);
                    break;
            }
            if(currentRoom.PowerStatus) 
                PowerEll.Fill = Brushes.Green;
            else 
                PowerEll.Fill = Brushes.Red;
            if(currentRoom.NetworkStatus)
                NetworkEll.Fill = Brushes.Green;
            else NetworkEll.Fill = Brushes.Red;
            if (currentRoom.AlarmStatus)
                Blink(2, AlarmEll);
            else
                AlarmEll.Fill = Brushes.White;
        }

        private void SetAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRoom == null) { return; }

            currentRoom.SensorStatus = 1;
        }

        private void Blink(int code, Ellipse ellipse){

            Storyboard storyboard = new Storyboard();
            ColorAnimation animation = new ColorAnimation();
            Color[] colorsList = new Color[4]
            {
                Colors.Gray,
                Colors.Green,
                Colors.Red,
                Colors.Yellow
            };

            animation.To = colorsList[code];
            animation.From = colorsList[0];
            animation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard.SetTarget(animation, ellipse);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Fill.Color"));

            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

    }
}

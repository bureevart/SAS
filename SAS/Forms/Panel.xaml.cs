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

        public enum SensorStatusses
        { 
            Off = 0,
            On = 1,
            Alarm = 2
        }

        public enum PanelAlarmStatus
        {
            Off = 0,
            On = 1,
            Alarm = 2,
            Error = 3
        }
        public bool SimulationStarted = false;
        
        public Panel()
        {
            InitializeComponent();
            Room.RedrawEllispse += PanelEllipseStatus;
            Rooms.Add(new Room(1004, SensorStatusses.Off, false, DisplayLabel));
            Rooms.Add(new Room(103, SensorStatusses.Off, false, DisplayLabel));
            Rooms.Add(new Room(8543, SensorStatusses.Off, false, DisplayLabel));

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
                        SetOnOffButtonStatus();
                        break;
                    }
                }
            }
            if (currentRoom != null)
            {

            }
        }
        private async void OnTestButton_Click(object sender, RoutedEventArgs e){
            if (SimulateController.OnSimulate)
            {
                SimulateController.OnSimulate = false;
                TestButton.Content = "ТЕСТ";
                TestButton.Background = Brushes.LightGray;
                TestButton.Foreground = Brushes.Black;
                return;
            }
            _ = Task.Run(() =>
            {
                SimulateController.Simulate(Rooms);
            });
            Room.OnStartTest();
            TestButton.Content = "СТОП";
            TestButton.Background = Brushes.Red;
            TestButton.Foreground = Brushes.White;
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

        private void OffAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRoom == null) { return; }
            currentRoom.OffAlarm();
        }

        private async void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if(currentRoom == null) { return; }
            
            switch (currentRoom.SensorStatus)
            {
                case SensorStatusses.Off:
                    SensorEll.Fill = Brushes.White;
                    break;
                case SensorStatusses.On:
                    SensorEll.Fill = Brushes.Green;
                    break;
                case SensorStatusses.Alarm:
                    Blink(3, SensorEll);
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

        private void OnOffAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRoom == null) { return; }

            if (currentRoom.PowerStatus)
            {
                currentRoom.DisableSystem();
                OnOffAlarmButton.Content = "ВКЛ";
                OnOffAlarmButton.Background = Brushes.Green;
                OnOffAlarmButton.Foreground = Brushes.Black;
                return;
            }
            currentRoom.EnableSystem();
            OnOffAlarmButton.Content = "ВЫКЛ";
            OnOffAlarmButton.Background = Brushes.Red;
            OnOffAlarmButton.Foreground = Brushes.White;
        }

        public void SetOnOffButtonStatus()
        {
            if (currentRoom == null) { return; }

            if (!currentRoom.PowerStatus)
            {
                OnOffAlarmButton.Content = "ВКЛ";
                OnOffAlarmButton.Background = Brushes.Green;
                OnOffAlarmButton.Foreground = Brushes.Black;
                return;
            }
            OnOffAlarmButton.Content = "ВЫКЛ";
            OnOffAlarmButton.Background = Brushes.Red;
            OnOffAlarmButton.Foreground = Brushes.White;

        }

        private void SetAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRoom == null) { return; }

            currentRoom.SetAlarm();
        }

        public void PanelEllipseStatus(object sender, EventArgs e)
        {
            PanelAlarmStatus currStatus = PanelAlarmStatus.Off;
            Rooms.ForEach(r =>
            {
                if (r.AlarmStatus)
                {
                    currStatus = PanelAlarmStatus.Alarm;
                }

                if(r.SensorStatus == SensorStatusses.On && currStatus != PanelAlarmStatus.Alarm)
                {
                    currStatus = PanelAlarmStatus.On;
                }
            });
            PanelAlarmEll.Dispatcher.Invoke(new Action(() =>
            {
                Blink((int)currStatus, PanelAlarmEll);
            }));

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

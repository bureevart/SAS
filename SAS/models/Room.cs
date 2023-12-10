using System;
using System.Windows.Controls;
using static SAS.Forms.Panel;

namespace SAS
{
    public class Room {
        public int Code { get; set; }
        public SensorStatusses SensorStatus { get; set; }
        public bool PowerStatus { get; set; }
        public bool AlarmStatus { get => SensorStatus == SensorStatusses.Alarm; }
        public bool IsAlarmSet{ get; set; }
        public bool NetworkStatus => PowerStatus && IsAlarmSet;
        public Label DisplayLabel { get; }
        public static event EventHandler<EventArgs> RedrawEllispse;

        public static void OnStartTest()
        {
            RedrawEllispse.Invoke(null, new EventArgs());
        }

        public Room(int code, SensorStatusses sensorStatus, bool powerStatus, Label displaylabel)
        {
            Code = code;
            SensorStatus = sensorStatus;
            PowerStatus = powerStatus;
            DisplayLabel = displaylabel;
            
        }

        public async void TriggerAlarm(){
            if(!PowerStatus || AlarmStatus || SensorStatus == SensorStatusses.Off || !NetworkStatus) { return; }

            SensorStatus = SensorStatusses.Alarm;
            DisplayLabel.Dispatcher.Invoke(() => DisplayLabel.Content = "Код комнаты: " + Code + " - Состояние: Тревога!");
            RedrawEllispse.Invoke(this, new EventArgs());
        }

        public async void OffAlarm()
        {
            if(!AlarmStatus) { return; }
            SensorStatus = SensorStatusses.On;
            DisplayLabel.Dispatcher.Invoke(() => DisplayLabel.Content = "Код комнаты: " + Code + " - Состояние: Тревога отключена!");
            RedrawEllispse.Invoke(this, new EventArgs());
        }

        public async void SetAlarm()
        {
            if (AlarmStatus || !PowerStatus || !NetworkStatus) { return; }

            SensorStatus = SensorStatusses.On;
            RedrawEllispse.Invoke(this, new EventArgs());
        }

        public async void EnableSystem()
        {
            if (PowerStatus || NetworkStatus) { return; }

            IsAlarmSet = true;
            PowerStatus = true;
            RedrawEllispse.Invoke(this, new EventArgs());
        }

        public async void DisableSystem()
        {
            if (!PowerStatus) { return; }

            IsAlarmSet = false;
            PowerStatus = false;
            SensorStatus = SensorStatusses.Off;
            RedrawEllispse.Invoke(this, new EventArgs());
        }
    }
}
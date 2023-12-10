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


        public Room(int code, SensorStatusses sensorStatus, bool powerStatus, Label displaylabel)
        {
            Code = code;
            SensorStatus = sensorStatus;
            PowerStatus = powerStatus;
            DisplayLabel = displaylabel;
            
        }

        public async void TriggerAlarm(){
            if(AlarmStatus || SensorStatus == SensorStatusses.Off || !NetworkStatus) { return; }

            SensorStatus = SensorStatusses.Alarm;
            DisplayLabel.Dispatcher.Invoke(() => DisplayLabel.Content = "Код комнаты: " + Code + " - Состояние: Тревога!");
        }

        public async void OffAlarm()
        {
            if(!AlarmStatus) { return; }
            SensorStatus = SensorStatusses.On;
            DisplayLabel.Dispatcher.Invoke(() => DisplayLabel.Content = "Код комнаты: " + Code + " - Состояние: Тревога отключена!");
        }

        public async void EnableSystem()
        {
            if (PowerStatus || NetworkStatus) { return; }

            IsAlarmSet = true;
            PowerStatus = true;

        }

        public async void DisableSystem()
        {
            if (!PowerStatus) { return; }

            IsAlarmSet = false;
            PowerStatus = false;
            SensorStatus = SensorStatusses.Off;

        }
    }
}
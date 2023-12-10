using SAS.Forms;
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
        public bool IsButtonBroken { get; set; } = false;
        public Label DisplayLabel { get; }
        public static event EventHandler<EventArgs> RedrawEllispse;
        public static event EventHandler<EventArgs> RewriteLabel;

        public static void OnStartTest()
        {
            RedrawEllispse.Invoke(null, new EventArgs());
            RewriteLabel.Invoke(null, new EventArgs()); 
        }

        public Room(int code, SensorStatusses sensorStatus, bool powerStatus, Label displaylabel)
        {
            Code = code;
            SensorStatus = sensorStatus;
            PowerStatus = powerStatus;
            DisplayLabel = displaylabel;
        }

        public async void TriggerAlarm(){
            if(!PowerStatus || AlarmStatus || SensorStatus == SensorStatusses.Off || !NetworkStatus || SensorStatus == SensorStatusses.Error) { return; }

            SensorStatus = SensorStatusses.Alarm;
            // DisplayLabel.Dispatcher.Invoke(() => DisplayLabel.Content = "Код комнаты: " + Code + " - Состояние: Тревога!");
            History.EventsController.AddEvent(this, "Код комнаты: " + Code + " - Состояние: Тревога!");
            RewriteLabel.Invoke(this, new EventArgs());
            RedrawEllispse.Invoke(this, new EventArgs());
        }

        public async void OffAlarm()
        {
            if(!AlarmStatus || SensorStatus == SensorStatusses.Error) { return; }
            SensorStatus = SensorStatusses.On;
            // DisplayLabel.Dispatcher.Invoke(() => DisplayLabel.Content = "Код комнаты: " + Code + " - Состояние: Тревога отключена!");
            History.EventsController.AddEvent(this, "Код комнаты: " + Code + " - Состояние: Тревога отключена!");
            RewriteLabel.Invoke(this, new EventArgs()); 
            RedrawEllispse.Invoke(this, new EventArgs());
        }

        public async void SetAlarm()
        {
            if (AlarmStatus || !PowerStatus || !NetworkStatus || SensorStatus == SensorStatusses.Error || SensorStatus == SensorStatusses.On) { return; }

            SensorStatus = SensorStatusses.On;
            History.EventsController.AddEvent(this, "Код комнаты: " + Code + " - Состояние: Сигнализация поставлена!");
            RewriteLabel.Invoke(this, new EventArgs());
            RedrawEllispse.Invoke(this, new EventArgs());
        }

        public async void EnableSystem()
        {
            if (PowerStatus || NetworkStatus) { return; }

            IsAlarmSet = true;
            PowerStatus = true;
            History.EventsController.AddEvent(this, "Код комнаты: " + Code + " - Состояние: Подсистема сигнализации включена!");
            RewriteLabel.Invoke(this, new EventArgs());
            RedrawEllispse.Invoke(this, new EventArgs());
        }

        public async void DisableSystem()
        {
            if (!PowerStatus) { return; }

            IsAlarmSet = false;
            PowerStatus = false;
            History.EventsController.AddEvent(this, "Код комнаты: " + Code + " - Состояние: Подсистема сигнализации выключена!");
            SensorStatus = SensorStatusses.Off;
            RewriteLabel.Invoke(this, new EventArgs());
            RedrawEllispse.Invoke(this, new EventArgs());
        }
        public async void DestroySensor()
        {
            if(SensorStatus == SensorStatusses.Error || SensorStatus == SensorStatusses.Off || SensorStatus == SensorStatusses.Alarm)
            {
                return;
            }
            SensorStatus = SensorStatusses.Error;
            
            History.EventsController.AddEvent(this, "Код комнаты: " + Code + " - Состояние: Датчик сломан!");
            RewriteLabel.Invoke(this, new EventArgs());
            RedrawEllispse.Invoke(this, new EventArgs());
        }
        public async void RepairSensor()
        {
            if(SensorStatus == SensorStatusses.On || SensorStatus == SensorStatusses.Off || SensorStatus == SensorStatusses.Alarm)
            {
                return;
            }
            SensorStatus = SensorStatusses.On;
            History.EventsController.AddEvent(this, "Код комнаты: " + Code + " - Состояние: Датчик восстановлен!");
            RewriteLabel.Invoke(this, new EventArgs());
            RedrawEllispse.Invoke(this, new EventArgs());
        }
        public async void BreakButton()
        {
            if (SensorStatus == SensorStatusses.Error || SensorStatus == SensorStatusses.Off || SensorStatus == SensorStatusses.Alarm || IsButtonBroken == true)
            {
                return;
            }
            IsButtonBroken = true;
            History.EventsController.AddEvent(this, "Код комнаты: " + Code + " - Состояние: Кнопка сломана!");
            RewriteLabel.Invoke(this, new EventArgs());
            RedrawEllispse.Invoke(this, new EventArgs());   
        }
        public async void FixButton()
        {
            if (IsButtonBroken == false)
            {
                return;
            }
            IsButtonBroken = false;
            History.EventsController.AddEvent(this, "Код комнаты: " + Code + " - Состояние: Кнопка восстановлена!");
            RewriteLabel.Invoke(this, new EventArgs());
            RedrawEllispse.Invoke(this, new EventArgs());
        }
    }
}
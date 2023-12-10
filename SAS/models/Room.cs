using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace SAS{
    public class Room {
        public Dictionary<int, int> roomCodes = new Dictionary<int, int>
        {
            { 1004, 317 },
            { 103, 205 },
            { 8543, 105 }
        };
        public int Code { get; set; }
        public int SensorStatus { get; set; }
        public bool PowerStatus { get; set; }
        public bool AlarmStatus { get; set; }
        public bool IsAlarmSet{ get; set; }
        public bool NetworkStatus => PowerStatus && IsAlarmSet;
        public Room(int Code, int SensorStatus, bool PowerStatus)
        {
            this.Code = Code;
            this.SensorStatus = SensorStatus;
            this.PowerStatus = PowerStatus;
        }
        public async void TriggerAlarm(int code, Label label){
            AlarmStatus = true;
            SensorStatus = 2;
            label.Dispatcher.Invoke(() => label.Content = "Код комнаты: " + code + " - Состояние: Тревога!");
            
        }
    }
}
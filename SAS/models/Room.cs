using System;
using System.Windows.Controls;
using System.Windows.Shapes;

public class Room {
    public int Code { get; set; }
    public int SensorStatus { get; set; }
    public bool PowerStatus { get; set; }
    public bool AlarmStatus { get; set; }
    public bool NetworkStatus { get; set; }

    public Room(int Code, int SensorStatus, bool PowerStatus, bool AlarmStatus, bool NetworkStatus)
    {
        this.Code = Code;
        this.SensorStatus = SensorStatus;
        this.PowerStatus = PowerStatus;
        this.AlarmStatus = AlarmStatus;
        this.NetworkStatus = NetworkStatus;
    }
    
}
using System;

public class Room {
    public int code { get; set; }
    public bool alarmStatus { get; set; }
    public String power { get; set; }
    public String sensor { get; set; }
    public String network { get; set; }
    public String alarm { get; set; }
    public String textButton  { get; set; }
    public void EnableAlarm() { 
        alarmStatus = true;
    }
}
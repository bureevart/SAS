using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SAS{


    public class SimulateController{

        public async static void Simulate(List<Room> rooms, Label label){
            Random rnd = new Random((int)DateTime.Today.Ticks);

            _ = Task.Run(async () =>
            {
                await Task.Delay(1000);
                var room = rooms[rnd.Next(rooms.ToArray().Length)];
                room.TriggerAlarm(room.Code, label);
            });
        }
    }
}
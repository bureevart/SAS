using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SAS{


    public class SimulateController{

        public static bool OnSimulate { get; set; } = false;

        // public async static void SimulateOnce(List<Room> rooms){
        //     if (OnSimulate) { return; }

        //     OnSimulate = true;

        //     Random rnd = new Random((int)DateTime.Today.Ticks);

        //     _ = Task.Run(async () =>
        //     {
        //         await Task.Delay(1000);
        //         var room = rooms[rnd.Next(rooms.ToArray().Length)];

        //         room.TriggerAlarm();
        //         OnSimulate = false;
        //     });
        // }

        public async static void Simulate(List<Room> rooms)
        {
            if(OnSimulate) { return; }

            OnSimulate = true;

            Random rnd = new Random((int)DateTime.Today.Ticks);

            _ = Task.Run(async () =>
            {
                while (OnSimulate) 
                {
                    await Task.Delay(1000);
                    if (OnSimulate){
                        var room = rooms[rnd.Next(rooms.ToArray().Length)];
                        room.TriggerAlarm();
                    }
                }
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Media;
using System.Net;

namespace SAS
{
    public class SoundController
    {
        public static bool IsMuted { get; set; } = false;
        public static bool IsPlaying { get; set; } = false;
        public static SoundPlayer Player = new SoundPlayer();
        public SoundController(){
            Player.SoundLocation = @"C:\Users\marov\Рабочий стол\4 курс\Сулейманова\SAS-master\SAS\sound.wav";
        }
        public static void Play(){
            if(IsMuted || IsPlaying){
                return;
            }
            IsPlaying = true;
            Console.WriteLine(Player.SoundLocation);
            Player.Play();
        }
        public static void Stop(){
            if(!IsPlaying){
                return;
            }
            IsPlaying = false;
            Player.Stop();
        }
    }
}

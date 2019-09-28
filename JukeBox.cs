using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Media;

namespace SpaceDarkVaders
{
    static class JukeBox
    {
        static Thread MusicThread;
        public static void PlayerLazerSounds()
        {
            SoundPlayer sounds = new SoundPlayer(Properties.Resources.laser6);
            sounds.Play();
        }

        public static void AlienLazerSounds()
        {
            SoundPlayer sounds = new SoundPlayer(Properties.Resources.laser9);
            sounds.Play();
        }

        public static void Explose()
        {
            MediaPlayer backSound = new MediaPlayer();
            backSound.Open(new Uri(@"D:\C#\SpaceDarkVaders\res\audio\explosion.wav"));
            backSound.Play();
        }

        public static void BackgroundMusic()
        {
            MusicThread = new Thread(new ThreadStart(() =>
            {
                MediaPlayer backSound = new MediaPlayer();
                backSound.Open(new Uri(@"D:\C#\SpaceDarkVaders\res\audio\yellow.wav"));
                backSound.Play();
            }));
            MusicThread.Start();
        }

        public static void Dispose()
        {
            MusicThread.Abort();
        }
    }
}

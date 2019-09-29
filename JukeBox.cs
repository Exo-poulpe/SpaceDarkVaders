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
        private static bool _mute = false;

        public static bool Mute { get => _mute; set => _mute = value; }

        public static void PlayerLazerSounds()
        {
            if (!Mute)
            {
                SoundPlayer sounds = new SoundPlayer(Properties.Resources.laser6);
                sounds.Play();
            }
        }

        public static void AlienLazerSounds()
        {
            if (!Mute)
            {
                SoundPlayer sounds = new SoundPlayer(Properties.Resources.laser9);
                sounds.Play();
            }
        }

        public static void Explose()
        {
            if (!Mute)
            {
                MediaPlayer backSound = new MediaPlayer();
                backSound.Open(new Uri(@"D:\C#\SpaceDarkVaders\res\audio\explosion.wav"));
                backSound.Play();
            }
        }

        public static void BackgroundMusic()
        {
            if (!Mute)
            {
                MusicThread = new Thread(new ThreadStart(() =>
                {
                    MediaPlayer backSound = new MediaPlayer();
                    backSound.Open(new Uri(@"D:\C#\SpaceDarkVaders\res\audio\yellow.wav"));
                    backSound.Play();
                }));
                MusicThread.Start();
            }
        }

        public static void Dispose()
        {
            MusicThread.Abort();
            Mute = true;
        }
    }
}

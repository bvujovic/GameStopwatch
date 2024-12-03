using System.Runtime.InteropServices;

namespace GameStopwatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DateTime gameStarted = DateTime.Now;
        private int minutesDisplayed = 0;
        private int gamePlayedAlarm = 20; // (minutes) sound will be heard after spec amount of time
        private DateTime? pauseStarted = null;
        private TimeSpan pausedTime;
        private bool pauseKeyPressed = false;

        // Ovo je osnova za reagovanje na pritiskanje tastera i u slucaju da prozor nije u fokusu
        //* https://stackoverflow.com/questions/63663036/how-to-receive-key-presses-when-out-of-focus-c-sharp-forms
        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);

        public static bool IsKeyPushedDown(Keys vKey)
        {
            return 0 != (GetAsyncKeyState((int)vKey) & 0x8000);
        }

        class X(Keys key, string sound, int secs)
        {
            public DateTime? Start { get; set; } = null;
            public Keys Key { get; set; } = key;
            public string Sound { get; set; } = sound;
            public int Secs { get; set; } = secs;
        }

        readonly List<X> xs =
        [
            // Bounce
            new X(Keys.RShiftKey, @"Sounds\Male_Saying_Arm.wav", 21), // Red Armour
            // Dungeon
            //new X(Keys.Delete, @"c:\Windows\Media\Windows Hardware Fail.wav", 30), // BFG Ammo
            new X(Keys.Delete, @"Sounds\Male_Saying_Ammo.wav", 30), // BFG Ammo
            new X(Keys.End, @"Sounds\Male_Saying_Arm.wav", 20), // Red Armour
            new X(Keys.PageDown, @"c:\Windows\Media\Windows Hardware Insert.wav", 60+45), // 4X
        ];

        private static void PlayPressSound()
        {
            using var soundPlayer = new System.Media.SoundPlayer(@"c:\Windows\Media\ringout.wav");
            soundPlayer.Play();
        }

        //private TimeSpan TimePassed(DateTime start)
        //{
        //    return (DateTime.Now - start) - pausedTime;
        //}

        private void Tim_Tick(object sender, EventArgs e)
        {
            // pauza: pocetak/kraj
            if (IsKeyPushedDown(Keys.Escape))
            {
                if (!pauseKeyPressed)
                {
                    if (!pauseStarted.HasValue) // pause: start
                    {
                        pauseStarted = DateTime.Now;
                        //System.Diagnostics.Debug.WriteLine(pausedTime);
                    }
                    else // pause: end
                    {
                        pausedTime = DateTime.Now - pauseStarted.Value;
                        //System.Diagnostics.Debug.WriteLine("paused: " + pausedTime + "@ " + DateTime.Now);
                        gameStarted += pausedTime;
                        foreach (X x in xs.Where(it => it.Start.HasValue))
                            x.Start += pausedTime;
                        pauseStarted = null;
                    }
                }
                pauseKeyPressed = true;
                PlayPressSound();   
            }
            else
                pauseKeyPressed = false;
            if (pauseStarted.HasValue)
                return;

            int minutes = (int)(DateTime.Now - gameStarted).TotalMinutes;
            //int minutes = (int)TimePassed(gameStarted).TotalMinutes;
            if (minutesDisplayed != minutes)
                lblMinutes.Text = $"{minutesDisplayed = minutes} minutes";

            // dosta igranja
            if (minutes >= gamePlayedAlarm)
            {
                gamePlayedAlarm += 10;
                using var soundPlayer = new System.Media.SoundPlayer(@"c:\Windows\Media\Alarm04.wav");
                soundPlayer.Play();
            }

            // ponisti sve tajmere
            if (IsKeyPushedDown(Keys.Tab))
            {
                foreach (X x in xs)
                    x.Start = null;
                PlayPressSound();
            }

            foreach (X x in xs)
            {
                if (!x.Start.HasValue)
                {
                    // ako tajmer nije vec ukljucen, a pritisnut je njegov taster - pokreni tajmer
                    if (IsKeyPushedDown(x.Key))
                    {
                        x.Start = DateTime.Now;
                        PlayPressSound();
                    }
                }
                else
                    // ako je vreme tajmera isteklo - pusti odgovarajuci zvuk
                    if ((DateTime.Now - x.Start.Value).TotalSeconds >= x.Secs)
                    //if (TimePassed(x.Start.Value).TotalSeconds >= x.Secs)
                {
                    using (var soundPlayer = new System.Media.SoundPlayer(x.Sound))
                        soundPlayer.Play();
                    x.Start = null;
                }
            }
        }
    }
}

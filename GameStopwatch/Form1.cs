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
            new X(Keys.Delete, @"c:\Windows\Media\Windows Hardware Fail.wav", 30), // BFG Ammo
            new X(Keys.End, @"Sounds\Male_Saying_Arm.wav", 20), // Red Armour
            new X(Keys.PageDown, @"c:\Windows\Media\Windows Hardware Insert.wav", 60+45), // 4X
        ];

        private static void PlayPressSound()
        {
            using var soundPlayer = new System.Media.SoundPlayer(@"c:\Windows\Media\ringout.wav");
            //using var soundPlayer = new System.Media.SoundPlayer(@"Sounds\Male_Saying_Arm.wav");
            soundPlayer.Play();
        }

        private void Tim_Tick(object sender, EventArgs e)
        {
            // dosta igranja
            if ((DateTime.Now - gameStarted).TotalMinutes >= 20)
            {
                gameStarted = DateTime.MaxValue;
                using var soundPlayer = new System.Media.SoundPlayer(@"c:\Windows\Media\Alarm04.wav");
                soundPlayer.Play();
            }

            // ponisti sve tajmere
            if (IsKeyPushedDown(Keys.Tab))
                foreach (X x in xs)
                    x.Start = null;

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
                {
                    using (var soundPlayer = new System.Media.SoundPlayer(x.Sound))
                        soundPlayer.Play();
                    x.Start = null;
                }
            }
        }
    }
}

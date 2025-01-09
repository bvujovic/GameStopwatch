using System.Runtime.InteropServices;
using System.Speech.Synthesis;
namespace GameStopwatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbVoices.SelectedIndex = 1;

            //synth.SpeakAsync("It's time");

            //foreach (var v in synth.GetInstalledVoices())
            //    Console.WriteLine(v);

            //using var fs = File.Create("C:\\Users\\bvnet\\Downloads\\x.wav");
            //var synthFormat = new SpeechAudioFormatInfo(11025, AudioBitsPerSample.Eight, AudioChannel.Mono);
            //    //EncodingFormat.Pcm,
            //    //11025, 16, 1, 16000, 2, null);
            //synth.SetOutputToAudioStream(fs, synthFormat);
        }

        private void CmbVoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbVoices.SelectedItem != null)
                    synth.SelectVoice((string)cmbVoices.SelectedItem);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private static readonly SpeechSynthesizer synth = new() { Volume = 100, Rate = 0 };

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
            // Bounce Map
            new X(Keys.RShiftKey, "Shield", 21), // Red Armour

            // Dungeon Map
            new X(Keys.Delete, "Munition", 32), // BFG Ammo
            new X(Keys.End, "Shield", 20), // Red Armour
            new X(Keys.PageDown, "Quad damage", 60+45), // 4X
        ];

        private static void PlayPressSound()
        {
            using var soundPlayer = new System.Media.SoundPlayer(@"c:\Windows\Media\ringout.wav");
            soundPlayer.Play();
        }

        private void Tim_Tick(object sender, EventArgs e)
        {
            // TEST
            //foreach (var k in Enum.GetValues<Keys>())
            //    if (IsKeyPushedDown(k))
            //        System.Diagnostics.Debug.WriteLine(k);

            // pauza: pocetak/kraj
            if (IsKeyPushedDown(Keys.Escape))
            {
                if (!pauseKeyPressed)
                {
                    if (!pauseStarted.HasValue) // pause: start
                        pauseStarted = DateTime.Now;
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
                synth.Speak("It's time");
            }

            // ponisti sve tajmere
            if (IsKeyPushedDown(Keys.OemPipe))
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
                {
                    synth.Speak(x.Sound);
                    x.Start = null;
                }
            }
        }
    }
}

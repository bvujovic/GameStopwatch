using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
namespace GameStopwatch
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private const string dataSetFileName = "ds.xml";
        public static Ds Ds { get; set; } = new Ds();

        private bool procAlreadyStarted = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (procAlreadyStarted = Process.GetProcesses().Count(it => it.ProcessName == Process.GetCurrentProcess().ProcessName) > 1)
                    Close();

                cmbVoices.SelectedIndex = Properties.Settings.Default.IdxVoice;
                minutesBefore = Properties.Settings.Default.MinutesBefore;
                CurrentDate = Properties.Settings.Default.CurrentDate;
                if (CurrentDate.Year < 2020)
                    CurrentDate = DateTime.Today;
                DisplayMinutes();

                Ds.ReadXml(dataSetFileName);

                //synth.SpeakAsync("It's time");

                //foreach (var v in synth.GetInstalledVoices())
                //    Console.WriteLine(v);

                //* Save sound to a file (ChatGPT)
                //using SpeechSynthesizer synth = new();
                //synth.SelectVoiceByHints(VoiceGender.Neutral); // Set voice to male
                //synth.Rate = 1; // Set the speed
                //synth.Volume = 100; // Set volume to 100%
                //string outputFilePath = "shield.wav";
                //synth.SetOutputToWaveFile(outputFilePath);
                //synth.Speak("SHIELD!");
                //Console.WriteLine($"Speech saved to {outputFilePath}");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (procAlreadyStarted)
                return;
            Properties.Settings.Default.IdxVoice = cmbVoices.SelectedIndex;
            //var min = Properties.Settings.Default.MinutesBefore = CalcMinutesTotal(CalcMinutes());
            Properties.Settings.Default.MinutesBefore = CalcMinutesTotal();
            Properties.Settings.Default.CurrentDate = CurrentDate;
            Properties.Settings.Default.Save();

            //var dm = Ds.DateMinutes.FindByDate(DateTime.Today);
            //if (dm == null)
            //    Ds.DateMinutes.AddDateMinutesRow(DateTime.Today, min);
            //else
            //    dm.Minutes = min;
            Ds.WriteXml(dataSetFileName);
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

        private static readonly SpeechSynthesizer synth = new() { Volume = 100, Rate = 3 };

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

        private static string MinToString(int min)
        {
            var m = min % 100;
            return min + " minute" + (m % 10 == 1 && m != 11 ? "" : "s");
        }

        private static void SpeakGameTime(int min)
        {
            synth.Speak(MinToString(min));
        }

        private void Tim_Tick(object sender, EventArgs e)
        {
            // TEST
            //foreach (var k in Enum.GetValues<Keys>())
            //    if (IsKeyPushedDown(k))
            //        System.Diagnostics.Debug.WriteLine(k);

            //int minutes = (int)(DateTime.Now - gameStarted).TotalMinutes;
            int minutes = CalcMinutes();

            // pauza: pocetak/kraj
            if (IsKeyPushedDown(Keys.Escape))
            {
                if (!pauseKeyPressed)
                {
                    if (!pauseStarted.HasValue) // pause: start
                    {
                        pauseStarted = DateTime.Now;
                        SpeakGameTime(minutes);
                    }
                    else // pause: end
                    {
                        pausedTime = DateTime.Now - pauseStarted.Value;
                        //System.Diagnostics.Debug.WriteLine("paused: " + pausedTime + "@ " + DateTime.Now);
                        gameStarted += pausedTime;
                        foreach (X x in xs.Where(it => it.Start.HasValue))
                            x.Start += pausedTime;
                        pauseStarted = null;
                        PlayPressSound();
                    }
                }
                pauseKeyPressed = true;
            }
            else
                pauseKeyPressed = false;
            if (pauseStarted.HasValue)
                return;

            if (minutesDisplayed != minutes)
                DisplayMinutes(minutesDisplayed = minutes);

            // dosta igranja
            if (minutes >= gamePlayedAlarm)
            {
                gamePlayedAlarm += 10;
                //synth.Speak("It's time");
                SpeakGameTime(minutes);
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

        private DateTime currentDate;
        public DateTime CurrentDate
        {
            get { return currentDate; }
            set
            {
                currentDate = value;
                lblCurrentDate.Text = value.ToShortDateString();
            }
        }

        /// <summary>Vreme (min) u aplikaciji pre tekuceg pokretanja.</summary>
        private int minutesBefore;

        /// <summary>Calculate minutes in app. minutesBefore do not count.</summary>
        private int CalcMinutes()
            => (int)(DateTime.Now - gameStarted).TotalMinutes;

        private int CalcMinutesTotal()
            => CalcMinutesTotal(CalcMinutes());

        private int CalcMinutesTotal(int minutesNow)
            => minutesBefore + (tsmiCountInCurrent.Checked ? minutesNow : 0);

        private void DisplayMinutes(int minutesNow)
        {
            lblMinutes.Text = MinToString(minutesNow);
            lblMinutesTotal.Text = MinToString(CalcMinutesTotal(minutesNow));
        }

        private void DisplayMinutes()
            => DisplayMinutes(CalcMinutes());

        private void TsmiCountInCurrent_CheckedChanged(object sender, EventArgs e)
        {
            lblMinutesTotal.Enabled = tsmiCountInCurrent.Checked;
            DisplayMinutes();
        }

        private void TsmiResetTotalTime_Click(object sender, EventArgs e)
        {
            try
            {
                Ds.DateMinutes.AddDateMinutesRow(CurrentDate
                    , CurrentDate.DayOfWeek.ToString(), CalcMinutesTotal());
                minutesBefore = 0;
                gameStarted = DateTime.Now;
                DisplayMinutes();
                CurrentDate = DateTime.Today;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void TsmiChangeBeforeTime_Click(object sender, EventArgs e)
        {
            tim.Stop();
            var frm = new FrmChangeBeforeTime { MinutesBefore = minutesBefore };
            if (frm.ShowDialog() == DialogResult.OK)
                minutesBefore = frm.MinutesBefore;
            DisplayMinutes();
            Thread.Sleep(500); // avoid catching Enter or Escape keypresses from frm and making a sound
            tim.Start();
        }

        private void BtnPastValues_Click(object sender, EventArgs e)
        {
            tim.Stop();
            new FrmPastValues().ShowDialog();
            Thread.Sleep(500); // avoid catching Enter or Escape keypresses from frm and making a sound
            tim.Start();
        }
    }
}

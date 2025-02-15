using System.Diagnostics;
using System.Runtime.InteropServices;
namespace GameStopwatch
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private const string dataSetFileName = "ds.xml";

        public Ds Ds { get; set; } = new Ds();

        private bool procAlreadyStarted = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (procAlreadyStarted = Process.GetProcesses().Count(it => it.ProcessName == Process.GetCurrentProcess().ProcessName) > 1)
                    Close();

                minutesBefore = Properties.Settings.Default.MinutesBefore;
                CurrentDate = Properties.Settings.Default.CurrentDate;
                if (CurrentDate.Year < 2020)
                    CurrentDate = DateTime.Today;
                DisplayMinutes();

                Ds.ReadXml(dataSetFileName);

                cmbVoices.Items.Clear();
                foreach (var v in speaker.Synth.GetInstalledVoices())
                    cmbVoices.Items.Add(v.VoiceInfo.Name);
                cmbVoices.SelectedIndex = Properties.Settings.Default.IdxVoice;

                //synth.SpeakAsync("It's time");

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
            Properties.Settings.Default.MinutesBefore = GetMinutesTotal();
            Properties.Settings.Default.CurrentDate = CurrentDate;
            Properties.Settings.Default.Save();
            Ds.WriteXml(dataSetFileName);
        }

        private void CmbVoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbVoices.SelectedItem != null)
                    speaker.Synth.SelectVoice((string)cmbVoices.SelectedItem);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private readonly Speaker speaker = new();

        private DateTime gameStarted = DateTime.Now;
        private int minutesDisplayed = 0;
        private const int notificationInterval = 10; // (minutes) sound will be heard after spec amount of time
        private int nextTimeNotification = notificationInterval; // (minutes)
        private DateTime? pauseStarted = null;
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

        private static readonly System.Media.SoundPlayer soundPlayer = new(@"c:\Windows\Media\ringout.wav");

        private static void PlayPressSound()
        {
            //using var soundPlayer = new System.Media.SoundPlayer(@"c:\Windows\Media\ringout.wav");
            soundPlayer.Play();
        }

        private static string MinToString(int min)
        {
            var m = min % 100;
            return min + " minute" + (m % 10 == 1 && m != 11 ? "" : "s");
        }

        private void SpeakGameTime(int min)
            => speaker.Speak(MinToString(min));

        private void Tim_Tick(object sender, EventArgs e)
        {
            // TEST
            //foreach (var k in Enum.GetValues<Keys>())
            //    if (IsKeyPushedDown(k))
            //        System.Diagnostics.Debug.WriteLine(k);

            //int minutes = (int)(DateTime.Now - gameStarted).TotalMinutes;
            int minutes = GetMinutes();

            // pauza: pocetak/kraj
            if (IsKeyPushedDown(Keys.Escape))
            {
                if (!pauseKeyPressed)
                {
                    if (!pauseStarted.HasValue) // pause: start
                    {
                        PauseStart();
                        SpeakGameTime(minutes);
                    }
                    else // pause: end
                    {
                        PauseStop();
                        //speaker.Speak(SpeakerSounds.KeyPress);
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
            if (minutes >= nextTimeNotification)
            {
                nextTimeNotification += notificationInterval;
                //synth.Speak("It's time");
                SpeakGameTime(minutes);
            }

            // ponisti sve tajmere
            if (IsKeyPushedDown(Keys.OemPipe))
            {
                foreach (X x in xs)
                    x.Start = null;
                //speaker.Speak(SpeakerSounds.KeyPress);
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
                        //speaker.Speak(SpeakerSounds.KeyPress);
                        PlayPressSound();
                    }
                }
                else
                    // ako je vreme tajmera isteklo - pusti odgovarajuci zvuk
                    if ((DateTime.Now - x.Start.Value).TotalSeconds >= x.Secs)
                {
                    //synth.Speak(x.Sound);
                    //speaker.Speak(SpeakerSounds.Shield);
                    speaker.Speak(x.Sound);
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
        private int GetMinutes()
            => (int)(DateTime.Now - gameStarted).TotalMinutes;

        public int GetMinutesTotal()
            => GetMinutesTotal(GetMinutes());

        private int GetMinutesTotal(int minutesNow)
            => minutesBefore + (tsmiCountInCurrent.Checked ? minutesNow : 0);

        private void DisplayMinutes(int minutesNow)
        {
            lblMinutes.Text = MinToString(minutesNow);
            lblMinutesTotal.Text = MinToString(GetMinutesTotal(minutesNow));
        }

        private void DisplayMinutes()
            => DisplayMinutes(GetMinutes());

        private void TsmiCountInCurrent_CheckedChanged(object sender, EventArgs e)
        {
            lblMinutesTotal.Enabled = tsmiCountInCurrent.Checked;
            DisplayMinutes();
        }

        private void TsmiResetTotalTime_Click(object sender, EventArgs e)
        {
            try
            {
                FillWithZeros();
                Ds.DateMinutes.AddDateMinutesRow(CurrentDate
                    , CurrentDate.DayOfWeek.ToString(), GetMinutesTotal());
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
            PauseStart();
            var frm = new FrmChangeBeforeTime { MinutesBefore = minutesBefore };
            if (frm.ShowDialog() == DialogResult.OK)
                minutesBefore = frm.MinutesBefore;
            DisplayMinutes();
            Thread.Sleep(500); // avoid catching Enter or Escape keypresses from frm and making a sound
            PauseStop();
            tim.Start();
        }

        private void BtnPastValues_Click(object sender, EventArgs e)
        {
            tim.Stop();
            PauseStart();
            new FrmPastValues(this).ShowDialog();
            Thread.Sleep(500); // avoid catching Enter or Escape keypresses from frm and making a sound
            PauseStop();
            tim.Start();
        }

        private void PauseStart()
        {
            if (pauseStarted.HasValue)
                PauseStop();
            pauseStarted = DateTime.Now;
        }

        private void PauseStop()
        {
            if (pauseStarted == null)
                return;
            var pausedTime = DateTime.Now - pauseStarted.Value;
            gameStarted += pausedTime;
            foreach (X x in xs.Where(it => it.Start.HasValue))
                x.Start += pausedTime;
            pauseStarted = null;
        }

        public List<Ds.DateMinutesRow> FillWithZeros()
        {
            if (Ds.DateMinutes.Count == 0)
                return [];
            var nextDate = Ds.DateMinutes.Max(it => it.Date).AddDays(1);
            if (nextDate >= CurrentDate)
                return [];

            var addedRows = new List<Ds.DateMinutesRow>();
            for (var d = nextDate; d < CurrentDate; d = d.AddDays(1))
                addedRows.Add(Ds.DateMinutes.AddDateMinutesRow(d, d.DayOfWeek.ToString(), 0));
            return addedRows;
        }
    }
}

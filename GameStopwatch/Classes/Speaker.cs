using System.Speech.Synthesis;

namespace GameStopwatch.Classes
{
    public class Speaker
    {
        private readonly Queue<string> speakerSounds = [];

        private readonly SpeechSynthesizer synth = new() { Volume = 100, Rate = 2 };

        public SpeechSynthesizer Synth => synth;

        public Speaker()
        {
            synth.SpeakCompleted += Synth_SpeakCompleted;
        }

        private void Synth_SpeakCompleted(object? sender, SpeakCompletedEventArgs e)
        {
            speakerSounds.Dequeue();
            if (speakerSounds.Count > 0)
                synth.SpeakAsync(speakerSounds.Last().ToString());
        }

        public void Speak(string text)
        {
            speakerSounds.Enqueue(text);
            if (synth.State != SynthesizerState.Speaking)
                synth.SpeakAsync(text);
        }

        //public enum SpeakerSounds
        //{
        //    KeyPress,
        //    Munition,
        //    Shield,
        //    QuadDamage
        //}

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
}

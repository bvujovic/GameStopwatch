using System.Speech.Synthesis;

namespace GameStopwatch
{
    //public enum SpeakerSounds
    //{
    //    KeyPress,
    //    Munition,
    //    Shield,
    //    QuadDamage
    //}

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
            //Speak2(speakerSounds.Last().ToString());

            //if (speakerSounds.Count > 0)
            //    synth.SpeakAsync(speakerSounds.Dequeue().ToString());
        }

        public void Speak(string text)
        {
            speakerSounds.Enqueue(text);
            if (synth.State != SynthesizerState.Speaking)
                synth.SpeakAsync(text);
            //Speak2(text);

            //if (synth.State == SynthesizerState.Speaking)
            //    speakerSounds.Enqueue(text);
            //else
            //    synth.SpeakAsync(text);
        }

        //public void Speak(SpeakerSounds sound)
        //{
        //    if (speakerSounds.Count > 0 && speakerSounds.Last() == sound.ToString())
        //        return;
        //    Speak(sound.ToString());
        //}
    }
}

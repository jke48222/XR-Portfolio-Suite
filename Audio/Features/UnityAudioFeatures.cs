using UnityEngine;

namespace XrShared.Audio.Features
{
    public sealed class UnityAudioFeatures : IAudioFeatures
    {
        private readonly float[] _spectrum = new float[64];

        public AudioFeatureFrame GetFrame()
        {
            AudioFeatureFrame f = new AudioFeatureFrame();

            AudioListener.GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);

            float low = 0f, mid = 0f, high = 0f;
            for (int i = 0; i < _spectrum.Length; i++)
            {
                float v = _spectrum[i];
                if (i < 10) low += v;
                else if (i < 30) mid += v;
                else high += v;
            }

            f.low = low;
            f.mid = mid;
            f.high = high;

            f.rms = Mathf.Sqrt((low * low + mid * mid + high * high) / 3f);
            return f;
        }
    }
}

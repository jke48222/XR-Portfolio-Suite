using System;

namespace XrShared.Audio.Features
{
    [Serializable]
    public struct AudioFeatureFrame
    {
        public float rms;
        public float low;
        public float mid;
        public float high;
    }
}

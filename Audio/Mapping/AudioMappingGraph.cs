using UnityEngine;

namespace XrShared.Audio.Mapping
{
    [CreateAssetMenu(menuName = "XR Shared/Audio Mapping Graph", fileName = "AudioMappingGraph")]
    public class AudioMappingGraph : ScriptableObject
    {
        [Range(0f, 10f)] public float lowGain = 1f;
        [Range(0f, 10f)] public float midGain = 1f;
        [Range(0f, 10f)] public float highGain = 1f;

        [Range(0f, 1f)] public float smoothing = 0.2f;
    }
}

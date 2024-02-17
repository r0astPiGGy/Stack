using UnityEngine;

namespace Audio.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Settings/Audio Settings")]
    public class AudioSettingsSO : ScriptableObject
    {

        [SerializeField] private AudioClip startSound;
        [SerializeField] private AudioClip placeSound;
        [SerializeField] private AudioClip failSound;
        [SerializeField] private AudioClip cutoffSound;

        public AudioClip StartSound => startSound;
        public AudioClip PlaceSound => placeSound;
        public AudioClip FailSound => failSound;
        public AudioClip CutoffSound => cutoffSound;

    }
}
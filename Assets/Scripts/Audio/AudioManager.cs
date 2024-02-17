using Audio.ScriptableObjects;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource source;

        public void PlaySound(AudioClip clip, float pitch = 1f)
        {
            source.Stop();
            if (clip == null) return;

            source.pitch = pitch;
            source.PlayOneShot(clip);
        }
    }
}
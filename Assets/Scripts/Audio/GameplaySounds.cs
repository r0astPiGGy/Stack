using System;
using Audio.ScriptableObjects;
using Events.ScriptableObjects;
using UnityEngine;

namespace Audio
{
    public class GameplaySounds : MonoBehaviour
    {

        [SerializeField] private AudioManager audioManager;
        [SerializeField] private AudioSettingsSO settings;
        [SerializeField] private int maxStreakPitch = 10;
        
        [Header("Listening on")] 
        [SerializeField] private IntEventChannelSO onStreakChanged;
        [SerializeField] private VoidEventChannelSO onGameStarted;
        [SerializeField] private IntEventChannelSO onGameOver;
        
        private void OnEnable()
        {
            onStreakChanged.OnEventRaised += OnStreakChanged;
            onGameStarted.OnEventRaised += PlayStartSound;
            onGameOver.OnEventRaised += PlayFailSound;
        }

        private void OnDisable()
        {
            onStreakChanged.OnEventRaised -= OnStreakChanged;
            onGameStarted.OnEventRaised -= PlayStartSound;
            onGameOver.OnEventRaised -= PlayFailSound;
        }

        private void PlayStartSound()
        {
            audioManager.PlaySound(settings.StartSound);
        }

        private void PlayFailSound(int score)
        {
            audioManager.PlaySound(settings.FailSound);
        }

        private void OnStreakChanged(int streak)
        {
            if (streak > 0)
            {
                PlayPerfectSound(streak);
            }
            else
            {
                PlayCutoffSound();
            }
        }

        private void PlayPerfectSound(int streak)
        {
            var clampedStreak = (streak - 1) % maxStreakPitch;
            var pitch = clampedStreak / maxStreakPitch * 2 + 1;
            
            audioManager.PlaySound(settings.PlaceSound, pitch);
        }

        private void PlayCutoffSound()
        {
            audioManager.PlaySound(settings.CutoffSound);
        }
    }
}
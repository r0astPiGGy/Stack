using System;
using Events.ScriptableObjects;
using UnityEngine;

namespace Gameplay
{
    public class StreakController : MonoBehaviour
    {

        [Header("Broadcasting on")]
        [SerializeField] private IntEventChannelSO streakChangedEvent;

        [Header("Listening on")] 
        [SerializeField] private TilePlaceResultEventChannelSO onTilePlaceResult;

        private int _streak;

        private int Streak
        {
            get => _streak;
            set
            {
                _streak = value;
                
                if (streakChangedEvent != null)
                    streakChangedEvent.RaiseEvent(value);
            }
        }
        
        private void OnEnable()
        {
            if (onTilePlaceResult != null)
            {
                onTilePlaceResult.OnEventRaised += OnTilePlaceResult;
            }
        }

        private void OnDisable()
        {
            if (onTilePlaceResult != null)
            {
                onTilePlaceResult.OnEventRaised -= OnTilePlaceResult;
            }
        }

        private void OnTilePlaceResult(TilePlaceResult result)
        {
            switch (result)
            {
                case TilePlaceResult.Perfect:
                    Streak++;
                    break;
                case TilePlaceResult.Sliced:
                    Streak = 0;
                    break;
                case TilePlaceResult.Cancelled:
                case TilePlaceResult.Missed:
                default: break;
            }
        }
    }
}
using System;
using Events.ScriptableObjects;
using UnityEngine;
using Utilities.ScriptableObjects;

namespace Gameplay
{
    public class TileSpeedController : MonoBehaviour
    {

        [SerializeField] private FloatVariable speed;
        [SerializeField] private float initialSpeed = 1.5f;
        
        [Header("Listening on")]
        [SerializeField] private IntEventChannelSO scoreChangedEvent;
        
        private void Start()
        {
            speed.value = initialSpeed;
        }
        
        private void OnEnable()
        {
            if (scoreChangedEvent != null)
            {
                scoreChangedEvent.OnEventRaised += OnScoreChanged;
            }
        }

        private void OnDisable()
        {
            if (scoreChangedEvent != null)
            {
                scoreChangedEvent.OnEventRaised += OnScoreChanged;
            }
        }

        private void OnScoreChanged(int score)
        {
            
        }
    }
}
using System;
using DG.Tweening;
using Events.ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    public class AscendingBehaviour : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;

        [Header("Move object up after the specified score is reached")]
        [SerializeField] private float moveAfter = 3;
        [SerializeField] private float moveAmount = 0.6f;

        [Header("Listening on")] 
        [SerializeField] private IntEventChannelSO onScoreChanged;
        
        private Tweener _animation;

        private void OnEnable()
        {
            if (onScoreChanged != null)
            {
                onScoreChanged.OnEventRaised += OnScoreChanged;
            }
        }

        private void OnDisable()
        {
            if (onScoreChanged != null)
            {
                onScoreChanged.OnEventRaised -= OnScoreChanged;
            }
        }

        private void OnScoreChanged(int score)
        {
            if (score > moveAfter)
            {
                MoveUp(moveAmount);   
            }
        }

        private void MoveUp(float amount)
        {
            if (_animation is { active: true })
            {
                _animation.Kill(complete: true);
            }

            _animation = transform
                .DOLocalMoveY(
                    transform.localPosition.y + amount,
                    speed
                );
        }
        
    }
}
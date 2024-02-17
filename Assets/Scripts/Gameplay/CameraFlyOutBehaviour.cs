using System;
using DG.Tweening;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Gameplay
{
    
    public class CameraFlyOutBehaviour : MonoBehaviour
    {
        
        [SerializeField] private int flyOutAtScore = 15;
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private ValueInterpolator flyOutSize; // 75, 25, 10

        [Header("Listening on")] 
        [SerializeField] private IntEventChannelSO onGameOver;

        private Camera _camera;
        
        private void Awake()
        {
            if (!TryGetComponent(out _camera))
            {
                _camera = Camera.main;
            }
        }

        private void OnEnable()
        {
            if (onGameOver != null)
            {
                onGameOver.OnEventRaised += OnGameOver;
            }
        }

        private void OnDisable()
        {
            if (onGameOver != null)
            {
                onGameOver.OnEventRaised -= OnGameOver;
            }
        }

        private void OnGameOver(int score)
        {
            if (score > flyOutAtScore)
            {
                FlyOut(score);
            }
        }

        private void FlyOut(int score)
        {
            DOTween.To(
                getter: () => _camera.orthographicSize,
                setter: value => _camera.orthographicSize = value,
                endValue: flyOutSize.Interpolate(score),
                duration: speed
            );   
        }
    }
}
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Gameplay
{
    public class ObjectPositionAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject objectToAnimate;
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private Ease ease;
        [SerializeField] private bool animateFromStartPosition;
        
        private Tweener _animation;

        [HideInInspector]
        public UnityEvent onAnimationEnd = new();

        public bool IsRunning => _animation?.active == true;

        public void Run()
        {
            Stop();

            var start = objectToAnimate.transform.localPosition;
            var end = start + positionOffset;

            if (!animateFromStartPosition)
            {
                (start, end) = (end, start);
            }
            
            _animation = objectToAnimate.transform
                .DOLocalMove(endValue: end, duration: duration)
                .SetDelay(delay)
                .From(start)
                .SetEase(ease)
                .OnComplete(OnStopped);
        }

        private void OnStopped()
        {
            _animation = null;
            onAnimationEnd?.Invoke();
        }

        public void Stop()
        {
            if (!IsRunning) return;
            
            _animation.Kill(complete: true);
            _animation = null;
        }
        
    }
}
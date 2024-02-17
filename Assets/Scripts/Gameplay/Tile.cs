using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(ColorModifiable))]
    public class Tile : MonoBehaviour
    {
        
        private ColorModifiable _colorModifiable;
        private Tweener _platformAnimation;

        public float Height => transform.localScale.y;
        
        private void Awake()
        {
            _colorModifiable = GetComponent<ColorModifiable>();
        }

        public void StartMoving(Vector3 start, Vector3 end, float speed)
        {
            StopMoving();
            
            _platformAnimation = transform
                .DOLocalMove(start, speed)
                .From(end)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear);
        }

        public void StopMoving()
        {
            if (_platformAnimation == null) return;
            
            _platformAnimation.Kill();
            _platformAnimation = null;
        }

        public void SetColor(Color color)
        {
            _colorModifiable.SetColor(color);
        }
    }
}

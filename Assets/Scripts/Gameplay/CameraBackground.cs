using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class CameraBackground : MonoBehaviour
    {
        
        [SerializeField] private Color topColor;
        [SerializeField] private Color bottomColor;

        private readonly int _topColorId = Shader.PropertyToID("_TopColor");
        private readonly int _bottomColorId = Shader.PropertyToID("_BottomColor");
        
        private Renderer _renderer;
        private Material _material;
        
        void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _material = new Material(_renderer.material);
            _renderer.material = _material;   
        }
        
        // Start is called before the first frame update
        void Start()
        {
            SetTopColor(topColor);
            SetBottomColor(bottomColor);
        }

        public Color GetTopColor() => topColor;
        
        public Color GetBottomColor() => bottomColor;
        
        public void SetTopColor(Color color)
        {
            topColor = color;
            _material.SetColor(_topColorId, color);
        }

        public void SetBottomColor(Color color)
        {
            bottomColor = color;
            _material.SetColor(_bottomColorId, color);
        }
        
    }
}
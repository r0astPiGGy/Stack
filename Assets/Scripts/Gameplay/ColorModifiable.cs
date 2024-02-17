using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class ColorModifiable : MonoBehaviour
    {
        [SerializeField] private Color color;
        
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
            SetMaterialColor(color);
        }

        // private void OnValidate()
        // {
        //     var render = GetComponent<Renderer>();
        //     if (render == null || render.sharedMaterial == null) return;
        //
        //     _renderer = render;
        //     SetMaterialColor(color);
        // }

        public void SetColor(Color materialColor)
        {
            color = materialColor;
            SetMaterialColor(materialColor);
        }

        public Color GetColor() => color;
        
        private IEnumerator SetColorRoutine(Color materialColor)
        {
            _material.color = materialColor;
            
            yield return null;
        }

        private void SetMaterialColor(Color materialColor)
        {
            if (_material == null)
            {
                StartCoroutine(SetColorRoutine(materialColor));   
            }
            else
            {
                _material.color = materialColor;
            }
        }
    }
}
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Gameplay
{
    public class TileColorProvider : MonoBehaviour
    {

        [Range(1, 100)]
        [SerializeField] private int interpolationStep = 25;
        
        [Inject]
        private IThemeHolder _themeHolder;
        private int _counter;
        private int _cachedThemeHash;

        public Color Next()
        {
            _counter++;
            
            var currentThemeHash = _themeHolder.Current.GetHashCode();

            if (_cachedThemeHash != currentThemeHash)
            {
                _cachedThemeHash = currentThemeHash;
                _counter = 0;
            }

            var halfStep = interpolationStep / 2;

            var currentColor = GetCurrentColor(_counter);

            if (_counter < halfStep) 
                return currentColor;
            
            var nextColor = _themeHolder.Next.TileColor;
            var shadeDelta = (float) halfStep / 255;
            var shadedNextColor = Shade(nextColor, shadeDelta);
                
            var delta = (float) (_counter - halfStep) / interpolationStep;
                
            return Color.Lerp(currentColor, shadedNextColor, delta * 2);
        }

        private Color GetCurrentColor(int counter)
        {
            var shadeStep = (counter + 1) * (interpolationStep / 2);
            var delta = (float) shadeStep / 255;
            return Shade(_themeHolder.Current.TileColor, delta);
        }
        
        private static Color Shade(Color color, float delta)
        {
            return new Color
            {
                r = AddDifference(color.r, delta),
                g = AddDifference(color.g, delta),
                b = AddDifference(color.b, delta)
            };
        }

        private static float AddDifference(float value, float delta)
        {
            return value < 0.5 ? value + delta : value - delta;
        }
    }
}
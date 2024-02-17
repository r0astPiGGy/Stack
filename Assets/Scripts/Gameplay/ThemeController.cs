using DG.Tweening;
using Events.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class ThemeController : MonoBehaviour
    {

        [SerializeField] private float fastAnimationDuration = 1f;
        [SerializeField] private float smoothAnimationDuration = 4f;
        [SerializeField] private CameraBackground cameraBackground;
        [SerializeField] private ColorModifiable tower;
        [SerializeField] private float changeThemeEvery = 20f;

        [Header("Listening on")]
        [SerializeField] private IntEventChannelSO onScoreChange;

        [Inject]
        private IThemeHolder _themeHolder;

        private void OnEnable()
        {
            if (onScoreChange != null)
            {
                onScoreChange.OnEventRaised += OnScoreChanged;
            }
        }

        private void OnDisable()
        {
            if (onScoreChange != null)
            {
                onScoreChange.OnEventRaised -= OnScoreChanged;
            }
        }

        private void Start()
        {
            // Change tower color according to theme
            _themeHolder.Reset();
            var previous = _themeHolder.Previous;
            _themeHolder.MoveNext();
            var current = _themeHolder.Current;

            if (previous != null)
            {
                // Fast animate background using previous background color according to theme
                cameraBackground.SetTopColor(previous.BackgroundTopColor);
                cameraBackground.SetBottomColor(previous.BackgroundBottomColor);
               
                FastAnimateBackground(current.BackgroundTopColor, current.BackgroundBottomColor);
            }
            else
            {
                cameraBackground.SetTopColor(current.BackgroundTopColor);
                cameraBackground.SetBottomColor(current.BackgroundBottomColor);
            }
            
            tower.SetColor(current.TowerColor);
        }
        
        private void OnScoreChanged(int score)
        {
            if (score == 0 || score % changeThemeEvery != 0) return;
            
            _themeHolder.MoveNext();
            
            AnimateBackground(_themeHolder.Current.BackgroundTopColor, _themeHolder.Current.BackgroundBottomColor);
        }

        private void FastAnimateBackground(Color topColor, Color bottomColor)
        {
            DOTween.To(getter: () => cameraBackground.GetTopColor(), setter: value => cameraBackground.SetTopColor(value), topColor, fastAnimationDuration);
            DOTween.To(getter: () => cameraBackground.GetBottomColor(), setter: value => cameraBackground.SetBottomColor(value), bottomColor, fastAnimationDuration);
        }

        private void AnimateBackground(Color topColor, Color bottomColor)
        {
            DOTween.To(getter: () => cameraBackground.GetTopColor(), setter: value => cameraBackground.SetTopColor(value), topColor, smoothAnimationDuration);
            DOTween.To(getter: () => cameraBackground.GetBottomColor(), setter: value => cameraBackground.SetBottomColor(value), bottomColor, smoothAnimationDuration);
        }
        
    }
}
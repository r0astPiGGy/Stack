using System;
using Events.ScriptableObjects;
using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI.Controllers
{
    public class GameOverScreenController : MonoBehaviour
    {
        [SerializeField] private ObjectPositionAnimator cameraPositionAnimator;
        
        [Header("Listening on")]
        [SerializeField] private IntEventChannelSO gameOverEvent;
        [SerializeField] private VoidEventChannelSO gameRestartEvent;

        [Header("Broadcasting on")] 
        [SerializeField] private VoidEventChannelSO onHideGameOverScreenUI;
        [SerializeField] private GameOverResultEventChannelSO onUpdateGameOverUI;

        [Inject] private ISaveManager _saveManager;
        
        private void OnEnable()
        {
            gameOverEvent.OnEventRaised += OnGameOver;
            gameRestartEvent.OnEventRaised += OnGameRestart;
            cameraPositionAnimator.onAnimationEnd.AddListener(OnCameraAnimationEnded);
        }

        private void OnGameRestart()
        {
            if (cameraPositionAnimator.IsRunning) return;
            
            // Explode the tower may be
            // onShowHomeScreen.RaiseEvent();
            cameraPositionAnimator.Run();
            onHideGameOverScreenUI.RaiseEvent();
        }

        private void OnCameraAnimationEnded()
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);   
        }
        
        private void OnGameOver(int score)
        {
            var gameData = _saveManager.Load();

            if (score > gameData.highScore)
            {
                var newGameData = new GameData { highScore = score };
                _saveManager.Save(newGameData);
            }

            var result = new GameOverResult(gameData, score);
            onUpdateGameOverUI.RaiseEvent(result);
        }

        private void OnDisable()
        {
            gameOverEvent.OnEventRaised -= OnGameOver;
            gameRestartEvent.OnEventRaised -= OnGameRestart;
            cameraPositionAnimator.onAnimationEnd.RemoveListener(OnCameraAnimationEnded);
        }
    }
}
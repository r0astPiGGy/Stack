using System;
using System.Collections;
using Events.ScriptableObjects;
using Gameplay;
using UI.Screens;
using UI.View;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]
    public class UIManager : MonoBehaviour
    {
        private UIDocument _document;
        private HomeScreen _homeScreen;
        private GameScreen _gameScreen;

        private UIView _currentScreen;

        [Header("Listening on")]
        [SerializeField] private IntEventChannelSO onScoreChanged;
        [SerializeField] private GameOverResultEventChannelSO onGameOverResult;
        [SerializeField] private VoidEventChannelSO onShowHomeScreen;
        [SerializeField] private VoidEventChannelSO onShowGameScreen;
        [SerializeField] private VoidEventChannelSO onHideGameOverScreenUI;

        [Header("Broadcasting on")]
        [SerializeField] private VoidEventChannelSO playClickedEvent;
        [SerializeField] private VoidEventChannelSO tilePlacedEvent;
        [SerializeField] private VoidEventChannelSO restartClickedEvent;
        
        private void OnEnable()
        {
            _document = GetComponent<UIDocument>();

            SetupViews();
            RegisterCallbacks();
            
            _gameScreen.HideImmediately();
        }

        private void Start()
        {
            StartCoroutine(AnimateShowHomeScreen());
        }

        private IEnumerator AnimateShowHomeScreen()
        {
            _homeScreen.HideImmediately();
            yield return null;
            _homeScreen.Hide();
            yield return null;
            ShowScreen(_homeScreen);
        }

        private void RegisterCallbacks()
        {
            onGameOverResult.OnEventRaised += OnGameOver;
            onShowGameScreen.OnEventRaised += ShowGameScreen;
            onShowHomeScreen.OnEventRaised += ShowHomeScreen;
            
            if (_gameScreen != null)
            {
                onScoreChanged.OnEventRaised += _gameScreen.OnScoreChanged;
                onHideGameOverScreenUI.OnEventRaised += _gameScreen.AnimateHideUI;
                _gameScreen.RestartAction += OnRestartClicked;
                _gameScreen.TilePlaceAction += OnTilePlace;   
            }

            if (_homeScreen != null)
            {
                _homeScreen.StartAction += OnPlayClicked;                
            }
        }

        private void UnregisterCallbacks()
        {
            onShowGameScreen.OnEventRaised -= ShowGameScreen;
            onShowHomeScreen.OnEventRaised -= ShowHomeScreen;
            onGameOverResult.OnEventRaised -= OnGameOver;

            if (_gameScreen != null)
            {
                onScoreChanged.OnEventRaised -= _gameScreen.OnScoreChanged; 
                onHideGameOverScreenUI.OnEventRaised -= _gameScreen.AnimateHideUI;
                _gameScreen.RestartAction -= OnRestartClicked;
                _gameScreen.TilePlaceAction -= OnTilePlace;   
            }

            if (_homeScreen != null)
            {
                _homeScreen.StartAction -= OnPlayClicked;   
            }
        }

        private void SetupViews()
        {
            _gameScreen = new GameScreen(_document.rootVisualElement.Q("GameScreen"));
            _gameScreen.Instantiate();

            _homeScreen = new HomeScreen(_document.rootVisualElement.Q("HomeScreen"));
            _homeScreen.Instantiate();
        }

        private void ShowScreen(UIView screen)
        {
            _currentScreen?.Hide();
            _currentScreen = screen;
            _currentScreen.Show();
        }

        private void ShowGameScreen() => ShowScreen(_gameScreen);

        private void ShowHomeScreen() => ShowScreen(_homeScreen);

        private void OnGameOver(GameOverResult gameOverResult)
        {
            _gameScreen.OnGameOver(gameOverResult);
        }
        
        private void OnPlayClicked()
        {
            playClickedEvent.RaiseEvent();
        }

        private void OnTilePlace()
        {
            tilePlacedEvent.RaiseEvent();
        }
        
        private void OnRestartClicked()
        {
            restartClickedEvent.RaiseEvent();
        }

        private void OnDisable()
        {
            UnregisterCallbacks();
            
            _homeScreen.Dispose();
            _gameScreen.Dispose();
        }
    }
}
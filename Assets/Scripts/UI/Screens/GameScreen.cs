using System;
using System.Collections.Generic;
using Gameplay;
using UI.View;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class GameScreen : UIView
    {
        private const string AnimatedElement = "animated-visibility";
        private const string Hidden = "hidden";
        private const string Visible = "visible";

        private List<VisualElement> _animatedItems;

        private VisualElement _highScoreContainer;
        private Label _highScoreLabel;
        private Label _scoreLabel;
        private Label _newRecordLabel;
        private VisualElement _footerContainer;
        
        public Action TilePlaceAction = default;
        public Action RestartAction = default;

        public readonly IGameScreenState PlayState;
        public readonly IGameScreenState GameOverState;
        public readonly IGameScreenState Idle = new GameScreenState();
        
        private IGameScreenState _currentState;
        public IGameScreenState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState.HideUI();
                _currentState = value;
                _currentState.ShowUI();
            }
        }
        
        private bool IsAnimatedElementTransitioning { get; set; }

        public GameScreen(VisualElement root) : base(root)
        {
            PlayState = new GameScreenState(handleScreenClick: OnTilePlaced);
            GameOverState = new GameScreenState(handleScreenClick: OnRestart);
            _currentState = PlayState;
        }

        public override void SetVisualElements()
        {
            _scoreLabel = Root.Q<Label>("score");
            Hide(_scoreLabel);
            
            _highScoreLabel = Root.Q<Label>("high-score");
            _highScoreContainer = Root.Q<VisualElement>("high-score-container");
            _footerContainer = Root.Q<VisualElement>("footer");
            _newRecordLabel = Root.Q<Label>("new-record");

            _animatedItems = Root.Query(className: AnimatedElement).Build().ToList();
            
            foreach (var visualElement in _animatedItems)
            {
                visualElement.RegisterCallback<TransitionEndEvent>(OnTransitionEnd);   
            }
        }

        private void OnTransitionEnd(TransitionEndEvent evt)
        {
            if (evt.target is not VisualElement target) return;
               
            if (!target.ClassListContains(AnimatedElement)) return;
            
            IsAnimatedElementTransitioning = false;    

            if (target.ClassListContains(Hidden))
            {
                Hide(target);
            }
        }

        public override void RegisterCallbacks()
        {
            Root.RegisterCallback<PointerDownEvent>(OnClick);
        }

        public void OnGameOver(GameOverResult result)
        {
            var score = result.Score;
            var highScore = result.GameData.highScore;
            
            if (!_scoreLabel.ClassListContains(AnimatedElement))
            {
                _scoreLabel.AddToClassList(AnimatedElement);
            }
            
            if (score == 0)
            {
                AnimateShow(_scoreLabel);
            }
            
            _scoreLabel.text = score.ToString();
            CurrentState = GameOverState;
            
            IsAnimatedElementTransitioning = true;
            if (score > highScore)
            {
                AnimateShow(_newRecordLabel);   
            }
            else
            {
                _highScoreLabel.text = highScore.ToString();
                AnimateShow(_highScoreContainer);
            }
            AnimateShow(_footerContainer);
        }

        public void AnimateHideUI()
        {
            OnHideGameOverUI();
            AnimateHide(_scoreLabel);
        }
        
        private void OnHideGameOverUI()
        {
            IsAnimatedElementTransitioning = true;
            _animatedItems.ForEach(AnimateHide);
        }

        private void AnimateShow(VisualElement element)
        {
            Show(element);
            element.RemoveFromClassList(Hidden);
            element.AddToClassList(Visible);
        }

        private void AnimateHide(VisualElement element)
        {
            element.RemoveFromClassList(Visible);
            element.AddToClassList(Hidden);
        }

        private void OnClick(PointerDownEvent evt)
        {
            _currentState.HandleScreenClick();
        }

        private void OnRestart()
        {
            if (IsAnimatedElementTransitioning) return;
            
            RestartAction();
            CurrentState = Idle;
        }

        private void OnTilePlaced() => TilePlaceAction();

        public void OnScoreChanged(int score)
        {
            if (score > 0 && _scoreLabel.ClassListContains(Hidden))
            {
                _scoreLabel.RemoveFromClassList(AnimatedElement);
                _scoreLabel.RemoveFromClassList(Hidden);
                Show(_scoreLabel);
            }
            
            _scoreLabel.text = score.ToString();
        }

        public override void Dispose()
        {
            Root.UnregisterCallback<PointerDownEvent>(OnClick);
        }

        private class GameScreenState : IGameScreenState
        {
            private readonly Action _hideUI;
            private readonly Action _showUI;
            private readonly Action _handleScreenClick;

            public GameScreenState(Action hideUI = default, Action showUI = default, Action handleScreenClick = default)
            {
                _hideUI = hideUI;
                _showUI = showUI;
                _handleScreenClick = handleScreenClick;
            }

            public void HideUI() => _hideUI?.Invoke();

            public void ShowUI() => _showUI?.Invoke();

            public void HandleScreenClick() => _handleScreenClick?.Invoke();
        }
        
        public interface IGameScreenState
        {
            void HideUI();

            void ShowUI();

            void HandleScreenClick();
        }
    }
}
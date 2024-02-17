using System;
using Events.ScriptableObjects;
using Gameplay;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;
using Utilities.ScriptableObjects;
using Random = System.Random;

namespace UI.Controllers
{
    public class GameScreenController : MonoBehaviour
    {
        [SerializeField] private TileStack tileStack;
        
        [Header("Broadcasting on")]
        [SerializeField] private IntEventChannelSO onScoreChanged;
        [SerializeField] private IntEventChannelSO onGameOver;
        [SerializeField] private TilePlaceResultEventChannelSO onTilePlaceResult;

        [Header("Listening on")]
        [SerializeField] private VoidEventChannelSO showGameScreenEvent;
        [SerializeField] private VoidEventChannelSO tilePlaceEvent;

        private int _score;

        private int Score
        {
            get => _score;
            set
            {
                _score = value;
                onScoreChanged.RaiseEvent(value);
            }
        }
        
        private void OnEnable()
        {
            showGameScreenEvent.OnEventRaised += HandleGameStart;
            tilePlaceEvent.OnEventRaised += HandleTilePlace;
        }

        private void OnDisable()
        {
            showGameScreenEvent.OnEventRaised -= HandleGameStart;
            tilePlaceEvent.OnEventRaised -= HandleTilePlace;
        }

        // Called when ShowGameScreen event has been triggered
        private void HandleGameStart()
        {
            tileStack.CreateNextTile();
        }
        
        // Called when user touches screen
        private void HandleTilePlace()
        {
            var result = tileStack.PlaceCurrentTile();
            
            onTilePlaceResult.RaiseEvent(result);

            if (result == TilePlaceResult.Missed)
            {
                OnGameOver();
                return;
            }

            Score++;
            tileStack.CreateNextTile();
        }

        private void OnGameOver()
        {
            tileStack.OnGameOver();
            onGameOver.RaiseEvent(Score);
        }
    }
}
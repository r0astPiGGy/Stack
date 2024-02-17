using System;
using Events.ScriptableObjects;
using Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Controllers
{
    public class MainScreenController : MonoBehaviour
    {
        [SerializeField] private ObjectPositionAnimator cameraPositionAnimator;
        
        [Header("Listening on")]
        [SerializeField] private VoidEventChannelSO gameStartEvent;
        
        [Header("Broadcasting on")] 
        [SerializeField] private VoidEventChannelSO onShowGameScreen;

        private void OnEnable()
        {
            if (cameraPositionAnimator != null)
            {
                cameraPositionAnimator.Run();   
            }
            gameStartEvent.OnEventRaised += StartGame;
        }

        private void StartGame()
        {
            if (cameraPositionAnimator != null && cameraPositionAnimator.IsRunning)
                return;
            
            onShowGameScreen.RaiseEvent();
        }

        private void OnDisable()
        {
            gameStartEvent.OnEventRaised -= StartGame;
        }
    }
}
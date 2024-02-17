using Gameplay;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/Game Over Result Channel")]
    public class GameOverResultEventChannelSO : ScriptableObject
    {

        public UnityAction<GameOverResult> OnEventRaised = null; 
        
        public void RaiseEvent(GameOverResult gameOverResult)
        {
            OnEventRaised?.Invoke(gameOverResult);
        }
        
    }
}
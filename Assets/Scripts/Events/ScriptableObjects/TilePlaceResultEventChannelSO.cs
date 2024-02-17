using Gameplay;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/Tile Place Result Event Channel")]
    public class TilePlaceResultEventChannelSO : ScriptableObject
    {

        public UnityAction<TilePlaceResult> OnEventRaised = null; 
        
        public void RaiseEvent(TilePlaceResult result)
        {
            OnEventRaised?.Invoke(result);
        }
        
    }
}
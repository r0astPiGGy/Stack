using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/Int Event Channel")]
    public class IntEventChannelSO : ScriptableObject
    {

        public UnityAction<int> OnEventRaised = null; 
        
        public void RaiseEvent(int value)
        {
            OnEventRaised?.Invoke(value);
        }
        
    }
}
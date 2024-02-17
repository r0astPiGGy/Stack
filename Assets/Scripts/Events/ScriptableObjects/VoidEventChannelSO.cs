using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventChannelSO : ScriptableObject
    {

        public UnityAction OnEventRaised = null; 
        
        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
        
    }
}
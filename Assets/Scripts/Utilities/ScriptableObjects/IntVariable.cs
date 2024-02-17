using UnityEngine;

namespace Utilities.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Settings/Int Variable")]
    public class IntVariable : ScriptableObject
    {
        [SerializeField] public int value;
    }
}
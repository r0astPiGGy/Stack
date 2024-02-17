using UnityEngine;

namespace Utilities.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Settings/Float Variable")]
    public class FloatVariable : ScriptableObject
    {
        [SerializeField] public float value;
    }
}
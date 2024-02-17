using System;
using UnityEngine;

namespace Utilities.ScriptableObjects
{
    [Serializable]
    public class FloatReference
    {
        [SerializeField] private float constantValue;
        [SerializeField] private FloatVariable variable;
        [SerializeField] private bool constant;

        public float Value
        {
            get => constant ? constantValue : variable.value;
            // set
            // {
            //     if (variable != null)
            //     {
            //         variable.value = value;
            //     }
            // }
        }
    }
}
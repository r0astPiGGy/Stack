using System;
using UnityEngine;

namespace Utilities.ScriptableObjects
{
    [Serializable]
    public class IntReference
    {
        [SerializeField] private int constantValue;
        [SerializeField] private IntVariable variable;
        [SerializeField] private bool constant;

        public int Value
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
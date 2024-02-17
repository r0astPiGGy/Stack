using System;
using UnityEngine;

namespace Gameplay
{
    [Serializable]
    public struct TilePositionRange
    {
    
        public Vector3 Start => start;
        public Vector3 End => end;
    
        [SerializeField]
        private Vector3 start;
    
        [SerializeField]
        private Vector3 end;
    }
}
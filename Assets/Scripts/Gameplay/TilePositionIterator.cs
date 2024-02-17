using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Gameplay
{
    [Serializable]
    public class TilePositionIterator
    {
        [SerializeField] private TilePositionRange[] ranges;
        
        private IEnumerator<TilePositionRange> _iterator;
        private TilePositionRange _currentRange;
        
        public void Reset()
        {
            _iterator?.Dispose();
            _iterator = InfiniteIterator.Of(ranges).GetEnumerator();
        }
        
        public TilePositionRange NextPosition()
        {
            if (_iterator == null)
            {
                Reset();
            }
            
            _iterator!.MoveNext();
            _currentRange = _iterator.Current;

            return _currentRange;
        }
        
        
    }
}
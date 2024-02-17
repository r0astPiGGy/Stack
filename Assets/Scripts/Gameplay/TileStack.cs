using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Utilities.ScriptableObjects;

namespace Gameplay
{
    public class TileStack : MonoBehaviour
    {
        [Header("Tile settings")]
        [SerializeField] private FloatReference tileSpeed;
        [SerializeField] private float errorMargin = 0.1f;
        [SerializeField] private TilePositionIterator tilePositionIterator;
        [SerializeField] private RubbleFactory rubbleFactory;
        [SerializeField] private TileFactory tileFactory;
        
        private readonly LinkedList<Tile> _tiles = new();
        
        private Vector2 _stackBounds = new(3f, 3f);
        
        private Tile _currentTile;
        private Tile _lastTile;

        private Vector3 LastTilePosition =>
            _lastTile == null ? new Vector3() : _lastTile.transform.localPosition;

        private float CurrentHeight
        {
            get
            {
                if (_tiles.Count == 0)
                {
                    return 0;
                }

                var tile = _tiles.Last.Value;

                return _tiles.Count * tile.Height;
            }
        }

        public void OnGameOver()
        {
            _currentTile.gameObject.AddComponent<Rigidbody>();
        }
        
        public void CreateNextTile()
        {
            var positionRange = tilePositionIterator.NextPosition();
            // Don't touch it! Otherwise it would break
            var height = CurrentHeight;
            
            _lastTile = _currentTile;
            
            _currentTile = tileFactory.CreateTile(new Vector3(), transform);
            _tiles.AddLast(_currentTile);
            
            ApplyStackBounds(_currentTile);

            var lastPos = LastTilePosition;
            var start = positionRange.Start.Add(x: lastPos.x, y: height, z: lastPos.z);
            var end = positionRange.End.Add(x: lastPos.x, y: height, z: lastPos.z);
            
            _currentTile.StartMoving(start, end, tileSpeed.Value);
        }
        
        public TilePlaceResult PlaceCurrentTile()
        {
            if (_currentTile == null) return TilePlaceResult.Cancelled;

            _currentTile.StopMoving();
            
            var lastPos = LastTilePosition;
            var curTransform = _currentTile.transform;
            var curPos = curTransform.localPosition;
            
            var x = GetPosition(lastPos.x, curPos.x, out var diffX);
            var z = GetPosition(lastPos.z, curPos.z, out var diffZ);
            
            _stackBounds.x -= diffX;
            _stackBounds.y -= diffZ;
            
            // Game over
            if (_stackBounds.x <= 0 || _stackBounds.y <= 0) 
                return TilePlaceResult.Missed;

            ApplyStackBounds(_currentTile);

            var curGlobalPos = curTransform.position;
            var curScale = curTransform.localScale;
            
            if (diffX > 0)
            {
                var rubblePosX = GetRubblePosition(x, curGlobalPos.x, curScale.x);
                
                var rubblePos = new Vector3(rubblePosX, curGlobalPos.y, curGlobalPos.z);
                var rubbleScale = new Vector3(diffX, curScale.y, curScale.z);

                CreateRubble(rubblePos, rubbleScale);
            }

            if (diffZ > 0)
            {
                var rubblePosZ = GetRubblePosition(z, curGlobalPos.z, curScale.z);
                
                var rubblePos = new Vector3(curGlobalPos.x, curGlobalPos.y, rubblePosZ);
                var rubbleScale = new Vector3(curScale.x, curScale.y, diffZ);
                
                CreateRubble(rubblePos, rubbleScale);
            }
            
            _currentTile.transform.localPosition = new Vector3(x, curPos.y, z);

            return diffX == 0 && diffZ == 0 ? TilePlaceResult.Perfect : TilePlaceResult.Sliced;
        }
        
        private static float GetRubblePosition(float newTilePosition, float tilePosition, float tileScale)
        {
            var condition = tilePosition > newTilePosition;
            return condition ? tilePosition + (tileScale / 2) : tilePosition - (tileScale / 2);
        }

        private float GetPosition(float lastPos, float curPos, out float difference)
        {
            difference = Math.Abs(lastPos - curPos);

            if (difference > errorMargin)
            {
                return (curPos + lastPos) / 2;
            }

            difference = 0;

            return lastPos;
        }
        
        private void ApplyStackBounds(Component tile)
        {
            var t = tile.transform;
            t.localScale = new Vector3(_stackBounds.x, t.localScale.y, _stackBounds.y);
        }
        
        private void CreateRubble(Vector3 position, Vector3 scale)
        {
            if (rubbleFactory != null)
            {
                rubbleFactory.CreateRubble(_currentTile, position, scale);   
            }
        }
    }
}

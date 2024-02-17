using UnityEngine;

namespace Gameplay
{
    public class TileFactory : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;
        [SerializeField] private TileColorProvider tileColorProvider;

        public Tile CreateTile(Vector3 position, Transform parent)
        {
            var tile = Instantiate(tilePrefab, position, Quaternion.identity, parent);
            
            var color = tileColorProvider.Next();
            tile.SetColor(color);
            
            return tile;
        }
        
    }
}
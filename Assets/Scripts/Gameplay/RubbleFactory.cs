using UnityEngine;

namespace Gameplay
{
    
    // TODO: implement pool system
    public class RubbleFactory : MonoBehaviour
    {

        public void CreateRubble(Tile tile, Vector3 position, Vector3 scale)
        {
            var rubble = Instantiate(tile, position, Quaternion.identity);

            rubble.gameObject.AddComponent<Rigidbody>();
            rubble.transform.localScale = scale;
        }
        
    }
}
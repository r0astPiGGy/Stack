using System;
using UnityEngine;

namespace Gameplay
{
    public class TileRemover : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Tile>(out _))
            {
                Destroy(other.gameObject);   
            }
        }
    }
}
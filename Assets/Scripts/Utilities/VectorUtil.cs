using UnityEngine;

namespace Utilities
{
    public static class VectorUtil
    {
        public static Vector3 Add(this Vector3 vector3, float x = 0, float y = 0, float z = 0)
        {
            return new Vector3(vector3.x + x, vector3.y + y, vector3.z + z);
        }
        
        public static Vector2 Add(this Vector2 vector2, float x = 0, float y = 0)
        {
            return new Vector2(vector2.x + x, vector2.y + y);
        }
    }
}
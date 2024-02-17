using UnityEngine;

namespace Gameplay
{
    [System.Serializable]
    public class GameData
    {
        public int highScore;
        
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public void LoadFromJson(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}
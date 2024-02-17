using UnityEngine;

namespace Gameplay.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Themes/New Theme")]
    public class ThemeSO : ScriptableObject
    {

        // TODO: REMOVE
        [SerializeField] private Color towerColor;
        [SerializeField] private Color tileColor;
        
        // TODO: REMOVE (Split)
        [SerializeField] private Color backgroundTopColor;
        [SerializeField] private Color backgroundBottomColor;

        public Color TowerColor => towerColor;
        public Color TileColor => TowerColor;
        public Color BackgroundTopColor => backgroundTopColor;
        public Color BackgroundBottomColor => backgroundBottomColor;
    }
}
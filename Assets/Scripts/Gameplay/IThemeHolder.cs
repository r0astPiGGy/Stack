using Gameplay.ScriptableObjects;
using JetBrains.Annotations;

namespace Gameplay
{
    public interface IThemeHolder
    {
        
        [CanBeNull] ThemeSO Previous { get; }

        ThemeSO Current { get; }
        
        ThemeSO Next { get; }
        
        void MoveNext();

        void Reset();

    }
}
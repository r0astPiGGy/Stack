using System.Collections.Generic;
using System.Linq;
using Gameplay.ScriptableObjects;
using UnityEngine;
using Utilities;
using Random = System.Random;

namespace Gameplay
{
    public class ThemeHolder : IThemeHolder
    {
        public ThemeSO Previous { get; private set; }
        public ThemeSO Current { get; private set; }
        public ThemeSO Next => _iterator.Current;

        private List<ThemeSO> _themes;

        private IEnumerator<ThemeSO> _iterator;
        
        public ThemeHolder()
        {
            // TODO
            _themes = Resources.LoadAll<ThemeSO>("Themes").ToList();
            Debug.Log("Loaded " + _themes.Count + " themes");
            Reset();
        }

        private void ResetIterator()
        {
            _iterator?.Dispose();
            _iterator = InfiniteIterator.FromCollection(_themes).GetEnumerator();
            MoveNext();
        }

        private void Shuffle()
        {
            _themes = Shuffle(_themes);
        }

        private static List<T> Shuffle<T>(IList<T> list)
        {
            var newList = new List<T>();
            var size = list.Count;
            var random = new Random();
            
            for (var i = 0; i < size; i++)
            {
                var nextIndex = random.Next(0, list.Count);
                newList.Add(list[nextIndex]);
                list.RemoveAt(nextIndex);
            }

            return newList;
        }
        
        public void MoveNext()
        {
            Previous = Current;
            Current = _iterator.Current;
            _iterator.MoveNext();
        }

        public void Reset()
        {
            Shuffle();
            ResetIterator();
        }
    }
}
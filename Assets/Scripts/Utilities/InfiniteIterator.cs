using System.Collections.Generic;

namespace Utilities
{
    public static class InfiniteIterator
    {
        
        
        public static IEnumerable<T> FromCollection<T>(ICollection<T> elements)
        {
            while (true)
            {
                foreach (var next in elements)
                {
                    yield return next;
                }
            }
        }

        
        public static IEnumerable<T> Of<T>(params T[] elements)
        {
            while (true)
            {
                foreach (var next in elements)
                {
                    yield return next;
                }
            }
        }
    
    }
}
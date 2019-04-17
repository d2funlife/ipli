using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IpLi.Core.Helpers
{
    public static class EnumerableHelper
    {
        public static Boolean IsEmpty(this IEnumerable collection)
        {
            if(collection == null)
            {
                return true;
            }

            var enumerator = collection.GetEnumerator();
            return !enumerator.MoveNext();
        }

        public static Boolean IsNotEmpty(this IEnumerable collection)
        {
            if(collection == null)
            {
                return true;
            }

            var enumerator = collection.GetEnumerator();
            return enumerator.MoveNext();
        }

        public static Boolean IsEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null ||
                   !collection.Any();
        }

        public static Boolean IsNotEmpty<T>(this IEnumerable<T> collection)
        {
            return collection != null &&
                   collection.Any();
        }
    }
}
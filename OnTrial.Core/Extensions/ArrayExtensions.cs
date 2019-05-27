using System;
using System.Collections.Generic;

namespace OnTrial
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Append the given objects to the original source array
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="pSource">The original array of values</param>
        /// <param name="pItem">The values to appeand to the source</param>
        /// <returns></returns>
        public static T[] Append<T>(this T[] pSource, params T[] pItems)
        {
            // Create a list of the source items
            var list = new List<T>(pSource);

            // Append the new items
            list.AddRange(pItems);

            // Return the new array
            return list.ToArray();
        }

        /// <summary>
        /// Prepend the given objects to the original source array
        /// </summary>
        /// <typeparam name="T">The type of array</typeparam>
        /// <param name="pSource">The original array of values</param>
        /// <param name="pItem">The values to prepeand to the source</param>
        /// <returns></returns>
        public static T[] Prepend<T>(this T[] pSource, params T[] pItems)
        {
            // Create a list of the toAdd items
            var list = new List<T>(pItems);

            // Append the source items
            list.AddRange(pSource);

            // Return the new array
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pSource"></param>
        /// <returns></returns>
        public static T Random<T>(this T[] pSource)
        {
            if (pSource == null)
                throw new NullReferenceException();

            if (pSource.Length == 1)
                throw new ArgumentException("Argument cannot contain only one value.");

            return pSource[global::OnTrial.Random.Int(pSource.Length)];
        }
    }
}

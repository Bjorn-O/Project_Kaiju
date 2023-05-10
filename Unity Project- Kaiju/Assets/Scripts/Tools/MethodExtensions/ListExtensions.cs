using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = System.Random;

namespace Toolbox.MethodExtensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Checks if the list is empty 
        /// </summary>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IList<T> target)
        {
            return target.Count == 0;
        }
        
        /// <summary>
        /// Checks if the list is NOT empty 
        /// </summary>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this IList<T> target)
        {
            return target.Count != 0;
        }
    
        /// <summary>
        /// Shuffle the list 
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    
        /// <summary>
        /// Return a random item from the list.
        /// Sampling with replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomItem<T>(this IList<T> list)
        {
            if (list.IsEmpty()) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Removes a random item from the list, returning that item.
        /// Sampling without replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            if (list.IsEmpty()) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>(this IList<T> list, int index)
        {
            if(list.IsEmpty()) throw new IndexOutOfRangeException("Cannot get item from an empty list");

            if (index < 0)
            {
                if(Math.Abs(index) > list.Count) index %= list.Count;
                index = list.Count + index;
            }
            else if (index > list.Count - 1) index %= list.Count;

            return list[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public static void SetAt<T>(this IList<T> list, int index, T item)
        {
            if (index < 0)
            {
                if(Math.Abs(index) > list.Count) index %= list.Count;
                index = list.Count + index;
            }
            else if (index > list.Count - 1) index %= list.Count;

            list.Insert(index, item);
        }

        /// <summary>
        /// Add list from same type to current list 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="otherList"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddList<T>(this IList<T> list, List<T> otherList)
        {
            foreach (var item in otherList)
            {
                list.Add(item);
            }
        }

        /// <summary>
        /// adds the item to the list if not null else returns the unchanged list.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddIfNotNull<T>(this IList<T> list, T item)
        {
            if (item != null) list.Add(item);
        }

        /// <summary>
        /// remove list from same type from current list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="otherList"></param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveList<T>(this IList<T> list, IList<T> otherList)
        {
            foreach (var item in otherList)
            {
                if(!list.Contains(item)) continue;
                list.Remove(item);
            }
        }

        /// <summary>
        /// ContainsSlot checks if the given number is between the list size.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool ContainsSlot<T>(this IList<T> list, int index)
        {
            return index >= 0 && list.Count - 1 >= index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldList"></param>
        /// <typeparam name="TY"></typeparam>
        /// <typeparam name="TU"></typeparam>
        public static List<TU> ConvertListItemsTo<TY, TU>(this IList<TY> oldList) where TU : class
        {
            return oldList.Select(oldItem => oldItem as TU).ToList();
        }

        /// <summary>
        /// GetPossibleIndex returns index that is within range of the list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int GetPossibleIndex<T>(this IList<T> list, int index)
        {
            if (index >= list.Count) return 0;
            if (index < 0) return list.Count - 1;
            return index;
        }
        
        public static T GetPossibleItem<T>(this IList<T> list, int index)
        {
            if (index >= list.Count - 1) index = 0;
            if (index < 0) index = 0;

            return list[index];
        }
        
        public static T GetNextPossibleItem<T>(this IList<T> list, T indexedObject)
        {
            var i = list.IndexOf(indexedObject) + 1;
            
            if (i >= list.Count) i = 0;
            if (i < 0) i = 0;

            return list[i];
        }
    }
}

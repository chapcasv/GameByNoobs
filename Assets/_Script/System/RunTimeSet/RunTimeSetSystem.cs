using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH.RunTimeSet
{
    public abstract class RunTimeSetSystem<T> : ScriptableObject
    {
        private List<T> items = new List<T>();

        public void Initialize()
        {
            items.Clear();
        }

        public T GetItem(int index)
        {
            return items[index];
        }

        public void Add(T item)
        {
            if (items.Contains(item))
            {
                Debug.Log(item.ToString() + " cant add");
            }
            else items.Add(item);
        }

        public void Remove(T item)
        {
            if (!items.Contains(item))
            {
                Debug.Log(item.ToString() + " cant remove");
            }
            else items.Remove(item);
        }
        
    }
}



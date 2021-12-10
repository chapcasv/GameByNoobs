using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();

        public void Register(GameEventListener l) => _listeners.Add(l);

        public void UnRegister(GameEventListener l) => _listeners.Remove(l);

        public void Invoke()
        {
            foreach (var l in _listeners)
            {
                l.RaiseEvent();
            }
        }
    }
}

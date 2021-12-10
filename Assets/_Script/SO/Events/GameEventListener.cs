using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SO
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEvent unityEvent;

        void OnEnable() => OnEnableLogic();

        /// <summary>
        /// Override this to override the OnEnableLogic()
        /// </summary>
        public virtual void OnEnableLogic()
        {
            if (gameEvent != null)
                gameEvent.Register(this);
        }

        void OnDisable() => OnDisableLogic();

        /// <summary>
        /// Override this to override the OnDisableLogic()
        /// </summary>
        public virtual void OnDisableLogic()
        {
            if (gameEvent != null)
                gameEvent.UnRegister(this);
        }


        public virtual void RaiseEvent() =>  unityEvent.Invoke();

    }
}

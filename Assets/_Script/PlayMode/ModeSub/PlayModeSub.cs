using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class PlayModeSub : ScriptableObject
    {
        [SerializeField] protected string discription;
        protected PlayModeEnemy enemy;

        public string Discription => discription;

        public abstract int GetTimeFindMatch();

        public virtual PlayModeEnemy GetEnemy()
        {
            return enemy;
        }

        public virtual void SetEnemy(PlayModeEnemy value)
        {
            enemy = value;
        }
    }
}


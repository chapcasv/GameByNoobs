using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public abstract class StatusEffect : ScriptableObject
    {
        [SerializeField] string statusDiscription;
        [SerializeField] float lifeTime;
        [SerializeField] float tickSpeed = 1;

        public float LifeTime => lifeTime;
        public float TickSpeed => tickSpeed;
        public string Discription => statusDiscription;

        public abstract void Execute(BaseUnit unit);

        public abstract void Remove(BaseUnit unit);
    }
}


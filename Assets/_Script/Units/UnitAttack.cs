using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class UnitAttack : MonoBehaviour
    {
        protected float attackSpeed; //Attacks per second
        protected float range;
        protected int str;
        protected bool canAttack;
        protected float waitBetweenAttack;
        public bool CanAtk => canAttack;
        
        public void Constructor(float ats, float range, int str)
        {
            attackSpeed = ats;
            this.range = CaculatorRangeAtk(range);
            this.str = str;
            canAttack = true;
        }

        public bool IsInRange(BaseUnit currentTarget) 
        {
            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentTarget != null && distance <= range)
            {
                return true;
            }
            else return false;
        }

        public abstract void Atk(BaseUnit currentTarget);

        private float CaculatorRangeAtk(float range)
        {
            return range * 6 + 2.5f; //CellSize
        }
    }
}


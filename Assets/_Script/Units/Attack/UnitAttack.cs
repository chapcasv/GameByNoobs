using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class UnitAttack : MonoBehaviour
    {
        protected float attackSpeed;
        protected float range;
        protected int damage;
        protected bool canAttack;
        protected float waitBetweenAttack;
        protected int critRate;
        public bool CanAtk => canAttack;
        
        public void Constructor(float ats, float range, int dmg, int critRate)
        {
            attackSpeed = ats;
            this.range = CaculatorRangeAtk(range);
            this.damage = dmg;
            this.critRate = critRate;
            canAttack = true;
        }

        public abstract bool IsInRange(BaseUnit currentTarget);

        public abstract void Attack(BaseUnit currentTarget);

        private float CaculatorRangeAtk(float range) => range * 6 + 2.5f; //CellSize
    }
}


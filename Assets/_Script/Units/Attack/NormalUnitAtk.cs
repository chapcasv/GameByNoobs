using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class NormalUnitAtk : UnitAttack
    {
        public override void Attack(BaseUnit currentTarget)
        {
            if (!canAttack)
                return;

            //Number atk in one second
            waitBetweenAttack = 1 / attackSpeed;
            currentTarget.TakeDamage(damage);
            StartCoroutine(WaitCoroutine());

        }

        public override bool IsInRange(BaseUnit currentTarget)
        {
            if (currentTarget == null) return false; //Target Dead

            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (distance <= range)
            {
                return true;
            }
            else return false;
        }

        IEnumerator WaitCoroutine()
        {
            canAttack = false;
            yield return null;
            //Anim
            yield return new WaitForSeconds(waitBetweenAttack);
            canAttack = true;
        }
    }
}


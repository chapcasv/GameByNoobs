using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class NormalUnitAtk : UnitAttack
    {
        public override void Atk(BaseUnit currentTarget)
        {
            if (!canAttack)
                return;

            //Number atk in one second
            waitBetweenAttack = 1 / attackSpeed;
            currentTarget.TakeDamage(str);
            StartCoroutine(WaitCoroutine());

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


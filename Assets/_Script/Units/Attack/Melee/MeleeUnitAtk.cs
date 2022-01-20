using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class MeleeUnitAtk : UnitAtkSystem
    {
        [SerializeField] protected AtkPointMelee atkPoint;
        public override void BasicAtk(BaseUnit currentTarget)
        {
            if (!canAttack || !currentTarget.IsLive)
                return;


            //Number atk in one second
            waitBetweenAttack = 1 / baseAttackSpeed;

            atkPoint.SetUp(basePhysicalDmg, currentTarget, this, DmgType.Physical);
            RotationFollowTarget(currentTarget);
            StartCoroutine(WaitCoroutine());

        }

        public override void CastAbility(BaseUnit currentTarget)
        {
            ability.CastSkill(currentTarget, this);
        }

        public override bool IsInRange(BaseUnit currentTarget)
        {
            if (currentTarget == null) return false; //Target Dead

            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (distance <= baseRangeAtk)
            {
                return true;
            }
            else return false;
        }

        public override bool IsInRangeAbility(BaseUnit currentTarget)
        {
            if (currentTarget == null) return false; //Target Dead

            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (distance <= ability.GetRange())
            {
                return true;
            }
            else return false;
        }

        IEnumerator WaitCoroutine()
        {
            animator.SetBool(AnimEnum.IsMoving.ToString(), false);
            canAttack = false;
            animator.SetTrigger(AnimEnum.IsAtk.ToString());
            
            yield return null;

            yield return new WaitForSeconds(waitBetweenAttack);

            canAttack = true;
        }
    }
}


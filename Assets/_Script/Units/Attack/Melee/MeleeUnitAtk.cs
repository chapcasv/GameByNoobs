using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{

    public class MeleeUnitAtk : UnitAtkSystem
    {
        public override void BasicAtk()
        {
            if (!canAttack || !currentTarget.IsLive)
                return;

            //Number atk in one second
            waitBetweenAttack = 1 / baseAttackSpeed;

            RotationFollowTarget(currentTarget);
            StartCoroutine(WaitCoroutine());

        }

        public override void CastAbility(BaseUnit currentTarget, BaseUnit caster)
        {
            ability.CastSkill(currentTarget, caster);
            TriggerAfterCastSkill(caster);
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

            if (!IsDisableAtk)
            {
                animator.SetTrigger(AnimEnum.IsAtk.ToString());

                yield return null;

                yield return new WaitForSeconds(waitBetweenAttack);

                canAttack = true;
            }
        }

        //animation event
        public void SenderDmgToCurrentTarget()
        {
            if (currentTarget == null) return;

            int preMitigationDmg = orPhysicalDmg;

            Caculator(ref preMitigationDmg, currentTarget);

            int dmgDeal = currentTarget.TakeDamage(holder, preMitigationDmg, DmgType.Physical, IsCrit);

            LifeStealByDmg(dmgDeal);
            TriggerBasicAtkAddOn();
        }
    }
}


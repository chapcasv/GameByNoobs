using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class MeleeUnitAttack : UnitAtkSystem
    {
        [SerializeField] protected DamageType normalAtkType;

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
            if (!canAttack || !currentTarget.IsLive)
                return;

            //Number atk in one second
            waitBetweenAttack = 1 / baseAttackSpeed;

            RotationFollowTarget(currentTarget);
            StartCoroutine(WaitAbility());
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

        protected IEnumerator WaitCoroutine()
        {
            animator.SetBool(AnimEnum.IsMoving.ToString(), false);

            canAttack = false;

            if (!IsDisableAtk)
            {
                animator.SetTrigger(AnimEnum.IsAtk.ToString());
                yield return new WaitForSeconds(waitBetweenAttack);
                canAttack = true;
            }
        }

        protected virtual IEnumerator WaitAbility()
        {
            animator.SetBool(AnimEnum.IsMoving.ToString(), false);

            if (!IsDisableAtk)
            {
                canAttack = false;
                canCastAbility = false;

                animator.SetTrigger(AnimEnum.IsCastAbility.ToString());
                PlayAbilityVFX();

                yield return new WaitForSeconds(ability.GetGetDeplay(this));
                canAttack = true;
                canCastAbility = true;
            }
        }

        //animation event
        public void SenderDmgToCurrentTarget()
        {
            if (currentTarget == null) return;

            int preMitigationDmg = orPhysicalDmg;

            Caculator(ref preMitigationDmg, currentTarget);

            int dmgDeal = currentTarget.TakeDamage(holder, preMitigationDmg, normalAtkType, IsCrit);

            LifeStealByDmg(dmgDeal);
            TriggerBasicAtkAddOn();
        }

        //animation event
        public void CastAbilityByAnim()
        {
            Debug.Log(gameObject.name + " Cast");

            if (currentTarget == null) return;

            ability.CastSkill(currentTarget, holder);
            TriggerAfterCastSkill(holder);
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class RangerUnitAtk : UnitAtkSystem
    {
        [SerializeField] protected ProjectileMove pfProjectile;
        [SerializeField] protected Transform firePoint;

        public override void BasicAtk(BaseUnit currentTarget)
        {
            if (!canAttack || !currentTarget.IsLive )
                return;

            //Number atk in one second
            waitBetweenAttack = 1 / attackSpeed;
            CreateProjectile(currentTarget);

            StartCoroutine(WaitCoroutine());
        }

        private void CreateProjectile(BaseUnit currentTarget)
        {
            ProjectileMove pm = Instantiate(pfProjectile, firePoint.position, Quaternion.identity,this.transform);
            pm.SetUp(currentTarget, damage);
        }

        public override void CastAbility(BaseUnit currentTarget)
        {
            ability.CastSkill(currentTarget, this);
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
            yield return null;
            animator.SetTrigger(AnimEnum.IsAtk.ToString());

            yield return new WaitForSeconds(waitBetweenAttack);
            canAttack = true;
        }
    }
}


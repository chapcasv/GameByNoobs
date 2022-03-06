using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class RangerUnitAtk : UnitAtkSystem
    {
        [SerializeField] protected ProjectileMove pfProjectile;
        [SerializeField] protected Transform firePoint;
        [SerializeField] protected DamageType normalAtkType;

        protected Queue<ProjectileMove> projectiles;

        #region Init & Projectile Pool

        public override void Constructor(float ats, float range, int dmg, Ability ability, UnitSurvivalStat uss, Animator anim)
        {
            base.Constructor(ats, range, dmg, ability, uss, anim);
            InitProjectilePool(2);
        }

        protected void InitProjectilePool(int amount)
        {
            projectiles = new Queue<ProjectileMove>();

            for (int i = 0; i < amount; i++)
            {
                Create();
            }
        }

        private void Create()
        {
            ProjectileMove pm = Instantiate(pfProjectile, firePoint.position, Quaternion.identity, transform);
            pm.Constructor(this, holder,normalAtkType);
            pm.gameObject.SetActive(false);
            projectiles.Enqueue(pm);
        }

        protected ProjectileMove GetProjectile()
        {
            if(projectiles.Count == 0)
            {
                Create();
            }
            return projectiles.Dequeue();
        }

        public void ReturnToPool(ProjectileMove pm)
        {
            pm.transform.position = firePoint.position;
            pm.gameObject.SetActive(false);
            projectiles.Enqueue(pm);
        }

        #endregion

        public override void BasicAtk()
        {
            if (!canAttack || !currentTarget.IsLive)
                return;

            //Number atk in one second
            waitBetweenAttack = 1 / baseAttackSpeed;

            RotationFollowTarget(currentTarget);
            StartCoroutine(WaitCoroutine());
        }

        protected IEnumerator WaitCoroutine()
        {
            animator.SetBool(AnimEnum.IsMoving.ToString(), false);

            if (!IsDisableAtk)
            {
                canAttack = false;
                animator.SetTrigger(AnimEnum.IsAtk.ToString());
                yield return new WaitForSeconds(waitBetweenAttack);
                canAttack = true;
            }
        }

        public void SpawnProjectile()
        {
            var projectile = GetProjectile();

            int preMitigationDmg = orPhysicalDmg;

            Caculator(ref preMitigationDmg, currentTarget);

            projectile.SetUp(currentTarget, preMitigationDmg, firePoint);
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


        public override void CastAbility(BaseUnit currentTarget, BaseUnit caster)
        {
            if (!canAttack || !currentTarget.IsLive)
                return;

            //Number atk in one second
            waitBetweenAttack = 1 / baseAttackSpeed;

            RotationFollowTarget(currentTarget);
            StartCoroutine(WaitAbility());
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

        //animation event
        public void CastAbilityByAnim()
        {
            if (currentTarget == null) return;

            ability.CastSkill(currentTarget, holder);
            TriggerAfterCastSkill(holder);
        }

    }
}


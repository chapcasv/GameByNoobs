using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PH
{
    public class AbilityProjectile : MonoBehaviour
    {
        [SerializeField] AbilityProjectileMove pfProjectile;
        [SerializeField] int poolAmount;
        [SerializeField] float delay;

        private Transform firePoint;
        private BaseUnit holder;
        private RangerUnitAtk atkSystem;
        private Queue<AbilityProjectileMove> pool;
        private DamageType damageType;

        private void Start()
        {
            holder = GetComponent<BaseUnit>();
            atkSystem = holder.GetAtkSystem as RangerUnitAtk;
            firePoint = atkSystem.GetFirePoint;
            pool = new Queue<AbilityProjectileMove>();

            damageType = atkSystem.GetAbility.GetDamageType;
            CreatePool();
        }

        public void Atk(BaseUnit target,int rawDmg)
        {
            if(pool.Count > 0)
            {
                var proj = pool.Dequeue();
                proj.gameObject.SetActive(true);
                proj.SetUp(target, rawDmg, firePoint);
            }
        }

        public void Atk(BaseUnit[] targetArray, int rawDmg)
        {
            if(pool.Count >= targetArray.Length)
            {
                StartCoroutine(CastMulti(targetArray, rawDmg));
            }
        }

        private IEnumerator CastMulti(BaseUnit[] targetArray, int rawDmg)
        {
            for (int i = 0; i < targetArray.Length; i++)
            {
                var proj = pool.Dequeue();
                proj.gameObject.SetActive(true);
                proj.SetUp(targetArray[i], rawDmg, firePoint);

                yield return new WaitForSeconds(delay);
            }
        }

        private void CreatePool()
        {
            for (int i = 0; i < poolAmount; i++)
            {
                var projectile = Instantiate(pfProjectile, transform);
                projectile.Constructor(atkSystem, holder,damageType,this);
                projectile.gameObject.SetActive(false);
                pool.Enqueue(projectile);
            }
        }

        public void ReturnToPool(AbilityProjectileMove pm)
        {
            pm.transform.position = firePoint.position;
            pm.gameObject.SetActive(false);
            pool.Enqueue(pm);
        }
    }
}


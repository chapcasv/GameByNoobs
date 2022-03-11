using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class AbilityProjectileHolder : MonoBehaviour
    {
        [SerializeField] AbilityProjectileMove pf;
        [SerializeField] int poolAmount;
        [SerializeField] float delay;

        private BaseUnit caster;
        private RangerUnitAtk rangeSystem;
        private Transform firePoint;
        private Queue<AbilityProjectileMove> pool;


        void Start()
        {
            caster = GetComponent<BaseUnit>();
            rangeSystem = caster.GetAtkSystem as RangerUnitAtk;
            firePoint = rangeSystem.GetFirePoint;

            var dmgType = rangeSystem.GetAbility.GetDamageType;

            pool = new Queue<AbilityProjectileMove>();

            for (int i = 0; i < poolAmount; i++)
            {
                var proj = Instantiate(pf, transform);
                proj.Constructor(rangeSystem, caster, dmgType, this);
                proj.gameObject.SetActive(false);
                pool.Enqueue(proj);
            }
        }

        public void Move(BaseUnit[] targetArray,int rawDmg)
        {
            if(targetArray.Length <= pool.Count)
            {
                StartCoroutine(MoveMulti(targetArray, rawDmg, firePoint));
            }
        }

        private IEnumerator MoveMulti(BaseUnit[] targetArray, int rawDmg, Transform firePoint)
        {
            for (int i = 0; i < targetArray.Length; i++)
            {
                if (targetArray[i] == null || !targetArray[i].IsLive)
                {
                    break;
                }

                var proj = pool.Dequeue();
                proj.SetUp(targetArray[i], rawDmg, firePoint);

                yield return new WaitForSecondsRealtime(delay);
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



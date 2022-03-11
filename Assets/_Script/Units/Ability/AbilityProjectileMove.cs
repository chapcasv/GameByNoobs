using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class AbilityProjectileMove : ProjectileMove
    {
        private AbilityProjectileHolder _pool;

        public void Constructor(RangerUnitAtk sender, BaseUnit holder, DamageType type, AbilityProjectileHolder pool)
        {
            _sender = sender;
            _holder = holder;
            _type = type;
            _pool = pool;

            InitImpact();
        }

        protected override void LateUpdate()
        {
            if (isEnterTarget)
            {
                if (!VfxExtention.ParticleIsPlay(particlesImpact))
                {
                    impact.SetActive(false);
                    _pool.ReturnToPool(this);
                }
            }
            else
            {
                if (_currentTarget == null || !_currentTarget.IsLive || _currentTarget.enabled == false)
                {
                    _pool.ReturnToPool(this);
                }
                else
                {
                    transform.position += moveSpeed * Time.deltaTime * moveDir;
                    Quaternion rotation = Quaternion.LookRotation(moveDir);
                    transform.rotation = rotation;
                }
            }
        }


        protected override void OnTriggerEnter(Collider other)
        {
            var unit = other.gameObject.GetComponent<BaseUnit>();

            if (unit != null && unit == _currentTarget)
            {
                isEnterTarget = true;
                unit.TakeDamage(_holder, _rawDmg, _type);
                ActiveImpact();
            }
        }

        protected override void ActiveImpact()
        {
            impact.transform.position = BattleMethods.GetMidPos(_currentTarget);
            impact.SetActive(true);
        }
    }
}


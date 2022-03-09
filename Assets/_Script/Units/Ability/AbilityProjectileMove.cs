using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class AbilityProjectileMove : ProjectileMove
    {
        private AbilityProjectile manager;

        public void Constructor(RangerUnitAtk sender, BaseUnit holder, DamageType type, AbilityProjectile manager)
        {
            _sender = sender;
            _holder = holder;
            _type = type;
            this.manager = manager;

            InitImpact();
        }

        protected override void LateUpdate()
        {
            if (isEnterTarget)
            {
                if (!VfxExtention.ParticleIsPlay(particlesImpact))
                {
                    impact.SetActive(false);
                    manager.ReturnToPool(this);
                }
            }
            else
            {
                if (_currentTarget == null || !_currentTarget.IsLive || _currentTarget.enabled == false)
                {
                    manager.ReturnToPool(this);
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
    }
}


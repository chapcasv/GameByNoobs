using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class ProjectileMove : MonoBehaviour
    {
        [SerializeField] protected GameObject pfImpact;

        protected DamageType _type;
        protected BaseUnit _currentTarget;
        protected RangerUnitAtk _sender;
        protected BaseUnit _holder;
        protected GameObject impact;
        protected List<ParticleSystem> particlesImpact;
        protected Vector3 moveDir;

        protected float moveSpeed = 30f;
        protected int _rawDmg;
        protected bool isEnterTarget;

        public void Constructor(RangerUnitAtk sender, BaseUnit holder, DamageType type)
        {
            _sender = sender;
            _holder = holder;
            _type = type;

            InitImpact();
        }

        protected void InitImpact()
        {
            impact = Instantiate(pfImpact, transform);
            particlesImpact = VfxExtention.GetParticlesChild(impact.transform);
            impact.SetActive(false);
        }

        public void SetUp(BaseUnit currentTarget, int rawDmg, Transform firePoint)
        {
            if (currentTarget == null || !currentTarget.IsLive)
            {
                _sender.ReturnToPool(this);
                return;
            }

            _currentTarget = currentTarget;
            _rawDmg = rawDmg;
            isEnterTarget = false;
            transform.position = firePoint.position;
            moveDir = GetMoveDir();
            gameObject.SetActive(true);
        }

        protected virtual void LateUpdate()
        {
            if (isEnterTarget)
            {
                if (!VfxExtention.ParticleIsPlay(particlesImpact))
                {
                    impact.SetActive(false);
                    _sender.ReturnToPool(this);
                }
            }
            else
            {
                if (_currentTarget == null || !_currentTarget.IsLive || _currentTarget.enabled == false)
                {
                    _sender.ReturnToPool(this);
                }
                else
                {
                    transform.position += moveSpeed * Time.deltaTime * moveDir;
                    Quaternion rotation = Quaternion.LookRotation(moveDir);
                    transform.rotation = rotation;
                }
            }
        }

        private Vector3 GetMoveDir()
        {
            Vector3 target = BattleMethods.GetMidPos(_currentTarget);
            Vector3 moveDir = (target - transform.position).normalized;
            return moveDir;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var unit = other.gameObject.GetComponent<BaseUnit>();

            if (unit != null && unit == _currentTarget)
            {
                isEnterTarget = true;

                int dmgDeal = unit.TakeDamage(_holder, _rawDmg, _type, _sender.IsCrit);
                _sender.LifeStealByDmg(dmgDeal);
                ActiveImpact();
            }
        }

        protected virtual void ActiveImpact()
        {   
            impact.transform.position = BattleMethods.GetMidPos(_currentTarget);
            impact.SetActive(true);
        }
    }
}


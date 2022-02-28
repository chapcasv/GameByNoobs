using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class ProjectileMove : MonoBehaviour
    {
        private const float offsetVFX = 4f;
        [SerializeField] GameObject pfImpact;
        private DamageType _type;
        private BaseUnit _currentTarget;
        private RangerUnitAtk _sender;
        private BaseUnit _holder;
        private readonly float moveSpeed = 22f;
        private int _rawDmg;
        private bool isEnterTarget;
        private GameObject impact;
        private List<ParticleSystem> particlesImpact;

        public void Constructor(RangerUnitAtk sender, BaseUnit holder, DamageType type)
        {
            _sender = sender;
            _holder = holder;
            _type = type;

            InitImpact();
        }

        private void InitImpact()
        {
            impact = Instantiate(pfImpact, transform);
            particlesImpact = new List<ParticleSystem>();

            for (int i = 0; i < impact.transform.childCount; i++)
            {
                var pI = impact.transform.GetChild(i).GetComponent<ParticleSystem>();
                if (pI != null)
                {
                    particlesImpact.Add(pI);
                }
            }

            impact.SetActive(false);
        }

        public void SetUp(BaseUnit currentTarget, int rawDmg, Transform firePoint)
        {
            _currentTarget = currentTarget;
            _rawDmg = rawDmg;
            isEnterTarget = false;
            transform.position = firePoint.position;

            gameObject.SetActive(true);
        }

        private void LateUpdate()
        {
            if (isEnterTarget)
            {
                if (!ParticleIsPlay())
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
                    Vector3 moveDir = GetMoveDir();
                    transform.position += moveSpeed * Time.deltaTime * moveDir;
                    Quaternion rotation = Quaternion.LookRotation(moveDir);
                    transform.rotation = rotation;
                }
            }
        }

        private Vector3 GetMoveDir()
        {
            Vector3 target = _currentTarget.transform.position;
            float OffsetY = _currentTarget.Col.size.y / 2;

            target = new Vector3(target.x, OffsetY, target.z);

            Vector3 moveDir = (target - transform.position).normalized;
            return moveDir;
        }

        private void OnTriggerEnter(Collider other)
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

        private void ActiveImpact()
        {   
            Vector3 target = _currentTarget.transform.position;
            Vector3 pos = new Vector3(target.x, offsetVFX, target.z);

            impact.transform.position = pos;
            impact.SetActive(true);
        }

        protected bool ParticleIsPlay()
        {
            bool isPlaying = true;

            foreach (var particle in particlesImpact)
            {
                isPlaying = particle.isPlaying;

                if (isPlaying)
                {
                    break;
                }
            }
            return isPlaying;
        }
    }
}


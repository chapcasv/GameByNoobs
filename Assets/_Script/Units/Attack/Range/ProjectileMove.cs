using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class ProjectileMove : MonoBehaviour
    {
        private DamageType _type;
        private BaseUnit _currentTarget;
        private RangerUnitAtk _sender;
        private BaseUnit _holder;
        private float _moveSpeed = 18f;
        private int _rawDmg;

        public void Constructor(RangerUnitAtk sender, BaseUnit holder, DamageType type)
        {
            _sender = sender;
            _holder = holder;
            _type = type;
        }

        public void SetUp(BaseUnit currentTarget, int rawDmg)
        {
            _currentTarget = currentTarget;
            _rawDmg = rawDmg;

            Vector3 rad = new Vector3(-1, 1, 0);
            iTween.PunchPosition(gameObject, rad, 0.5f);

            gameObject.SetActive(true);
        }

        private void Update()
        {

            if (_currentTarget == null || !_currentTarget.IsLive || _currentTarget.enabled == false)
            {
                _sender.ReturnToPool(this);
            }
            else
            {
                Vector3 moveDir = (_currentTarget.transform.position - transform.position).normalized;
                transform.position += _moveSpeed * Time.deltaTime * moveDir;
                Quaternion rotation = Quaternion.LookRotation(moveDir);
                transform.rotation = rotation;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var unit = other.gameObject.GetComponent<BaseUnit>();

            if (unit != null && unit == _currentTarget)
            {
                int dmgDeal = unit.TakeDamage(_holder, _rawDmg,_type, _sender.IsCrit);
                _sender.LifeStealByDmg(dmgDeal);
                _sender.ReturnToPool(this);
            }
        }
    }
}


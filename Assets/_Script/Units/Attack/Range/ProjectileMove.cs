using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class ProjectileMove : MonoBehaviour
    {
        private BaseUnit _currentTarget;
        private UnitAtkSystem _sender;
        private BaseUnit _holder;
        private DmgType _type;
        private float _moveSpeed = 10f;
        private int _rawDmg;

        public void SetUp(BaseUnit currentTarget, UnitAtkSystem sender, int rawDmg, DmgType type, BaseUnit holder)
        {
            _currentTarget = currentTarget;
            _sender = sender;
            _rawDmg = rawDmg;
            _holder = holder;
            _type = type;
        }

        private void Update()
        {
            if (_currentTarget == null || !_currentTarget.IsLive || _currentTarget.enabled == false)
            {
                Destroy(gameObject);
            }
            else
            {
                Vector3 moveDir = (_currentTarget.transform.position - transform.position).normalized;
                transform.position += moveDir * _moveSpeed * Time.deltaTime;
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
                Destroy(gameObject);
            }
        }
    }
}


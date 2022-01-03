using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class ProjectileMove : MonoBehaviour
    {
        private BaseUnit _currentTarget;
        private float _moveSpeed = 10f;
        private int _dmgSender;

        public void SetUp(BaseUnit currentTarget, int dmgSender)
        {
            _currentTarget = currentTarget;
            _dmgSender = dmgSender;
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
                unit.TakeDamage(_dmgSender);
                Destroy(gameObject);
            }

        }
    }
}


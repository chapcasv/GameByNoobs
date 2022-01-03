using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class AtkPointMelee : MonoBehaviour
    {
        private BoxCollider box;
        private int _dmgSender;
        private BaseUnit _currenTarget;

        private void Awake()
        {
            box = GetComponent<BoxCollider>();
            box.enabled = false;
        }

        public void SetUp(int dmg, BaseUnit currentTarget)
        {
            _dmgSender = dmg;
            _currenTarget = currentTarget;
            box.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<BaseUnit>();
            if(target == _currenTarget)
            {
                _currenTarget.TakeDamage(_dmgSender);
                box.enabled = false;
            }
        }
    }
}


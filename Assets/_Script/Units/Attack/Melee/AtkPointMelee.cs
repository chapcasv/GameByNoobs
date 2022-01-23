using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class AtkPointMelee : MonoBehaviour
    {
        private BoxCollider box;
        private BaseUnit _currenTarget;
        private UnitAtkSystem _sender;
        private DmgType _type;
        private int _rawDmg;

        private void Awake()
        {
            box = GetComponent<BoxCollider>();

            box.enabled = false;
        }

        public void SetUp(int dmg, BaseUnit currentTarget, UnitAtkSystem sender, DmgType dmgType)
        {
            _rawDmg = dmg;
            _type = dmgType;
            _currenTarget = currentTarget;
            _sender = sender;
            box.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<BaseUnit>();
            if(target == _currenTarget)
            {
                int dmgDeal = _currenTarget.TakeDamage(_rawDmg,_type);

                _sender.LifeStealByDmg(dmgDeal);

                box.enabled = false;
            }
        }
    }
}


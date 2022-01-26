using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PH
{
    [CreateAssetMenu(fileName = "CalDmg", menuName = "ScriptableObject/Card/Unit/Calculator Pre Mitigation/Calculator random Normal Damage")]
    public class CalRandomNormalDamage : CalPreMitigation
    {
        public override void Cal(ref int dmg, BaseUnit currentTarget, UnitAtkSystem atkSystem)
        {
            int maxRandomBonusDamage = (int)(atkSystem.BasePhysicalDmg / 10);
            int randomBonusDamage = Random.Range(0, maxRandomBonusDamage);
            dmg += randomBonusDamage;
        }
    }

}


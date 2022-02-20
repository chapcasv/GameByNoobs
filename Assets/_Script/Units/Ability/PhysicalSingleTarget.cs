using HexColor;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Physical Single Target")]
    public class PhysicalSingleTarget : Ability
    {
        [SerializeField] int physicalDmg;
        [SerializeField] DamageType damageType;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            if (currentTarget == null) return; //Target dead

            int dmg = physicalDmg + caster.GetAtkSystem.ORPhysicalDamage;
            currentTarget.TakeDamage(caster, dmg, damageType);
        }

        public override string GetDiscription(CardUnit unit)
        {
            int dmg = physicalDmg + unit.Damage;
            return GetDiscription(dmg);
        }

        public override string GetDiscription(BaseUnit unit)
        {
            int dmg = physicalDmg + unit.GetAtkSystem.ORPhysicalDamage;
            return GetDiscription(dmg);
        }

        protected override string GetDiscription(int value)
        {
            int dmg = physicalDmg + value;
            string color = damageType.HexColor();
            string discription = "Gây" + "<color=" + color + "> " + dmg + "</color>" + " sát thương lên mục tiêu";

            return discription;
        }
    }
}


using HexColor;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Physical Single Target")]
    public class PhysicalSingleTarget : Ability
    {
        [SerializeField] int physicalDmg;

        public override void CastSkill(BaseUnit currentTarget,UnitAtkSystem atkSystem)
        {
            if (currentTarget == null) return; //Target dead

            int dmg = physicalDmg + atkSystem.ORPhysicalDamage;
            currentTarget.TakeDamage(dmg,DmgType.Physical);
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

        protected string GetDiscription(int value)
        {
            int dmg = physicalDmg + value;
            string physicalColor = HexColorString.PhysicalDmg();
            string discription = "Gây" + "<color=" + physicalColor + "> " + dmg + "</color>" + " sát thương lên mục tiêu";

            return discription;
        }
    }
}


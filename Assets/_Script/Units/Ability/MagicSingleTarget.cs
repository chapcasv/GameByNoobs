using HexColor;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Magic Single Target")]
    public class MagicSingleTarget : Ability
    {
        [SerializeField] int magicDmg;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            if (currentTarget == null) return; //Target dead

            int dmg = magicDmg + caster.GetAtkSystem.ORPhysicalDamage;
            currentTarget.TakeDamage(dmg, DmgType.Magic);
        }

        public override string GetDiscription(CardUnit unit)
        {
            int dmg = magicDmg + unit.Damage;
            return GetDiscription(dmg);
        }

        public override string GetDiscription(BaseUnit unit)
        {
            int dmg = magicDmg + unit.GetAtkSystem.ORPhysicalDamage;
            return GetDiscription(dmg);
        }

        protected override string GetDiscription(int value)
        {
            int dmg = magicDmg + value;
            string physicalColor = HexColorString.MagicDmg();
            string discription = "Gây" + "<color=" + physicalColor + "> " + dmg + "</color>" + " sát thương lên mục tiêu";

            return discription;
        }
    }
}

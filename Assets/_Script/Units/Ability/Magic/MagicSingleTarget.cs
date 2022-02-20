using HexColor;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Magic/Single Target")]
    public class MagicSingleTarget : Ability
    {
        [SerializeField] int magicDmg;
        [SerializeField] DamageType damageType;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            if (currentTarget == null) return; //Target dead

            int dmg = GetDmg(caster.GetAtkSystem.ORMagicPower);
            currentTarget.TakeDamage(caster, dmg, damageType);
        }

        public override string GetDiscription(CardUnit unit)
        {
            int dmg = GetDmg(100);
            return GetDiscription(dmg);
        }

        public override string GetDiscription(BaseUnit unit)
        {
            int dmg = GetDmg(unit.GetAtkSystem.ORMagicPower);
            return GetDiscription(dmg);
        }

        protected override string GetDiscription(int value)
        {
            string color = damageType.HexColor();
            string discription = "Gây" + "<color=" + color + "> " + value + "</color>" + " sát thương lên mục tiêu";

            return discription;
        }

        protected int GetDmg(int magicPower)
        {
            int dmg = (int)(magicDmg / 100f * magicPower);
            return dmg;
        }
    }
}

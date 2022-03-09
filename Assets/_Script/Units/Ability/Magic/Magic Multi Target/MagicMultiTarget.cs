using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class MagicMultiTarget : Ability
    {
        [SerializeField] int dmgValue;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            throw new System.NotImplementedException();
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
            string discription = "Gây" + "<color=" + color + "> " + value + "</color>" + " vào 5 mục tiêu ngẫu nhiên";

            return discription;
        }

        protected int GetDmg(int magicPower)
        {
            int dmg = (int)(dmgValue / 100f * magicPower);
            return dmg;
        }
    }

}

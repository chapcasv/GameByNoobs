using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Magic/MultiTarget/Fire")]
    public class MagicMultiTarget : Ability
    {
        [SerializeField] int dmgValue;

        [SerializeField] int numberTarget;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            var allEnemy = DictionaryTeamBattle.GetUnitsAgainst(caster.GetTeam());

            int dmg = GetDmg(caster.GetAtkSystem.ORMagicPower);

            AbilityProjectile ap = caster.GetComponent<AbilityProjectile>();

            if (ap == null) throw new System.Exception("Cant get abilityProjectile");


            BaseUnit[] arrayTarget = new BaseUnit[numberTarget];
            arrayTarget[0] = currentTarget;

            for (int i = 1; i < numberTarget; i++)
            {
                var rand = Random.Range(0, allEnemy.Count);
                var target = allEnemy[rand];
                arrayTarget[i] = target;
            }

            ap.Atk(arrayTarget, dmg);
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
            string discription = "Gây" + "<color=" + color + "> " + value + "</color>" + " vào " + numberTarget + " mục tiêu ngẫu nhiên";

            return discription;
        }

        protected int GetDmg(int magicPower)
        {
            int dmg = (int)(dmgValue / 100f * magicPower);
            return dmg;
        }
    }

}

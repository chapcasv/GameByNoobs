using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Magic/Multi Target")]
    public class MagicMultiTarget : Ability
    {
        [SerializeField] int dmgValue;
        [SerializeField] int numberTarget;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            AbilityProjectileHolder aph = caster.GetComponent<AbilityProjectileHolder>();

            int rawDmg = GetDmg(caster.GetAtkSystem.ORMagicPower);

            List<BaseUnit> allEnemy = DictionaryTeamBattle.GetUnitsAgainst(caster.GetTeam());
            if (allEnemy.Count == 0) return;

            BaseUnit[] targetArray = new BaseUnit[numberTarget];
            targetArray[0] = currentTarget;

            for (int i = 1; i < numberTarget; i++)
            {
                var index = Random.Range(0, allEnemy.Count);
                var target = allEnemy[index];
                targetArray[i] = target;
            }

            aph.Move(targetArray, rawDmg);
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

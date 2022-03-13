using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Buff/Heal caster and team mate lower")]
    public class BuffHealTeamMate : Ability
    {
        [SerializeField] int heal;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            var teamMate = GetTeamMateLowHP(caster);

            int healValue = GetHealValue(caster.GetAtkSystem.ORMagicPower);

            if(teamMate == caster)
            {
                teamMate.GetUnitSurvivalStat.RegenHealWithEffect(healValue/2);
            }
            else
            {
                teamMate.GetUnitSurvivalStat.RegenHealWithEffect(healValue);
            }
            
            caster.GetUnitSurvivalStat.RegenHealWithEffect(healValue);
        }

        protected BaseUnit GetTeamMateLowHP(BaseUnit caster)
        {
            var teamMate = DictionaryTeamBattle.GetTeamMate(caster.GetTeam());

            int currentHP = int.MaxValue;

            BaseUnit unitLowHP = null;

            foreach (var unit in teamMate)
            {
                if (unit.IsLive)
                {
                    var hp = unit.GetUnitSurvivalStat.ORCurrentHP;
                    if (hp < currentHP)
                    {
                        currentHP = hp;
                        unitLowHP = unit;
                    }
                }
            }
            return unitLowHP;
        }

        protected int GetHealValue(int magicPower)
        {
            return heal / 100 * magicPower + heal/2;
        }

        public override string GetDiscription(CardUnit unit)
        {
            return GetDiscription(100);
        }

        public override string GetDiscription(BaseUnit unit)
        {
            return GetDiscription(unit.GetAtkSystem.ORMagicPower);
        }

        protected override string GetDiscription(int value)
        {
            string disc = "Hồi phục cho ta và đồng minh thấp máu nhất " + value + " máu";
            return disc;
        }
    }

}

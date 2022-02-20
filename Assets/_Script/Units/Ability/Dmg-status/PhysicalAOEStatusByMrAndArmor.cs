using HexColor;
using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    /// <summary>
    /// Deal aoe Dmg by magic resist and armor
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Physical AOE Status by magic resist and armor")]
    public class PhysicalAOEStatusByMrAndArmor : Ability
    {
        [SerializeField] StatusEffect abilityStatusEffect;

        [SerializeField] int pctDmg;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            List<BaseUnit> targets = GetTarget(caster);

            var USS = caster.GetUnitSurvivalStat;

            float preMitigationDmg = GetPreMitigationDmg(USS.ORArmor, USS.ORMagicResist);

            foreach (var target in targets)
            {
                target.TakeDamage(caster, (int)preMitigationDmg, DmgType.Physical);
                target.GetUnitStatusEffect.ApplyStatusEffect(abilityStatusEffect);
            }

        }

        public override string GetDiscription(CardUnit unit)
        {
            float preMitigationDmg = GetPreMitigationDmg(unit.Armor, unit.MagicResist);
            return GetDiscription((int)preMitigationDmg);
        }

        public override string GetDiscription(BaseUnit unit)
        {
            var USS = unit.GetUnitSurvivalStat;

            float preMitigationDmg = GetPreMitigationDmg(USS.ORArmor, USS.ORMagicResist);
            return GetDiscription((int)preMitigationDmg);
        }

        protected override string GetDiscription(int value)
        {
            int dmg = value;
            string physicalColor = HexColorString.PhysicalDmg;
            string status = abilityStatusEffect.Discription;
            string lifeTime = abilityStatusEffect.LifeTime.ToString();

            string discription = "Gây" + "<color=" + physicalColor + "> " + dmg + "</color>" + " sát thương lên các mục tiêu xung quanh." +
               " Đồng thời " + status + " mỗi mục tiêu trúng chiêu trong " + lifeTime + " giây";

            return discription;
        }

        private float GetPreMitigationDmg(int armor, int magicResist)
        {
            int value = armor + magicResist;

            float preMitigationDmg = pctDmg * (value / 100f);
            return preMitigationDmg;
        }

        //Need create extension methods
        private List<BaseUnit> GetTarget(BaseUnit caster)
        {
            List<Node> nodeClose = GridBoard.GetNodesCloseTo(caster.CurrentNode);

            var allEnemy = DictionaryTeamBattle.GetUnitsAgainst(caster.GetTeam());

            List<BaseUnit> result = new List<BaseUnit>();

            foreach (var e in allEnemy)
            {
                if (nodeClose.Contains(e.CurrentNode))
                {
                    result.Add(e);
                }
            }

            return result;
        }
    }
}



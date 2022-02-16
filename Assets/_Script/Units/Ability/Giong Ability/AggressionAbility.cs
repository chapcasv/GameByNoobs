﻿using HexColor;
using PH.GraphSystem;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Giong/Aggression")]
    public class AggressionAbility : Ability
    {
        [SerializeField] int pctDmg;

        [SerializeField] int healPerTarget = 50;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            List<BaseUnit> targets = GetTarget(caster);

            float preMitigationDmg = GetPreMitigationDmg(caster.GetAtkSystem.ORPhysicalDamage);

            foreach (var target in targets)
            {
                target.TakeDamage(caster, (int) preMitigationDmg, DmgType.Physical);
            }

            int heal = healPerTarget * targets.Count;

            caster.GetUnitSurvivalStat.HealthUp(heal);
        }

        public override string GetDiscription(CardUnit unit)
        {
            float preMitigationDmg = GetPreMitigationDmg(unit.Damage);
            return GetDiscription((int)preMitigationDmg);
        }

        public override string GetDiscription(BaseUnit unit)
        {
            float preMitigationDmg = GetPreMitigationDmg(unit.GetAtkSystem.ORPhysicalDamage);
            return GetDiscription((int)preMitigationDmg);
        }

        protected override string GetDiscription(int value)
        {
            int dmg = value;
            string physicalColor = HexColorString.PhysicalDmg();
            string healColor = HexColorString.Heal();
            string discription = "Gây" + "<color=" + physicalColor + "> " + dmg + "</color>" + " sát thương lên các mục tiêu xung quanh." +
                "Hồi phục" + "<color=" + healColor + "> " + healPerTarget + "</color>" + " mỗi mục tiêu trúng chiêu";

            return discription;
        }

        private float GetPreMitigationDmg(int orPhysicalDmg)
        {
            float preMitigationDmg = pctDmg * (orPhysicalDmg / 100f);
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


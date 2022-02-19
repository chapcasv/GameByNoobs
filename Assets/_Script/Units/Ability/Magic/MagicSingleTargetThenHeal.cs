using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Magic/Single Target then heal")]
    public class MagicSingleTargetThenHeal : MagicSingleTarget
    {
        [SerializeField] int heal;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            base.CastSkill(currentTarget, caster);
            caster.GetUnitSurvivalStat.RegenHeal(heal);
        }

        public override string GetDiscription(BaseUnit unit)
        {   
            string baseDisc = base.GetDiscription(unit);
            string newDisc = baseDisc + NewDiscription();
            return newDisc;
        }

        public override string GetDiscription(CardUnit unit)
        {
            string baseDisc = base.GetDiscription(unit);
            string newDisc = baseDisc + NewDiscription();
            return newDisc;
        }

        protected string NewDiscription()
        {   
            string newDis = " Hồi phục " + heal + " máu";
            return newDis;
        }
    }
}


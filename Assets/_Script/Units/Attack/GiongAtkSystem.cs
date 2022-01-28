using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class GiongAtkSystem : MeleeUnitAtk
    {
        private Ability _baseAbility;

        public override void Constructor(float ats, float range, int dmg, Ability ability, UnitSurvivalStat uss, Animator anim)
        {
            base.Constructor(ats, range, dmg, ability, uss, anim);

            _baseAbility = ability;
        }

        public void ChangeAbility(Ability a)
        {
            ability = a;
        }

        public override void RemoveOneRoundAddOn()
        {
            base.RemoveOneRoundAddOn();
            ability = _baseAbility;
        }

    }
}


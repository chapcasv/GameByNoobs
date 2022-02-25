using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class GiongAtkSystem : MeleeUnitAttack
    {
        private Ability _baseAbility;
        private string _abilityAnimName;
        private const string ABILITY_ANIM_NAME_DEFAULT = "IsCastAbility";

        public override void Constructor(float ats, float range, int dmg, Ability ability, UnitSurvivalStat uss, Animator anim)
        {
            base.Constructor(ats, range, dmg, ability, uss, anim);

            _baseAbility = ability;
            _abilityAnimName = ABILITY_ANIM_NAME_DEFAULT;
        }

        public void ChangeAbility(Ability a, string abilityAnimName)
        {
            ability = a;
            _abilityAnimName = abilityAnimName;
        }

        protected override IEnumerator WaitAbility()
        {
            animator.SetBool(AnimEnum.IsMoving.ToString(), false);

            if (!IsDisableAtk)
            {
                canAttack = false;
                canCastAbility = false;

                animator.SetTrigger(_abilityAnimName);
                PlayAbilityVFX();

                yield return new WaitForSeconds(ability.GetGetDeplay(this));
                canAttack = true;
                canCastAbility = true;
            }
        }

        public override void RemoveOneRoundAddOn()
        {
            base.RemoveOneRoundAddOn();
            ability = _baseAbility;
            _abilityAnimName = ABILITY_ANIM_NAME_DEFAULT;
        }

    }
}


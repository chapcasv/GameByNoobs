using HexColor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Giong/Poop Crop")]
    public class PoopCropAbility : Ability
    {
        [SerializeField] int dmg;

        [SerializeField] int manaDecrease;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            BaseUnit farUnit = GetFarUnit(caster);

            float preMitigationDmg = dmg * (caster.GetAtkSystem.ORMagicPower / 100f);

            farUnit.TakeDamage((int)preMitigationDmg, DmgType.Magic);
            farUnit.GetManaSystem.DecreaseMana(manaDecrease);

        }

        public override string GetDiscription(CardUnit unit)
        {
            float preMitigationDmg = GetPreMitigationDmg(100); //default magic power;

            return GetDiscription((int)preMitigationDmg);
        }

        public override string GetDiscription(BaseUnit unit)
        {
            float preMitigationDmg = GetPreMitigationDmg(unit.GetAtkSystem.ORMagicPower);

            return GetDiscription((int)preMitigationDmg);
        }

        private float GetPreMitigationDmg(int magicPower)
        {
            float preMitigationDmg = dmg * (magicPower / 100f);
            return preMitigationDmg;
        }

        protected override string GetDiscription(int value)
        {
            int dmg = value;
            string magicColor = HexColorString.MagicDmg();
            string discription = "Gây" + "<color=" + magicColor + "> " + dmg + "</color>" + " sát thương lên mục tiêu xa nhất." +
                " Đồng thời triệt tiêu " + manaDecrease + " mana";

            return discription;
        }

        private BaseUnit GetFarUnit(BaseUnit caster)
        {
            var allEnemy = DictionaryTeamBattle.GetUnitsAgainst(caster.GetTeam());

            BaseUnit farUnit = null;

            float distance = 0f;

            foreach (var unit in allEnemy)
            {
                float d = Vector3.Distance(caster.transform.position, unit.transform.position);
                if(d > distance)
                {
                    distance = d;
                    farUnit = unit;
                }
            }
            return farUnit;
        }
    }
}


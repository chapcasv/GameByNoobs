using HexColor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Giong/Epidemic")]

    public class Epidemic : Ability
    {
        [SerializeField] int pctPhysicalDMG = 170;
        [SerializeField] int pctTrueDmgBonusByMaxHp = 12;
        [SerializeField] DamageType needTrueDmg;

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            int orPhysicalDmg = caster.GetAtkSystem.ORPhysicalDamage;

            float rawDmg = GetRawDmg(orPhysicalDmg);

            int maxHpTarget = currentTarget.GetUnitSurvivalStat.ORMaxHP;

            float trueDmgBonus = pctTrueDmgBonusByMaxHp * (maxHpTarget / 100f);

            currentTarget.TakeDamage(caster, (int)rawDmg, damageType);
            currentTarget.TakeDamage(caster, (int)trueDmgBonus, damageType);

            vfx.PlayVFX(currentTarget.transform.position);
        }

        private float GetRawDmg(int ORPhysicalDmg)
        {
            int baseDmg = ORPhysicalDmg;

            float rawDmg = pctPhysicalDMG * (baseDmg / 100f);
            return rawDmg;
        }

        public override string GetDiscription(CardUnit unit)
        {
            float rawDmg = GetRawDmg(unit.Damage);

            return GetDiscription((int)rawDmg);
        }

        public override string GetDiscription(BaseUnit unit)
        {
            float rawDmg = GetRawDmg(unit.GetAtkSystem.ORPhysicalDamage);
            return GetDiscription((int)rawDmg);
        }

        protected override string GetDiscription(int value)
        {
            int dmg = value;
            string physicalColor = HexColorString.PhysicalDmg;
            string discription = "Gây" + "<color=" + physicalColor + "> " + dmg + "</color>" + " sát thương, " +
                "đồng thời gây thêm sát thương chuẩn dựa trên " + pctTrueDmgBonusByMaxHp + "% máu tối đa của mục tiêu";

            return discription;
        }
    }

}

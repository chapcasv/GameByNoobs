using HexColor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Ability/Giong/Calamity")]
    public class CalamityAbility : Ability
    {
        [SerializeField] int physicalDmg;
        [SerializeField] int pct;
        [SerializeField] int critDmgBonus;
        [SerializeField] StatusEffect effect;
        [SerializeField] DamageType damageType;
        

        public override void CastSkill(BaseUnit currentTarget, BaseUnit caster)
        {
            var Atk = caster.GetAtkSystem;
            Atk.UpOneRoundCritDmg(critDmgBonus);
            float dmg = GetPreMitigationDmg(Atk.ORPhysicalDamage);
            currentTarget.TakeDamage(caster, (int)dmg, damageType);
            currentTarget.GetUnitStatusEffect.ApplyStatusEffect(effect);
        }

        public override string GetDiscription(CardUnit unit)
        {
            float preMitigationDmg = GetPreMitigationDmg(unit.Damage);
            return GetDiscription((int)preMitigationDmg);
        }

        public override string GetDiscription(BaseUnit unit)
        {
            int dmg = unit.GetAtkSystem.ORPhysicalDamage;
            float preMitigationDmg = GetPreMitigationDmg(dmg);
            return GetDiscription((int)preMitigationDmg);
        }

        private float GetPreMitigationDmg(int orPhysicalDmg)
        {
            float preMitigationDmg = physicalDmg + pct * (orPhysicalDmg / 100f);
            return preMitigationDmg;
        }

        protected override string GetDiscription(int value)
        {
            string color = damageType.HexColor();
            string status = effect.Discription;
            string timeDuring = effect.LifeTime.ToString();
            string discription = "Gây" + "<color=" + color + "> " + value + "</color>" + 
                " sát thương lên mục tiêu." +
                status + " trong " + timeDuring + " giây." + " Gia tăng " + critDmgBonus +" Sát thương chí mạng";

            return discription;
        }
    }
}


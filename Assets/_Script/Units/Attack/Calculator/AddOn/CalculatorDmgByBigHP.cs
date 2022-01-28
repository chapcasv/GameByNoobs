using UnityEngine;


namespace PH
{   
    [CreateAssetMenu(fileName ="CalDmg", 
        menuName = "ScriptableObject/Card/Unit/Calculator Pre Mitigation/Calculator by Big HP")]
    public class CalculatorDmgByBigHP : CalPreMitigation
    {
        [SerializeField] int minIncreasedDmg = 20;

        [SerializeField] int maxIncreasedDmg = 60;
        [Tooltip("If the enemy has over hpOverValue, system will use max increased dmg")]
        [SerializeField] int hpOverValue = 600;

        public override void Cal(ref int dmg, BaseUnit currentTarget, UnitAtkSystem atkSystem)
        {
            var HPtarget = currentTarget.GetUnitSurvivalStat.ORMaxHP;

            if (HPtarget >= hpOverValue)
            {
                float dmgInCreased = dmg * 1f / 100 * maxIncreasedDmg;
                dmg += (int)dmgInCreased;
            }
            else
            {
                float dmgInCreased = dmg * 1f / 100 * minIncreasedDmg;
                dmg += (int)dmgInCreased;
            }
        }
    }
}


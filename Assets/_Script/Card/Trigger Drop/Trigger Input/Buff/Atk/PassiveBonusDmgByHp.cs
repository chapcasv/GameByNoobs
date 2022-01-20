using UnityEngine;


namespace PH
{
    [CreateAssetMenu(fileName = "Passive", menuName = "ScriptableObject/Card/Trigger Drop/Input/Buff/Passive Bonus Dmg By Hp")]
    public class PassiveBonusDmgByHp : Buff
    {
        [SerializeField] CalculatorDmgByBigHP calculator;

        public override void Excute(BaseUnit unit)
        {
            var atkSystem = unit.GetAtkSystem;

            if (buffOneRound)
            {
                atkSystem.AddOneRoundCal(calculator);
            }
            else
            {
                atkSystem.AddBaseCaculator(calculator);
            }
        }

    }
}


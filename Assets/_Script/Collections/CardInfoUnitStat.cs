using TMPro;
using UnityEngine;
namespace PH
{
    public class CardInfoUnitStat : MonoBehaviour
    {
        [Header("Stat Unit")]
        [SerializeField] TextMeshProUGUI orPhysicalDmg;
        [SerializeField] TextMeshProUGUI orMagicPower;
        [SerializeField] TextMeshProUGUI orArmor;
        [SerializeField] TextMeshProUGUI orMagicResist;
        [SerializeField] TextMeshProUGUI orAtkSpeed;
        [SerializeField] TextMeshProUGUI orRange;
        [SerializeField] TextMeshProUGUI orLifeSteal;
        [SerializeField] TextMeshProUGUI orCritRate;
        [SerializeField] TextMeshProUGUI orCritDmg;


        [SerializeField] TextMeshProUGUI hpValue;
        [SerializeField] TextMeshProUGUI manaValue;

        public void LoadInfoBar(CardUnit unit)
        {
            hpValue.text = unit.Hp.ToString() + "/" + unit.Hp.ToString();
            manaValue.text = unit.ManaStart.ToString() + "/" + unit.ManaMax.ToString();
        }

        public void LoadCardUnitInfoStat(CardUnit unit)
        {
            orPhysicalDmg.text = unit.Damage.ToString();
            orMagicPower.text = GameConst.MAGIC_POWER_DEFAULT.ToString() + "%";
            orArmor.text = unit.Armor.ToString();
            orMagicResist.text = unit.MagicResist.ToString();
            orAtkSpeed.text = unit.AtkSpeed.ToString();
            orRange.text = unit.Range.ToString();
            orLifeSteal.text = GameConst.LIFE_STEAL_DEFAULT.ToString() + "%";
            orCritRate.text = GameConst.CRIT_RATE_DEFAULT.ToString() + "%";
            orCritDmg.text = GameConst.CRIT_DMG_DEFAULT.ToString() + "%";
        }
        public void LoadUnitInfoStat(BaseUnit unit)
        {
            var Atk = unit.GetAtkSystem;
            var USS = unit.GetUnitSurvivalStat;

            orPhysicalDmg.text = Atk.ORPhysicalDamage.ToString();
            orMagicPower.text = Atk.ORMagicPower.ToString() + "%";
            orArmor.text = USS.ORArmor.ToString();
            orMagicResist.text = USS.ORMagicResist.ToString();
            orAtkSpeed.text = Atk.ORAtkSpd.ToString();

            orLifeSteal.text = Atk.ORLifeSteal.ToString() + "%";
            orCritRate.text = Atk.ORCritRate.ToString() + "%";
            orCritDmg.text = Atk.ORCritDmg.ToString() + "%";
            
        }
      
        public void LoadInfoBar(BaseUnit unit)
        {
            var USS = unit.GetUnitSurvivalStat;
            var mana = unit.GetManaSystem;

            hpValue.text = USS.ORCurrentHP.ToString() + "/" + USS.ORMaxHP.ToString();
            manaValue.text = mana.ORManaCurrent.ToString() + "/" + mana.ORMaxMana.ToString();

        }
    }

}

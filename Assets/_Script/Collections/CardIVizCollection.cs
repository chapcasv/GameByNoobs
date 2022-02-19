using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace PH
{
    public class CardIVizCollection : CardVisual
    {
        [SerializeField] TextMeshProUGUI discription;
        [SerializeField] TextMeshProUGUI hpValue;
        [SerializeField] TextMeshProUGUI manaValue;

        [SerializeField] TextMeshProUGUI orPhysicalDmg;
        [SerializeField] TextMeshProUGUI orMagicPower;
        [SerializeField] TextMeshProUGUI orArmor;
        [SerializeField] TextMeshProUGUI orMagicResist;
        [SerializeField] TextMeshProUGUI orAtkSpeed;
        [SerializeField] TextMeshProUGUI orRange;
        [SerializeField] TextMeshProUGUI orLifeSteal;
        [SerializeField] TextMeshProUGUI orCritRate;
        [SerializeField] TextMeshProUGUI orCritDmg;

        [SerializeField] TextMeshProUGUI abilityName;
        [SerializeField] TextMeshProUGUI abilityDiscription;

        [SerializeField] private ALLCard _allCards;
        private Image[] _iconFactions;
        private const int MAGIC_POWER_DEFAULT = 100;
        private const int CRIT_RATE_DEFAULT = 25;
        private const int CRIT_DMG_DEFAULT = 130;
        private const int LIFE_STEAL_DEFAULT = 0;
        public void LoadUnit(BaseUnit unit)
        {
            int baseID = unit.GetID;
            Card card = _allCards.GetCard(baseID);

            LoadCard(card);
            LoadInfoUnit(unit);
            
        }
        protected override void LoadCard(Card card)
        {
            base.LoadCard(card);
            LoadCardDiscription(card);
            LoadInfoByType(card);
        }

        private void LoadInfoUnit(BaseUnit unit)
        {
            LoadInfoBar(unit);
            LoadUnitInfoStat(unit);
            LoadUnitAbility(unit);
        }
        private void LoadCardDiscription(Card card)
        {
            discription.text = card.GetDiscription;
        }

        private void LoadInfoByType(Card card)
        {
            var type = card.GetCardType();

            if (type == TypeMode.UNIT)
            {
                LoadCardInfoUnit(card);
            }
            else LoadInfo(card);
        }
        private void LoadUnitAbility(BaseUnit unit)
        {
            Ability a = unit.GetAtkSystem.GetAbility;

            abilityName.text = a.GetAbilityName;
            abilityDiscription.text = a.GetDiscription(unit);

        }

        private void LoadUnitInfoStat(BaseUnit unit)
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

        private void LoadInfoBar(BaseUnit unit)
        {
            var USS = unit.GetUnitSurvivalStat;
            var mana = unit.GetManaSystem;

            hpValue.text = USS.BaseMaxHP.ToString();
            manaValue.text = mana.BaseMaxMana.ToString();

        }

        private void LoadInfo(Card card)
        {
            throw new NotImplementedException();
        }

        private void LoadCardInfoUnit(Card card)
        {
            CardUnit unit = (CardUnit)card;

            LoadInfoBar(unit);
            LoadCardUnitInfoStat(unit);
            LoadUnitAbility(unit);
        }

        private void LoadUnitAbility(CardUnit unit)
        {
            Ability a = unit.Abitity;

            abilityName.text = a.GetAbilityName;
            abilityDiscription.text = a.GetDiscription(unit);
        }

        private void LoadCardUnitInfoStat(CardUnit unit)
        {
            orPhysicalDmg.text = unit.Damage.ToString();
            orMagicPower.text = MAGIC_POWER_DEFAULT.ToString() + "%";
            orArmor.text = unit.Armor.ToString();
            orMagicResist.text = unit.MagicResist.ToString();
            orAtkSpeed.text = unit.AtkSpeed.ToString();
            orRange.text = unit.Range.ToString();
            orLifeSteal.text = LIFE_STEAL_DEFAULT.ToString() + "%";
            orCritRate.text = CRIT_RATE_DEFAULT.ToString() + "%";
            orCritDmg.text = CRIT_DMG_DEFAULT.ToString() + "%";
        }

        private void LoadInfoBar(CardUnit unit)
        {
            hpValue.text = unit.Hp.ToString();
            manaValue.text = unit.ManaMax.ToString();
        }

     
    }

}

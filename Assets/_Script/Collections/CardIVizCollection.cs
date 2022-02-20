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

        [Header("Stat Unit")]
        [SerializeField] private GameObject surStat;
        [SerializeField] private GameObject unitInfor;
        [SerializeField] private GameObject unitSkill;
        [SerializeField] TextMeshProUGUI orPhysicalDmg;
        [SerializeField] TextMeshProUGUI orMagicPower;
        [SerializeField] TextMeshProUGUI orArmor;
        [SerializeField] TextMeshProUGUI orMagicResist;
        [SerializeField] TextMeshProUGUI orAtkSpeed;
        [SerializeField] TextMeshProUGUI orRange;
        [SerializeField] TextMeshProUGUI orLifeSteal;
        [SerializeField] TextMeshProUGUI orCritRate;
        [SerializeField] TextMeshProUGUI orCritDmg;

        [Header("Ability")]
        [SerializeField] Image abilityIcon;
        [SerializeField] TextMeshProUGUI abilityName;
        [SerializeField] TextMeshProUGUI abilityDiscription;
       

        [SerializeField] private ALLCard _allCards;
     
        private const int MAGIC_POWER_DEFAULT = 100;
        private const int CRIT_RATE_DEFAULT = 25;
        private const int CRIT_DMG_DEFAULT = 130;
        private const int LIFE_STEAL_DEFAULT = 0;

        public void LoadCardInformation(Card card)
        {
            LoadCard(card);
        }
        protected override void LoadCard(Card card)
        {
            base.LoadCard(card);
            LoadCardDiscription(card);
            LoadInfoByType(card);
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
     
        private void LoadInfo(Card card)
        {
            unitInfor.SetActive(false);
            unitSkill.SetActive(false);
            surStat.SetActive(false);
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
            abilityIcon.sprite = a.GetIcon;
            unitSkill.SetActive(true);
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
            unitInfor.SetActive(true);
        }

        private void LoadInfoBar(CardUnit unit)
        {
            hpValue.text = "HP: " + unit.Hp.ToString();
            manaValue.text = "Mana: " + unit.ManaMax.ToString();
            surStat.SetActive(true);
        }

     
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace PH
{
    public class CardVizInfoCollection : CardVisual
    {
        
        [SerializeField] Image backgroundAvt;
        [SerializeField] Image backgroundName;

        [SerializeField] TextMeshProUGUI discription;
        [SerializeField] TextMeshProUGUI hpValue;
        [SerializeField] TextMeshProUGUI manaValue;
        [SerializeField] TextMeshProUGUI priceValue;
        [Header("Stat Unit")]
        [SerializeField] private GameObject unitSurStat;
        [SerializeField] private GameObject cardDescription;
        [SerializeField] private GameObject unitDetail;
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

        [SerializeField] Button B_descriptionDetail;
        [SerializeField] Button B_UnitDetail;
     
       
        [SerializeField] private ALLCard _allCards;
     
        private const int MAGIC_POWER_DEFAULT = 100;
        private const int CRIT_RATE_DEFAULT = 25;
        private const int CRIT_DMG_DEFAULT = 130;
        private const int LIFE_STEAL_DEFAULT = 0;

        private void Start()
        {
            AddListenerDetail();
        }
        public void LoadCardInformation(Card card, CardCollectionUI cardUI)
        {
            LoadCard(card);
            LoadPrice(cardUI);
            LoadRankColor(card);

        }
       
        protected override void LoadCard(Card card)
        {
            base.LoadCard(card);
            LoadCardDiscription(card);
            LoadInfoByType(card);
        }
        private void AddListenerDetail()
        {
            B_descriptionDetail.onClick.AddListener(OnDescriptionDetailCallBack);
            B_UnitDetail.onClick.AddListener(OnUnitDetailCallBack);
        }

        private void OnUnitDetailCallBack()
        {
            unitDetail.SetActive(true);
            cardDescription.SetActive(false);

        }

        private void OnDescriptionDetailCallBack()
        {
            unitDetail.SetActive(false);
            cardDescription.SetActive(true);
        }

        private void LoadPrice(CardCollectionUI card)
        {
            priceValue.text = card.Price.ToString(); 
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
            unitSurStat.SetActive(false);
            cardDescription.SetActive(true);
            unitDetail.SetActive(false);
            DeInteractive(false);
        }

        private void LoadCardInfoUnit(Card card)
        {
            
            CardUnit unit = (CardUnit)card;
            DeInteractive(true);
            LoadInfoBar(unit);
            
            B_descriptionDetail.Select();
            //LoadCardUnitInfoStat(unit);
            //LoadUnitAbility(unit);
        }
        private void DeInteractive(bool active)
        {
            B_descriptionDetail.interactable = active;
            B_UnitDetail.interactable = active;
        }
        private void LoadUnitAbility(CardUnit unit)
        {
            Ability a = unit.Abitity;

            abilityName.text = a.GetAbilityName;
            abilityDiscription.text = a.GetDiscription(unit);
            abilityIcon.sprite = a.GetIcon;
            
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
            hpValue.text = unit.Hp.ToString() + "/" + unit.Hp.ToString();
            manaValue.text = unit.ManaStart.ToString() + "/" + unit.ManaMax.ToString();
            unitSurStat.SetActive(true);
        }
        private void LoadRankColor(Card card)
        {
            var rank = card.GetRank;
            backgroundAvt.color = rank.BackGroundColor;
            backgroundName.color = rank.BaseColor;
        }
        

    }

}

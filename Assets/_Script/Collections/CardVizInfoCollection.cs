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
        [System.Serializable]
        public class InforTab
        {
            public Button main;
            public Image selected;
            public Action Onselected;
            public void Init()
            {
                Onselected += OnClickCallBack;
                main.onClick.AddListener(() => Onselected?.Invoke());
            }

            private void OnClickCallBack()
            {
                selected.gameObject.SetActive(true);
                
            }
            public void DeSelect()
            {
                selected.gameObject.SetActive(false);
            }
            public void Setactive(bool active)
            {
                main.interactable = active;
            }
        }
        [SerializeField] Image backgroundAvt;
        [SerializeField] Image backgroundName;
        [Header("ButtonTab")]
        [SerializeField] private InforTab descriptionTab;
        [SerializeField] private InforTab detailButtonTab;
        [Header("Survival Stat")]
        
        [SerializeField] TextMeshProUGUI hpValue;
        [SerializeField] TextMeshProUGUI manaValue;
       
        [Header("Stat Unit")]
        [SerializeField] private GameObject borderBar;
        [SerializeField] private GameObject cardDescription;
        [SerializeField] private GameObject unitDetails;

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
       
        [Header("Other Information")]
        [SerializeField] TextMeshProUGUI discription;
        [SerializeField] private ALLCard _allCards;
        [SerializeField] TextMeshProUGUI priceValue;
        private const int MAGIC_POWER_DEFAULT = 100;
        private const int CRIT_RATE_DEFAULT = 25;
        private const int CRIT_DMG_DEFAULT = 130;
        private const int LIFE_STEAL_DEFAULT = 0;

        public void LoadCardInformation(Card card, CardCollectionUI cardUI)
        {
            LoadCard(card);
            LoadPrice(cardUI);
            LoadRankColor(card);
        }
        private void Start()
        {
            detailButtonTab.Init();
            descriptionTab.Init();

            descriptionTab.Onselected += OnDescriptionSelect;
            detailButtonTab.Onselected += OnDetailSelect;
        }
        private void OnDescriptionSelect()
        {
            detailButtonTab.DeSelect();
            cardDescription.SetActive(true);
            unitDetails.SetActive(false);
        }
        private void OnDetailSelect()
        {
            descriptionTab.DeSelect();
            cardDescription.SetActive(false);
            unitDetails.SetActive(true);
            
        }
        protected override void LoadCard(Card card)
        {
            base.LoadCard(card);
            LoadCardDiscription(card);
            LoadInfoByType(card);
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
            cardDescription.SetActive(true);
            NonUnitLoad();
        }

        private void NonUnitLoad()
        {
            unitDetails.SetActive(false);
            borderBar.SetActive(false);
            detailButtonTab.Setactive(false);
            descriptionTab.Setactive(false);
            detailButtonTab.DeSelect();
            descriptionTab.DeSelect();
        }

        private void LoadCardInfoUnit(Card card)
        {
            CardUnit unit = (CardUnit)card;

            LoadInfoBar(unit);
            LoadCardUnitInfoStat(unit);
            LoadUnitAbility(unit);
            detailButtonTab.Setactive(true);
            descriptionTab.Setactive(true);
            descriptionTab.Onselected?.Invoke();
            
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
            borderBar.SetActive(true);
        }
        private void LoadRankColor(Card card)
        {
            var rank = card.GetRank;
            backgroundAvt.color = rank.BackGroundColor;
            backgroundName.color = rank.BaseColor;
        }
        
    }

}

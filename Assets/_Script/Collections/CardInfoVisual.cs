using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [RequireComponent(typeof(CardInfoUnitStat))]

    [RequireComponent(typeof(CardInfoUnitAbility))]
    public class CardInfoVisual : CardVisual
    {
        [Header("CHILD PROPERTIES")]
        [SerializeField] protected GameObject cardDescription;
        [SerializeField] protected GameObject unitStat;
        [SerializeField] protected GameObject borderBar;
        [SerializeField] Image backgroundAvt;
        [SerializeField] Image backgroundName;
        protected CardInfoUnitStat cardUnitStat;
        protected CardInfoUnitAbility cardAbility;
       
        public void Initialized()
        {
            cardUnitStat = GetComponent<CardInfoUnitStat>();
            cardAbility = GetComponent<CardInfoUnitAbility>();
        }
        protected override void LoadCard(Card card)
        {
            base.LoadCard(card);
            LoadRankColor(card);
            LoadInfoByType(card);
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
        protected virtual void LoadCardInfoUnit(Card card)
        {
            CardUnit unit = (CardUnit)card;
            cardUnitStat.LoadInfoBar(unit);
            borderBar.SetActive(true);
            cardUnitStat.LoadCardUnitInfoStat(unit);
            cardAbility.LoadUnitAbility(unit);
        }
        protected virtual void LoadInfo(Card card)
        {
            borderBar.SetActive(false);
            unitStat.SetActive(false);
        }
        protected void LoadRankColor(Card card)
        {
            var rank = card.GetRank;
            backgroundAvt.color = rank.BackGroundColor;
            backgroundName.color = rank.BaseColor;
        }
    }
}


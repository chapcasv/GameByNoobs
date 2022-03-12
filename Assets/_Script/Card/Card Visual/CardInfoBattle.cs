using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PH
{

    public class CardInfoBattle : CardInfoVisual
    {
        [SerializeField] private Button B_leftClose;
        [SerializeField] private Button B_rightClose;
        [Header("Unit Item")]
        [SerializeField] GameObject unitItemPanel;
        [SerializeField] GameObject unitInfomation;
        [SerializeField] Image[] itemIcons;
        private ALLCard _allCard;

        public void Init(ALLCard aLLCard)
        {
            _allCard = aLLCard;
            gameObject.SetActive(false);
            B_leftClose.onClick.AddListener(() => Close());
            B_rightClose.onClick.AddListener(() => Close());
        }

        public void LoadUnit(BaseUnit unit)
        {
            int baseID = unit.GetID;

            Card card = _allCard.GetCard(baseID);
            LoadCard(card);

            LoadInfoUnit(unit);
            gameObject.SetActive(true);
        }
 
        protected override void LoadCard(Card c)
        {
            base.LoadCard(c);
            gameObject.SetActive(true);
        }
        private void LoadInfoUnit(BaseUnit unit)
        {
            cardUnitStat.LoadInfoBar(unit);
            borderBar.SetActive(true);
            cardUnitStat.LoadUnitInfoStat(unit);
            unitInfomation.SetActive(true);

            LoadUnitItem(unit.UnitEquipment._slots);
            cardAbility.LoadUnitAbility(unit);
        }
        protected override void LoadInfo(Card card)
        {
            base.LoadInfo(card);
            unitItemPanel.SetActive(false);
            unitInfomation.SetActive(false);
        }

        protected override void LoadCardInfoUnit(Card card)
        {
            base.LoadCardInfoUnit(card);
            unitInfomation.SetActive(true);
            LoadUnitItem();
        }
      
        private void LoadUnitItem()
        {
            for (int i = 0; i < itemIcons.Length; i++)
            {
                itemIcons[i].gameObject.SetActive(false);
            }
            unitItemPanel.SetActive(true);
        }
        
        private void LoadUnitItem(SlotItem[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].SlotFree)
                {
                    itemIcons[i].sprite = slots[i].GetIconItem();
                    itemIcons[i].gameObject.SetActive(true);
                }
                else
                {
                    itemIcons[i].gameObject.SetActive(false);
                }
            }
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            B_leftClose.onClick.RemoveAllListeners();
            B_rightClose.onClick.RemoveAllListeners();
        }
        //public override void LoadFaction(Faction[] factions)
        //{
        //    if (factions.Length == 0) return;

        //    factionIcon.sprite = factions[0].Icon;
        //}
    }
}


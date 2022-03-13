using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

        private RectTransform rect;
        private ALLCard _allCard;
        private Vector2 rightScene = new Vector2(780, 198);
        private Vector2 original;
        private const float speed = 0.3f;

        public void Init(ALLCard aLLCard)
        {
            _allCard = aLLCard;

            rect = GetComponent<RectTransform>();
            original = rect.anchoredPosition;
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
            ActiveGameObj();
        }
 
        protected override void LoadCard(Card c)
        {
            base.LoadCard(c);
            ActiveGameObj();
        }

        private void ActiveGameObj()
        {
            gameObject.SetActive(true);
            rect.DOAnchorPos(rightScene, speed);
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
            rect.DOAnchorPos(original, speed).OnComplete(() => gameObject.SetActive(false));
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


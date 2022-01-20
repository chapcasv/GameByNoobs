using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PH
{
    public class CardInfoVisual : CardVisual
    {
        [SerializeField] TextMeshProUGUI discription;
        [Header("Bar")]
        [SerializeField] GameObject barHolder;
        [SerializeField] TextMeshProUGUI hpValue;
        [SerializeField] TextMeshProUGUI manaValue;

        [Header("Unit Item")]
        [SerializeField] GameObject unitItemPanel;

        [Header("Unit Infomation")]

        [Header("Stat")]
        [SerializeField] GameObject unitInfomation;
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


        private Image[] _iconFactions;
        private TextMeshProUGUI[] _nameFactions;
        private const int MAGIC_POWER_DEFAULT = 100;
        private const int CRIT_RATE_DEFAULT = 25;
        private const int CRIT_DMG_DEFAULT = 130;
        private const int LIFE_STEAL_DEFAULT = 0;

        public void Init()
        {
            _iconFactions = new Image[factionSlotViz.Length];
            _nameFactions = new TextMeshProUGUI[factionSlotViz.Length];

            for (int i = 0; i < factionSlotViz.Length; i++)
            {
                _iconFactions[i] = factionSlotViz[i].GetComponent<Image>();
                _nameFactions[i] = factionSlotViz[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            }
        }

        protected override void LoadCard(Card c)
        {
            base.LoadCard(c);
            LoadCardDiscription(c);
            LoadInfoByType(c);
            gameObject.SetActive(true);
        }

        private void LoadInfoByType(Card card)
        {
            var type = card.GetCardType();

            if (type == TypeMode.UNIT)
            {
                LoadInfoUnit(card);
            }
            else LoadInfo(card);
        }

        //spell item
        private void LoadInfo(Card c)
        {
            barHolder.SetActive(false);
            unitInfomation.SetActive(false);
            unitItemPanel.SetActive(false);
        }

        private void LoadInfoUnit(Card card)
        {
            CardUnit unit = (CardUnit)card;

            LoadInfoBar(unit);
            LoadUnitInfoStat(unit);
            LoadUnitItem(unit);
            LoadUnitAbility(unit);
        }

        private void LoadInfoBar(CardUnit unit)
        {
            hpValue.text = unit.Hp.ToString() + "/" + unit.Hp.ToString();
            manaValue.text = unit.ManaStart.ToString() + "/" + unit.ManaMax.ToString();

            barHolder.SetActive(true);
        }

        private void LoadUnitInfoStat(CardUnit unit)
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

            unitInfomation.SetActive(true);
        }

        private void LoadUnitItem(CardUnit unit)
        {
            unitItemPanel.SetActive(true);
        }

        private void LoadUnitAbility(CardUnit unit)
        {
            Ability a = unit.Abitity;

            abilityName.text = a.GetAbilityName;
            abilityDiscription.text = a.GetDiscription(unit);
            abilityIcon.sprite = a.GetIcon;
        }

        private void LoadCardDiscription(Card card)
        {
            discription.text = card.GetDiscription;
        }

        public override void LoadFaction(Faction[] factions)
        {
            factionHolder.SetActive(true);

            if (factions.Length <= factionSlotViz.Length)
            {
                HidenFactionSlot(); //Reload
                for (int i = 0; i < factions.Length; i++)
                {   
                    if (factions[i].Icon == null) continue;

                    _iconFactions[i].sprite = factions[i].Icon;
                    _nameFactions[i].text = factions[i].FactionName;

                    factionSlotViz[i].SetActive(true);
                }
            }
        }
    }
}


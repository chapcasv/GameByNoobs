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
        private ALLCard _allCard;

        private const int MAGIC_POWER_DEFAULT = 100;
        private const int CRIT_RATE_DEFAULT = 25;
        private const int CRIT_DMG_DEFAULT = 130;
        private const int LIFE_STEAL_DEFAULT = 0;

        public void Init(ALLCard aLLCard)
        {
            _allCard = aLLCard;
            gameObject.SetActive(false);
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
            else LoadInfo();
        }

        //spell item
        private void LoadInfo()
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

        private void LoadInfoUnit(BaseUnit unit)
        {
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

        private void LoadInfoBar(BaseUnit unit)
        {
            var USS = unit.GetUnitSurvivalStat;
            var mana = unit.GetManaSystem;

            hpValue.text = USS.ORCurrentHP.ToString() + "/" + USS.ORMaxHP.ToString();
            manaValue.text = mana.ORManaCurrent.ToString() + "/" + mana.ORMaxMana.ToString();

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
            unitInfomation.SetActive(true);
        }

        private void LoadUnitItem(CardUnit unit)
        {
            unitItemPanel.SetActive(true);
        }

        private void LoadUnitItem(BaseUnit unit) => unitItemPanel.SetActive(true);

        private void LoadUnitAbility(CardUnit unit)
        {
            Ability a = unit.Abitity;

            abilityName.text = a.GetAbilityName;
            abilityDiscription.text = a.GetDiscription(unit);
            abilityIcon.sprite = a.GetIcon;
        }

        private void LoadUnitAbility(BaseUnit unit)
        {
            Ability a = unit.GetAtkSystem.GetAbility;

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
            if (factions.Length == 0) return;

            factionIcon.sprite = factions[0].Icon;
        }
    }
}


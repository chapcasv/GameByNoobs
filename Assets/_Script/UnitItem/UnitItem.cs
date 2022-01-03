using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class UnitItem
    {
        private Sprite _icon;

        private ItemBonusSurvivalStat _itemBonusSurvivalStat;
        private ItemBonusPhysicalAtkStat _itemBonusPhysicalAtkStat;

        public UnitItem(Sprite icon, CardItem cardItem)
        {
            _icon = icon;
            _itemBonusSurvivalStat = cardItem.GetBonusSurvivalStat;
            _itemBonusPhysicalAtkStat = cardItem.GetBonusPhysicalAtkStat;
        }

        public void SetSurvivalStat(UnitSurvivalStat uss)
        {   
            if(_itemBonusSurvivalStat != null)
            {
                _itemBonusSurvivalStat.UpSurvialStat(uss);
            }
        }

        public void SetPhysicalAtkStat(UnitAtkSystem upa)
        {
            if(_itemBonusPhysicalAtkStat != null)
            {
                _itemBonusPhysicalAtkStat.UpPhysicalAtkStat(upa);
            }
        }

        public Sprite Icon() => _icon;
    }
}


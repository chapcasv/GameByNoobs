using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class UnitItem
    {
        private Sprite _icon;
        private bool isOnRound;

        public UnitItem(Sprite icon, CardItem cardItem)
        {
            _icon = icon;
            this.isOnRound = cardItem.IsOnRound;
        }
        public Sprite Icon() => _icon;
        public bool IsOnRound{get => isOnRound;}
    }
}


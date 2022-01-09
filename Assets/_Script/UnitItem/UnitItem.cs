using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class UnitItem
    {
        private Sprite _icon;

        public UnitItem(Sprite icon, CardItem cardItem)
        {
            _icon = icon;
         
        }
        public Sprite Icon() => _icon;
    }
}


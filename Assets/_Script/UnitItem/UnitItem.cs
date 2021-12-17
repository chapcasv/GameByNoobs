using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class UnitItem
    {
        private Sprite _icon;
        private bool isHave;

        public UnitItem(Sprite icon)
        {
            _icon = icon;
            IsHave = false;
        }

        public bool IsHave { get => isHave; set => isHave = value; }

        public Sprite Icon() => _icon;
    }
}


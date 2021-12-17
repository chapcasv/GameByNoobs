using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class SlotItem
    {
        private UnitItem item;
        private bool slotFree;

        public UnitItem Item { get => item; set => item = value; }
        public bool SlotFree { get => slotFree; set => slotFree = value; }

        public SlotItem()
        {
            Item = null;
            SlotFree = true;
        }

        public Sprite GetIconItem() => Item.Icon();
    }
}


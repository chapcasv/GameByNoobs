using UnityEngine;

namespace PH
{
    public class SlotItem
    {
        private UnitItem item;
        private bool slotFree;

        public UnitItem Item { get => item; }
        public bool SlotFree { get => slotFree; }

        public SlotItem()
        {
            item = null;
            slotFree = true;
        }

        public Sprite GetIconItem() => Item.Icon();

        public void SetItem(UnitItem item)
        {
            slotFree = false;
            this.item = item;
        }
    }
}


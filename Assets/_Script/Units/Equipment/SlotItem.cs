using UnityEngine;

namespace PH
{
    public class SlotItem
    {
        private UnitItem item;
        private bool slotFree;

        private bool equipSlotOneRound;
        public UnitItem Item { get => item; }
        public bool SlotFree { get => slotFree; }

        public bool EquipSlotOneRound { get => equipSlotOneRound; }
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
            equipSlotOneRound = item.IsOnRound;
        }
        public void ClearItem()
        {
            slotFree = true;
        }
    }
}


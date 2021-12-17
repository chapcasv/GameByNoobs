using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PH
{
    public class UnitEquipment : MonoBehaviour
    {
        public event Action<SlotItem[]> OnEquipItem;

        [SerializeField] ElementImage cardItemArt;

        private SlotItem[] _slots;

        public bool Equip(CardItem item)
        {
            Sprite icon = GetIcon(item);

            int index = GetIndexSlotFree();
            if(index == int.MaxValue) return false;

            if(icon != null)
            {
                UnitItem newItem = new UnitItem(icon);
                _slots[index].Item = newItem;
                _slots[index].SlotFree = false;
                OnEquipItem?.Invoke(_slots);
                return true;
            }
            else return false;

        }

        private int GetIndexSlotFree()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                if(_slots[i].SlotFree)
                {
                    return i;
                }
            }
            return int.MaxValue; //full slot
        }

        private Sprite GetIcon(CardItem item)
        {
            Sprite icon = null;

            for (int i = 0; i < item.baseProperties.Length; i++)
            {
                if (item.baseProperties[i].element == cardItemArt)
                {
                    icon = item.baseProperties[i].sprite;
                    return icon;
                }
            }
            return icon;
        }

        public void SetUp()
        {
            _slots = new SlotItem[] { new SlotItem(), new SlotItem(), new SlotItem() };
        }
        
    }
}


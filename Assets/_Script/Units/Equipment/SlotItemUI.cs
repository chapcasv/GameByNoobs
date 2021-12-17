
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class SlotItemUI : MonoBehaviour
    {
        [SerializeField] Image[] slot;

        private UnitEquipment _unitEquipment;

        private void Awake()
        {
            _unitEquipment = GetComponentInParent<UnitEquipment>();
            _unitEquipment.OnEquipItem += OnEquipItem;
        }

        private void OnEquipItem(SlotItem[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {   
                if(!slots[i].SlotFree)
                {
                    slot[i].sprite = slots[i].GetIconItem();
                    slot[i].gameObject.SetActive(true);
                }
                else
                {
                    slot[i].gameObject.SetActive(false);
                }
                
            };
        }

        private void OnDisable()
        {
            _unitEquipment.OnEquipItem -= OnEquipItem;
        }
    }
}


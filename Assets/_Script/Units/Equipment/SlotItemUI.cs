using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class SlotItemUI : MonoBehaviour
    {
        [SerializeField] Image[] slotUI;

        private UnitEquipment _unitEquipment;

        private void Awake()
        {
            _unitEquipment = GetComponentInParent<UnitEquipment>();
           
        }

        private void OnEnable()
        {
            _unitEquipment.OnSlotChange += OnEquipItem;
        }

        private void OnEquipItem(SlotItem[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {   
                if(!slots[i].SlotFree)
                {
                    slotUI[i].sprite = slots[i].GetIconItem();
                    slotUI[i].gameObject.SetActive(true);
                }
                else
                {
                    slotUI[i].gameObject.SetActive(false);
                }
            }
        }

        private void OnDisable()
        {
            _unitEquipment.OnSlotChange -= OnEquipItem;
        }
    }
}


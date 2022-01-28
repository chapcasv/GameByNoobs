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
            _unitEquipment.OnDestroyItemOnRound += OnReMoveSlotItem;
        }

        private void OnEquipItem(SlotItem[] slots)
        {

            for (int i = 0; i < slots.Length; i++)
            {   
                if(!slots[i].SlotFree)
                {
                    slotUI[i].sprite = slots[i].GetIconItem();
                    slotUI[i].gameObject.SetActive(true);
                    slotUI[i].transform.parent.gameObject.SetActive(true);
                }
                else
                {
                    slotUI[i].transform.parent.gameObject.SetActive(false);
                }
            }
        }

        private void OnReMoveSlotItem(SlotItem[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].SlotFree && slots[i].EquipSlotOneRound)
                {
                    slotUI[i].transform.parent.gameObject.SetActive(false);
                }
                else return;
            }
        }

        private void OnDisable()
        {
            _unitEquipment.OnSlotChange -= OnEquipItem;
            _unitEquipment.OnDestroyItemOnRound -= OnReMoveSlotItem;
        }
    }
}


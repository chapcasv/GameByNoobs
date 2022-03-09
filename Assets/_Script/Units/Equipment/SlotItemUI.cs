using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class SlotItemUI : MonoBehaviour
    {
        [SerializeField] Image[] slotUI;

        private UnitEquipment _unitEquipment;
        private MoveCanvasHoder popUp;
        private bool isFirstEquip;
        private bool canMoveDown;
        private void Awake()
        {
            _unitEquipment = GetComponentInParent<UnitEquipment>();
            popUp = GetComponent<MoveCanvasHoder>();
            isFirstEquip = true;
            canMoveDown = true;
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
            if (!isFirstEquip) return;
            popUp.OnChangeCanvasHoder?.Invoke(true);
            isFirstEquip = false;
            canMoveDown = true;
        }

        private void OnReMoveSlotItem(SlotItem[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].SlotFree && slots[i].EquipSlotOneRound)
                {
                    slotUI[i].transform.parent.gameObject.SetActive(false);
                }
            }
            bool equiped = EquipedItem(slots);
            if (equiped) return;
            if (!canMoveDown) return;
            popUp.OnChangeCanvasHoder?.Invoke(equiped);
            isFirstEquip = true;
            canMoveDown = false;
        }
        private bool EquipedItem(SlotItem[] slots)
        {
            bool equiped = false;
            for (int i = 0; i < slots.Length; i++)
            {
                if (!slots[i].EquipSlotOneRound && !slots[i].SlotFree)
                {
                    equiped = true;
                    break;
                }
            }
            return equiped;
          
        }
        
        private void OnDisable()
        {
            _unitEquipment.OnSlotChange -= OnEquipItem;
            _unitEquipment.OnDestroyItemOnRound -= OnReMoveSlotItem;
        }
    }
}


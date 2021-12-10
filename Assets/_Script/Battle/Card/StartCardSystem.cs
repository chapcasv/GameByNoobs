using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SO;
using UnityEngine.UI;

namespace PH
{
    
    public class StartCardSystem : MonoBehaviour
    {

        [SerializeField] Button btnComplete;

        public event Action OnStartCard;
        public event Action OnReplace;
        public static event Action OnComplete;
        public LocalPlayer playerTeam;

        private void Awake()
        {
            btnComplete.onClick.AddListener(Complete);
        }

        private void Start()
        {
            GetStartCard();
            OnStartCard?.Invoke();
        }

        private void GetStartCard()
        {
            //Player start with 4 card
            for (int i = 0; i < 4; i++)
            {
                playerTeam.DrawStartCard();
            }
        }

        //Tracking ===Start Card ===
        public void Replace(Transform slot)
        {
            int startCardIndex = GetSlotIndex(slot);
            playerTeam.ReplaceCardHand(startCardIndex);
            OnReplace?.Invoke();
        }

        private int GetSlotIndex(Transform slot) => slot.GetSiblingIndex();

        private void Complete()
        {
            OnComplete?.Invoke();
            Hiden();
        }

        private void Hiden() => gameObject.SetActive(false);

    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class UIManager : MonoBehaviour
    {
        public ALLCard allcardManager;
        public GameObject[] cardSlots;
        private void Start()
        {
            ShowUICard();
            
        }
        private void ShowUICard()
        {
            for (int i = 0; i < allcardManager.allCards.Count; i++)
            {
                cardSlots[i].transform.GetChild(2).GetComponent<Image>().sprite = allcardManager.allCards[i].baseProperties[0].sprite;
            }
        }
    }

}

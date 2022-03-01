using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class DeckSetUp : MonoBehaviour
    {
        [SerializeField] private Button B_setup;
        [SerializeField] private GameObject showChildCard;
        [SerializeField] private GameObject deck;
        private void Start()
        {
            B_setup.onClick.AddListener(SetUpDeckCallBack);
            
        }

        private void SetUpDeckCallBack()
        {
            deck.SetActive(false);
            showChildCard.SetActive(true);
            
        }
    }
}


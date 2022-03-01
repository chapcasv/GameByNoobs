using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class ChildCardInfor : MonoBehaviour
    {
        [SerializeField] private Button B_done;
        [SerializeField] private GameObject deckScreen;
        [SerializeField] private GameObject showChildCard;
        private void Start()
        {
            B_done.onClick.AddListener(GoDeckCallBack);
        }

        private void GoDeckCallBack()
        {
            deckScreen.SetActive(true);
            showChildCard.SetActive(false);

        }
    }
}


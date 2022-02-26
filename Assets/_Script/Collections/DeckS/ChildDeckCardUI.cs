using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    public class ChildDeckCardUI : MonoBehaviour
    {
        [SerializeField] private Button done;
        [SerializeField] private GameObject screenObj;

        private void Start()
        {
            done.onClick.AddListener(Close);
        }

        private void Close()
        {
            screenObj.SetActive(false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using System;
using UnityEngine.UI;
using TMPro;

namespace PH
{

    public class BattleUIManager : MonoBehaviour
    {
        [SerializeField] PVE_Raid currentRaid;
        [SerializeField] LocalPlayer player;

        [Header("BattleInfo")]
        [SerializeField] RectTransform battleInfo;
        [SerializeField] TextMeshProUGUI waveCount;

        private void Awake()
        {
            StartCardSystem.OnComplete += MoveBattleInfo;
     
       
        }

        void Start()
        {
           
        }


        private void MoveBattleInfo()
        {
            battleInfo.gameObject.SetActive(true);
        }



    }
}


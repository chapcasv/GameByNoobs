using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using HexColor;

namespace PH
{
    public class BattleNotifyUI : MonoBehaviour
    {
        [SerializeField] GameObject notifyPhaseUI;
        [SerializeField] TextMeshProUGUI notify;
        [SerializeField] Image panelBG;


        public void SetNotifyRound(string text)
        {
            notify.text = "Vòng " + text;
            //panelBG.color = HexColorString.Blue32;
            panelBG.color = Color.green;
            notifyPhaseUI.SetActive(true);

            StartCoroutine(Hiden());
        }

        public void SetNotifyBeforeBattle()
        {
            notify.text = "Chiến Đấu";
            panelBG.color = Color.yellow;
            notifyPhaseUI.SetActive(true);

            StartCoroutine(Hiden());

        }

        private IEnumerator Hiden()
        {
            yield return new WaitForSeconds(1.5f);
            notifyPhaseUI.SetActive(false);
        }
    }
}


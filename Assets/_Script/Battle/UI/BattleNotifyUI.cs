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
        [SerializeField] TextMeshProUGUI notify;
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            gameObject.SetActive(false);
        }

        public void SetNotifyRound(string text)
        {
            notify.text = "Vòng " + text;
            gameObject.SetActive(true);
            anim.SetTrigger("Display Blue");

            StartCoroutine(Hiden());
        }

        public void SetNotifyBeforeBattle()
        {
            notify.text = "Chiến Đấu";
            gameObject.SetActive(true);
            anim.SetTrigger("Display Red");

            StartCoroutine(Hiden());
        }

        private IEnumerator Hiden()
        {
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }
    }
}


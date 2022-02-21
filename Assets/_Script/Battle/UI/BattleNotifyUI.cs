using System.Collections;
using TMPro;
using UnityEngine;

namespace PH
{
    public class BattleNotifyUI : MonoBehaviour
    {
        private const string TEAM_FIGHT = "Chiến Đấu";
        private const string ROUND = "Vòng ";
        [SerializeField] TextMeshProUGUI notify;
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            gameObject.SetActive(false);
        }

        public void SetNotifyRound(string text)
        {
            notify.text = ROUND + text;
            gameObject.SetActive(true);
            anim.SetTrigger("Display Blue");

            StartCoroutine(Hiden());
        }

        public void SetNotifyBeforeBattle()
        {
            notify.text = TEAM_FIGHT;
            gameObject.SetActive(true);
            anim.SetTrigger("Display Red");

            StartCoroutine(Hiden());
        }

        private IEnumerator Hiden()
        {   
            //Fade time
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }
    }
}


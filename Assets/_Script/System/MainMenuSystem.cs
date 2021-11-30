using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace PH
{
    public class MainMenuSystem : MonoBehaviour
    {

        [SerializeField] PlayerSO player;

        [Header("UI")]
        [SerializeField] TextMeshProUGUI coin;
        [SerializeField] TextMeshProUGUI playerName;
        [SerializeField] GameObject selectPlayMode;

        void Start()
        {
            LoadPlayer();
        }

        private void LoadPlayer()
        {
            coin.text = player.Coin.ToString();
            playerName.text = player.PlayerName;
        }

        public void ShowPlayMode()
        {
            selectPlayMode.SetActive(true);
        }

        public void HidenPlayMode()
        {
            selectPlayMode.SetActive(false);
        }

        public void GotoPVE()
        {
            SceneManager.LoadScene(SceneSelect.PVE.ToString());
        }

        public void GoToCollection()
        {
            SceneManager.LoadScene(SceneSelect.Collection.ToString());
        }
    }
}


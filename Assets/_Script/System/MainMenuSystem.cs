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


        void Start()
        {
            LoadPlayer();
        }

        private void LoadPlayer()
        {
            coin.text = player.Coin.ToString();
            playerName.text = player.PlayerName;
        }

        public void GoToCollection()
        {
            SceneManager.LoadScene(SceneSelect.Collection.ToString());
        }
    }
}


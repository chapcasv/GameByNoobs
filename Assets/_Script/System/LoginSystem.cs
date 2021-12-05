using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;


namespace PH.Player
{
    public class LoginSystem : MonoBehaviour
    {

        [SerializeField] GameObject newPlayer_popUp;
        [SerializeField] TextMeshProUGUI input_PlayerName;
        [SerializeField] TextMeshProUGUI ruler_PlayerName;
        [SerializeField] PlayerSO player;
        [SerializeField] PlayerDefaultData defaultPlayer;


        public void StarGame()
        {
            if (player.IsHaveData) GoTo_mainMenu();
            else Active_newPlayerPopUp();       
        }

        private void Active_newPlayerPopUp()
        {
            newPlayer_popUp.SetActive(true);
        }

        public void ResetPlayer()
        {
            player.IsHaveData = false;
        }

        public void Create_newPlayer()
        {
            string playerName = input_PlayerName.GetComponent<TextMeshProUGUI>().text; ;

            if (InitializeNewPlayer.CanInitialize(playerName, player, defaultPlayer))
            {
                GoTo_mainMenu();
            }
            else NotifyCantUsePlayerName();
        }


        private void NotifyCantUsePlayerName()
        {
            ruler_PlayerName.text = "Tên nhân vật không được có kí tự đặc biệt";
        }

        

        public void unActive_newPlayerName_popUp()
        {
            newPlayer_popUp.SetActive(false);
        }

        public void GoTo_mainMenu()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }

        public void Quit_game()
        {
            Application.Quit();
        }


    }
}


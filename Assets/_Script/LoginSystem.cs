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
        [SerializeField] PlayerSO defaultPlayer;

        private bool newPlayer_popUp_IsActive = false;

        public void StarGame()
        {
            if (player.IsHaveData) GoTo_mainMenu();
            else active_newPlayerPopUp(); ;
        }

        private void active_newPlayerPopUp()
        {
            newPlayer_popUp_IsActive = !newPlayer_popUp_IsActive;
            newPlayer_popUp.SetActive(newPlayer_popUp_IsActive);
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
            //ruler_PlayerName.GetComponent<TextMeshProUGUI>().text = SystemString.rulePlayerName_Error;
        }

        

        public void unActive_newPlayerName_popUp()
        {
            newPlayer_popUp_IsActive = false;
            newPlayer_popUp.SetActive(newPlayer_popUp_IsActive);
        }

        public void GoTo_mainMenu()
        {
            //SceneManager.LoadScene(SelectScene.MainMenu.ToString());
        }

        public void Quit_game()
        {
            Application.Quit();
        }


    }
}


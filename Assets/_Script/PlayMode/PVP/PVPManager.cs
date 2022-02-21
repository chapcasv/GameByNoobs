using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PH
{
    public class PVPManager : MonoBehaviour
    {

        [SerializeField] PVP_AI_Bot currentPlayer;
        public static PVE_Mode pveMode;
        //private SoundManager sound;
        //private Player_Database player_data;




        private void Awake()
        {
            
        }


        public void Back_to_mainMenu()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }

        public void GoToBattle()
        {
            SceneManager.LoadScene(SceneSelect.Battle.ToString());
        }
    }

    public enum PVE_Mode
    {
        Tutorial,
        M15Wave,
        Story
    }

}

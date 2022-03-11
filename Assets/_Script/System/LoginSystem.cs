using PH.Save;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PH
{
    public class LoginSystem : MonoBehaviour
    {

        [SerializeField] GameObject newPlayer_popUp;
        [SerializeField] TextMeshProUGUI input_PlayerName;
        [SerializeField] TextMeshProUGUI ruler_PlayerName;
        [SerializeField] PlayerLocalSO playerSO;
        [SerializeField] PlayerDefaultData defaultPlayer;
        [SerializeField] ALLCard allCards;

        public void StarGame()
        {
            if (SaveSystem.IsHavePlayerData())
            {
                SaveSystem.LoadPlayer(playerSO,allCards);
                GoTo_mainMenu();
            }
            else Active_newPlayerPopUp();
        }

        private void Active_newPlayerPopUp()
        {
            newPlayer_popUp.SetActive(true);
        }

        public void ResetPlayer()
        {
            SaveSystem.RemovePlayerData();
        }

        public void Create_newPlayer()
        {
            string playerName = input_PlayerName.GetComponent<TextMeshProUGUI>().text; ;

            if (InitializeNewPlayer.CanInitialize(playerName, playerSO, defaultPlayer))
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


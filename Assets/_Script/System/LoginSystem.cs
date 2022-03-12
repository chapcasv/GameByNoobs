using PH.Save;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace PH
{
    public class LoginSystem : MonoBehaviour
    {
        private const string reloadMess = "Bạn muốn xóa dữ liệu cá nhân ?";
        private const string reloadTitle = "Xóa Dữ Liệu";
        private const string exitMess = "Bạn muốn thoát trò chơi ?";
        private const string exitTitle = "Thoát Trò Chơi";
        private const string reloadTextMess = "Xoá Dữ Liệu Thành Công";
        [SerializeField] GameObject newPlayer_popUp;
        [SerializeField] TextMeshProUGUI input_PlayerName;
        [SerializeField] TextMeshProUGUI ruler_PlayerName;
        [SerializeField] PlayerLocalSO playerSO;
        [SerializeField] PlayerDefaultData defaultPlayer;
        [SerializeField] ALLCard allCards;
        [SerializeField] private Button B_reloadData;
        [SerializeField] private Button B_quit;

        private IPopUpManager popUpManager;
        private UITextPopUp UITextPopUp;
        private void Awake()
        {
            Addlisten();
            ThirdParties.Find<IPopUpManager>(out popUpManager);
        }
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
        private void Addlisten()
        {
            B_quit.onClick.AddListener(() => Quit_game());
            B_reloadData.onClick.AddListener(() => ResetPlayer());
        }
        public void ResetPlayer()
        {
            popUpManager.ShowPopUpConfirm(reloadMess, reloadTitle, ReloadData, null);
            
        }
        private void ReloadData()
        {
            ThirdParties.Find<UITextPopUp>(out var UITextPopUp);

            SaveSystem.RemovePlayerData();
            UITextPopUp.Set(reloadTextMess);

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
            popUpManager.ShowPopUpConfirm(exitMess, exitTitle, () => Application.Quit(), null);
            
        }
        private void OnDestroy()
        {
            B_quit.onClick.RemoveListener(() => Quit_game());
            B_reloadData.onClick.RemoveListener(() => ResetPlayer());
        }

    }
}


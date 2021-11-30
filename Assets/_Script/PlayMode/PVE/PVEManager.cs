using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PH
{
    public class PVEManager : MonoBehaviour
    {

        public static PVE_Raid currentRaid;
        public static PVE_Mode pveMode;
        //private SoundManager sound;
        //private Player_Database player_data;


        [Header("Raid Mode")]

        [SerializeField] GameObject B_raidContent;

        [SerializeField] List<PVE_Raid> raid15Wave;
        [SerializeField] Transform content_15Wave;
        private bool raid15Wave_Created = false;

        [SerializeField] List<PVE_Raid> raidStory;
        [SerializeField] Transform content_raidStory;
        private bool raidStory_Created = false;

        [SerializeField] List<PVE_Raid> raidTutorial;
        [SerializeField] Transform content_Tutorial;
        private bool raidTutorial_Created = false;


        [Header("Raid Infomation")]

        [SerializeField] TextMeshProUGUI raidDiscription;
        [SerializeField] TextMeshProUGUI raidName;
        [SerializeField] Image raidAvatar;


        private void Awake()
        {
            Selected_15Wave();
        }

        public void Selected_15Wave()
        {
            pveMode = PVE_Mode.M15Wave;
            if (raid15Wave_Created)
            {
                Show_List_R15Wave();
            }
            else Create_List_R15Wave();
        }

        public void Selected_Tutorial()
        {
            pveMode = PVE_Mode.Tutorial;
            if (raidTutorial_Created)
            {
                Show_List_RTutorial();
            }
            else Create_List_RaidTutorial();
        }

        private void Show_List_RTutorial()
        {
            content_Tutorial.gameObject.SetActive(true);
            content_15Wave.gameObject.SetActive(false);
            content_raidStory.gameObject.SetActive(false);
        }

        public void Selected_raidStory()
        {
            pveMode = PVE_Mode.M50Wave;
            if (raidStory_Created)
            {
                Show_List_raidStory();
            }
            else Create_List_raidStory();
        }

        private void Show_List_raidStory()
        {
            content_raidStory.gameObject.SetActive(true);
            content_Tutorial.gameObject.SetActive(false);
            content_15Wave.gameObject.SetActive(false);
        }

        private void Create_List_R15Wave()
        {
            GameObject temp;
            foreach (PVE_Raid raid in raid15Wave)
            {
                temp = Instantiate(B_raidContent, content_15Wave);
                temp.transform.GetComponent<Image>().sprite = raid.RaidAvatar;
                temp.transform.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Show_InfoStage(raid);
                });
            }
            raid15Wave_Created = true;
            Show_List_R15Wave();
            Show_InfoStage(raid15Wave[0]);
        }

        private void Show_List_R15Wave()
        {
            content_15Wave.gameObject.SetActive(true);
            //content_50Wave.gameObject.SetActive(false);
            //content_Tutorial.gameObject.SetActive(false);
        }



        private void Create_List_raidStory()
        {
            GameObject temp;
            foreach (PVE_Raid raid in raidStory)
            {

                temp = Instantiate(B_raidContent, content_raidStory);
                temp.transform.GetComponent<Image>().sprite = raid.RaidAvatar;
                temp.transform.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Show_InfoStage(raid);
                });
            }
            raidStory_Created = true;
            Show_List_raidStory();
        }

        private void Create_List_RaidTutorial()
        {
            GameObject temp;
            foreach (PVE_Raid raid in raidTutorial)
            {
                temp = Instantiate(B_raidContent, content_Tutorial);
                temp.transform.GetComponent<Image>().sprite = raid.RaidAvatar;
                temp.transform.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Show_InfoStage(raid);
                });
            }
            raidTutorial_Created = true;
            Show_List_RTutorial();
        }

        public void Show_InfoStage(PVE_Raid raid)
        {
            raidDiscription.text = raid.RaidDescription;
            raidAvatar.sprite = raid.RaidAvatar;
            currentRaid = raid;
        }


        //Need fix magic text
        public void Go_to_Battle()
        {
            //sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
            //sound.PlaySound("fight");
            SceneManager.LoadScene(SceneSelect.Battle.ToString());
        }

        public void Back_to_mainMenu()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }
    }

    public enum PVE_Mode
    {
        Tutorial,
        M15Wave,
        M50Wave
    }

}

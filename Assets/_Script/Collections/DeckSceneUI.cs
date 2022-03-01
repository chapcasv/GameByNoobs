using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace PH
{
    public class DeckSceneUI : MonoBehaviour
    {
        [SerializeField] private Button backToMenu;
        [SerializeField] private ALLCard allcard;
        [SerializeField] private ShowChildCard showChildCard;
        private void Start()
        {
            backToMenu.onClick.AddListener(BackToMainMenuCallBack);
            showChildCard.Init(allcard);
        }

        private void BackToMainMenuCallBack()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }
    }
}


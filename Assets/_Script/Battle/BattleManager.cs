using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] LocalPlayer localPlayer;

        private void Awake()
        {
            localPlayer.Init();
        }

    }
}


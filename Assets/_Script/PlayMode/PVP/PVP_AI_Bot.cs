using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "New Bot", menuName = "ScriptableObject/Play Mode/PVP/Bot")]
    public class PVP_AI_Bot : PVP_Enemy
    {
        public PVE_Mode RaidMode;

        public int RewardsMoney;

        public GameObject boardPrefab;


    }

}


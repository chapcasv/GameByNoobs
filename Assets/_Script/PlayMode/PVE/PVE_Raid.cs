using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "New PVE Raid", menuName = "ScriptableObject/Raid/PVE Raid")]
    public class PVE_Raid : ScriptableObject
    {
        public string RaidName;
        [Range(20, 40)]
        public int PlayerLife = 20;
        [Range(2, 99)]
        public int EnemyLife = 50;
        [Range(10, 20)]
        [Tooltip("Number coin player have when raid start")]
        public int PlayerStartCoin = 10;
        [TextArea]
        public string RaidDescription;
        public Sprite RaidAvatar;
        public PVE_Mode RaidMode;
        public Wave[] Waves;
        public int RewardsMoney;

        public GameObject boardPrefab;


    }

}


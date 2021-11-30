using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "New PVE Raid", menuName = "Raid/PVE Raid")]
    public class PVE_Raid : ScriptableObject
    {
        public string RaidName;
        [Range(20, 40)]
        public int PlayerLife = 20;
        [Range(50, 100)]
        public int EnemyLife = 50;
        [Range(10, 20)]
        public int Coin = 10;
        [TextArea]
        public string RaidDescription;
        public Sprite RaidAvatar;
        public PVE_Mode RaidMode;
        public PVE_RaidID ID;
        public List<Wave> ListWave;
        public int RewardsMoney;
    }

    public enum PVE_RaidID
    {
        Lava,
        Beach,
        Japan
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Play Mode/Sub/AI")]
    public class ModeSubAI : PlayModeSub
    {
        [SerializeField] PVPAiBot[] allBots;
        [SerializeField] int time;

        public override PlayModeEnemy GetEnemy()
        {
            int index = Random.Range(0, allBots.Length);


            return allBots[index];
        }

        public override int GetTimeFindMatch()
        {
            return time;
        }
    }
}


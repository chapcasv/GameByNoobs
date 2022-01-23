using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "new Input Buff AOE", menuName = "ScriptableObject/Card/Trigger Input/Buff/Buff AOE")]
    public class TriggerInputBuffAOE : TriggerInput
    {
        [SerializeField] FactionMode factionTakeBuff;

        [SerializeField] Buff[] buffs;
        
        public FactionMode FactionTakeBuff { get => factionTakeBuff; }
        public Buff[] Buffs { get => buffs;}
    }
}


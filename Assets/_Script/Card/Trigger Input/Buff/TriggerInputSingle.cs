using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "Input Single", menuName = "ScriptableObject/Card/Trigger Input/Buff/Single")]
    public class TriggerInputSingle : TriggerInput
    {
        [SerializeField] Buff[] buffs;
        public Buff[] GetBuffs => buffs;
    }
}


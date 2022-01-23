using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "new Input Drop Item", menuName = "ScriptableObject/Card/Trigger Input/Buff/Drop Item")]
    public class TriggerInputDropItem : TriggerInput
    {
        [SerializeField] Buff[] buffs;
        public Buff[] GetBuffs => buffs;
    }
}


using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "new Input Drop Item", menuName = "ScriptableObject/Card/Trigger Drop/Input/Buff/Drop Item")]
    public class TriggerInputDropItem : CardDropTriggerInput
    {
        [SerializeField] Buff[] buffs;

        public Buff[] GetBuffs => buffs;

    }
}


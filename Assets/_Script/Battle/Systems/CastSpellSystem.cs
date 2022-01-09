using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Spell System")]
    public class CastSpellSystem : ScriptableObject
    {
        public bool Drop(CardSpell cardSpell, Node nodeDrop, UnitTeam team)
        {
            return false;
        }
    }
}

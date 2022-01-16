using UnityEngine;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Faction/Container")]
    public class FactionContainer : ScriptableObject
    {
        [SerializeField] Faction[] factions;

        public Faction[] GetFactions => factions;
    }
}


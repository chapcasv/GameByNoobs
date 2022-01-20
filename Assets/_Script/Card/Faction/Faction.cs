using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class Faction : ScriptableObject
    {
        [SerializeField] string factionName;
        [SerializeField] Sprite icon;

        public string FactionName { get => factionName;}
        public Sprite Icon { get => icon; }
    }
}


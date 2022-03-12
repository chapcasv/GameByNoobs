using UnityEngine;

namespace PH
{
    public abstract class RankInstance : ScriptableObject
    {
        [SerializeField] Sprite icon;
        [SerializeField] string rankName;

        public Sprite Icon { get => icon;}
        public string RankName { get => rankName;}

    }
}


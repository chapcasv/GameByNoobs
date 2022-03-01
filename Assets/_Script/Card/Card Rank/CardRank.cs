using UnityEngine;
using UnityEngine.UI;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Rank")]
    public class CardRank : ScriptableObject
    {
        [SerializeField] protected Sprite icon;
        [SerializeField] protected Color baseColor;
        [SerializeField] protected Color bgColor;
        [SerializeField] int rankTier;

        public int RankTier => rankTier;

        public Color BaseColor => baseColor;

        public Color BackGroundColor => bgColor;

        public Sprite GetIcon => icon;
    }
}

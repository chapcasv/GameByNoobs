

using UnityEngine;

namespace HexColor
{
    public static class HexColorString 
    {
        private const string PHYSICAL_DMG = "#BF7A2D";
        private const string MAGIC_DMG = "#4FA8FF";
        private const string HEAL = "#FF3B3B";
        private static Color32 BLUE = new Color32(0, 111, 255, 255);

        public static Color32 Blue32 { get => BLUE; }

        public static string PhysicalDmg => PHYSICAL_DMG;

        public static string MagicDmg => MAGIC_DMG;

        public static string Heal { get => HEAL; }
    }
}


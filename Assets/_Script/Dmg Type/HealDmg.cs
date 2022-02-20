using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexColor;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Damage Type/Heal")]
    public class HealDmg : DamageType
    {
        private Color32 color = new Color32(83, 255, 98, 255);

        public override int CalDmg(int amount, int armor, int magicResist)
        {
            return amount;
        }

        public override Color32 GetColor()
        {
            return color;
        }

        public override string HexColor()
        {
            return HexColorString.Heal;
        }
    }
}


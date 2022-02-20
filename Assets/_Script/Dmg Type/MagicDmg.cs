using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexColor;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Damage Type/Magic")]
    public class MagicDmg : DamageType
    {   
        private Color32 color = new Color32(98, 239, 255, 255);

        public override int CalDmg(int amount, int armor, int magicResist)
        {
            float pct = 100f;

            float postMitigationDmg = amount * (pct / (pct + magicResist));

            return (int)postMitigationDmg;
        }

        public override Color32 GetColor()
        {
            return color;
        }

        public override string HexColor()
        {
            return HexColorString.MagicDmg;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexColor;


namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Damage Type/Physical")]
    public class PhysicalDmg : DamageType
    {   
        private Color32 _color = new Color32(255, 111, 52, 255);

        public override int CalDmg(int amount, int armor, int magicResist)
        {
            float pct = 100f;

            float postMitigationDmg = amount * (pct / (pct + armor));

            return (int)postMitigationDmg;
        }

        public override Color32 GetColor()
        {
            return _color;
        }

        public override string HexColor()
        {
            return HexColorString.PhysicalDmg;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class DamageType : ScriptableObject
    {
        public abstract Color32 GetColor();

        public abstract string HexColor();

        public abstract int CalDmg(int amount, int armor, int magicResist);
    }
}


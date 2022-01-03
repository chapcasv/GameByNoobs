using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace PH
{
    public class UnitAtkLife 
    {
        private int _dmgLife;

        public UnitAtkLife(int dmgLife)
        {
            _dmgLife = dmgLife;
        }

        public int GetDamageLife()
        {
            return _dmgLife;
        }
    }
}


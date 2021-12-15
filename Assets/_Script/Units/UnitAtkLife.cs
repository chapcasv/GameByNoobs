using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace PH
{
    public class UnitAtkLife 
    {
        private int _dmgLife;
        private UnitTeam _myTeam;

        public UnitAtkLife(int dmgLife, UnitTeam team)
        {
            _dmgLife = dmgLife;
            _myTeam = team;
        }

        public void AttackLife(LifeSystem life)
        {
            if (_myTeam == UnitTeam.Player)
            {
                life.DecreaseEnemyLife(_dmgLife);
            }
            else life.DescreasePlayerLife(_dmgLife);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class WolfMonster : BaseUnit
    {
        
        void Update()
        {
            if (!inTeamFight) return;

            if (!HasEnemy)
            {
                currentTarget = Find.CurrentTarget();
            }

            if (Atk.IsInRange(currentTarget) && Move.IsMoving)
            {
                if(Atk.CanAtk)
                {
                    Attack();
                }
            }
            else
            {
                GetInRange();
            }
        }
    }
}


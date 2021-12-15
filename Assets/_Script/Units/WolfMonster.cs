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
                currentTarget = FindTarget.GetCurrentTarget();
            }

            if (Attack.IsInRange(currentTarget) && Move.IsMoving)
            {
                if(Attack.CanAtk)
                {
                    AttackTarget();
                }
            }
            else
            {
                GetInRange();
            }
        }
    }
}


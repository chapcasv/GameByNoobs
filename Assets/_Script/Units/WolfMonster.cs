using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class WolfMonster : BaseUnit
    {
        
        void Update()
        {
            if (!InTeamFight) return;

            if (!HasEnemy)
            {
                FindTarget();
            }

            if (IsInRange && !moving)
            {
                Debug.Log("Atk");
                return;
                //In range for attack!
                if(CanAttack)
                {
                    Attack();
                    currentTarget.TakeDamage(10);
                }
            }
            else
            {
                GetInRange();
            }
        }
    }
}


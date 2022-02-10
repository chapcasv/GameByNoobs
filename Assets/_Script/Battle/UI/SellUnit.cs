using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class SellUnit : MonoBehaviour
    {
        [SerializeField] MemberSystem memberSystem;

        public void Sell(BaseUnit unit)
        {   
            DictionaryTeamBattle.SellUnit(unit.GetTeam(), unit);
            PlayerCacheUnitData.Remove(unit);
            PlayerCacheUnitData.RemoveCacheUnitData(unit);

            unit.CurrentNode.SetOccupied(false);
            Destroy(unit.gameObject);

            memberSystem.DecreaseMember();
        }


    }
}


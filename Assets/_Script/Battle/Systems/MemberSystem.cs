using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SO;


namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Member System")]
    public class MemberSystem : ScriptableObject
    {
        public event Action OnMemberAmountChange;
        [SerializeField] IntReference memberAmount;
        public int GetMemberAmount => memberAmount.Value;

        public void SetData()
        {
            memberAmount.Value = 0;
        }

        public void IncreaseMember()
        {
            memberAmount.Value++;
            OnMemberAmountChange?.Invoke();
        }

        public void DecreaseMember()
        {
            memberAmount.Value--;
            OnMemberAmountChange?.Invoke();
        }
 

    }
}


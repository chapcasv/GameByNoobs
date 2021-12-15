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
        [NonSerialized] bool _isInit = false;
        public int GetMemberAmount => memberAmount.Value;

        public void Init()
        {
            if (_isInit) return;

            memberAmount.Value = 0;

            _isInit = true;
        }

        public void IncreaseMember()
        {
            memberAmount.Value++;
            OnMemberAmountChange?.Invoke();
        }

        public void SubTractMember()
        {
            memberAmount.Value--;
            OnMemberAmountChange?.Invoke();
        }
 

    }
}


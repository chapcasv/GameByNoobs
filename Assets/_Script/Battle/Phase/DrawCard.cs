using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using System;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Phase/Draw Card")]
    public class DrawCard : Phase
    {

        [SerializeField] LocalPlayer _player;

        private bool _complete = false;

        public override bool IsComplete() => _complete;
        

        public override void OnStartPhase()
        {
            _player.DrawCard();

            //Wait for animation draw Card
            _complete = true;
        }

        public override void OnEndPhase()
        {

        }

        
    }
}


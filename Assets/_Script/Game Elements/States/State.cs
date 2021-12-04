using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH.States
{   
    [CreateAssetMenu(menuName = "ScriptableObject/State")]
    public class State : ScriptableObject
    {
        public List<Action> actions;

        public void Tick(float d)
        {
            foreach (var a in actions)
            {
                a.Execute(d);
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Card/ALLCard")]
    public class AllCard : ScriptableObject
    {
        public List<Card> allCard;
    }
}


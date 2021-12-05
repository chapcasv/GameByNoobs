using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Card/ALLCard")]
    public class ALLCard : ScriptableObject
    {
        public List<Card> allCard;
    }
}


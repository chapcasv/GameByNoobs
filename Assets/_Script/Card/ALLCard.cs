using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName ="Card/ALLCard")]
    public class ALLCard : ScriptableObject
    {
        public List<Card> allCard;
    }
}


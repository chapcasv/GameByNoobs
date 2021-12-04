using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Variable/String")]
    public class StringVariable : ScriptableObject
    {   
        [TextArea]
        public string value;
    }
}


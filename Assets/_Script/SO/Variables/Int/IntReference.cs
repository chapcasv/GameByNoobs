using UnityEngine;
using System;
using UnityEngine.Events;

namespace SO
{
    [Serializable]
    public class IntReference
    {
        [SerializeField] bool UseConstant = false;
        [SerializeField] int ConstantValue;
        [SerializeField] IntVariable Variable;

        public int Value
        {
            get { return UseConstant ? ConstantValue : Variable.value; }
            set
            {
                if (!UseConstant)
                {
                    Variable.Set(value);
                }
                else
                {
                    ConstantValue = value;
                }
            }
        }



    }
}


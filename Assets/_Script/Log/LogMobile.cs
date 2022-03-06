using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PH
{
    public class LogMobile : MonoSingleton<LogMobile>
    {
        [SerializeField] TextMeshProUGUI log;

        public void SetText(string value)
        {
            if (log != null)
            {
                log.text = value;

            }
        }
    }
}


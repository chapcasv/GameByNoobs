using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class UIScreen : MonoBehaviour
    {
        public virtual void Initialize()
        {
        }
        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }
        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }

}

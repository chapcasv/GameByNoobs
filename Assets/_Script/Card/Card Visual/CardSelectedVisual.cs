using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH
{
    public class CardSelectedVisual : CardVisual
    {
        protected override void LoadCard(Card c)
        {
            if (_card == null) return;

            for (int i = 0; i < c.baseProperties.Length; i++)
            {
                BaseProperties bp = c.baseProperties[i];
                PropertiesUI bpUI = GetBaseProperty(bp.element);

                if (bpUI == null) continue;
                if (bp.element is ElementImage) bpUI.img.sprite = bp.sprite;
            }
        }

    }
}


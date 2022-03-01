using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PH
{
    public class CardVisual : MonoBehaviour
    {
        [SerializeField] protected PropertiesUI[] basePropertiesViz;
        [SerializeField] protected Image factionIcon;

        protected Card _card;
        public Card GetCard => _card;

        public virtual void SetCard(Card card)
        {
            _card = card;
            LoadCard(_card);
        }

        protected virtual void LoadCard(Card c)
        {
            if (c == null) return;

            c.OnSetFactionViz(this);

            for (int i = 0; i < c.baseProperties.Length; i++)
            {
                BaseProperties bp = c.baseProperties[i];
                PropertiesUI bpUI = GetBaseProperty(bp.element);

                if (bpUI == null)
                    continue;

                if(bp.element is ElementImage)
                {
                    bpUI.img.sprite = bp.sprite;

                }else if(bp.element is ElementInt)
                {
                    bpUI.text.text = bp.intValue.ToString();
                }
                else if(bp.element is ElementText)
                {
                    bpUI.text.text = bp.stringValue;
                }
            }
        }

        public virtual void LoadFaction(Faction[] factions)
        {
            if (factions.Length == 0) return;

            factionIcon.sprite = factions[0].Icon;
        }

        protected PropertiesUI GetBaseProperty(Element e)
        {
            PropertiesUI result = null;

            for (int i = 0; i < basePropertiesViz.Length; i++)
            {
                if(basePropertiesViz[i].element == e)
                {
                    result = basePropertiesViz[i];
                    break;
                }
            }
            return result;
        }

    }
}


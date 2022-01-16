using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PH
{
    public class CardVisual : MonoBehaviour
    {
        [SerializeField] GameObject factionHolder;
        [SerializeField] PropertiesUI[] basePropertiesViz;
        [SerializeField] GameObject[] factionSlotViz;

        protected Card _card;

        public void SetCard(Card card)
        {
            _card = card;
            LoadCard(_card);
        }

        protected virtual void LoadCard(Card c)
        {
            if (_card == null) return;

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

        public void LoadFaction(Faction[] factions)
        {
            factionHolder.SetActive(true);

            if (factions.Length <= factionSlotViz.Length)
            {
                HidenFactionSlot(); //Reload
                for (int i = 0; i < factions.Length; i++)
                {
                    if (factions[i].icon == null) continue;

                    var icon = factionSlotViz[i].transform.GetChild(2);
                    icon.GetComponent<Image>().sprite = factions[i].icon;
                    factionSlotViz[i].SetActive(true);
                }
            } 
        }

        private void HidenFactionSlot()
        {
            for (int i = 0; i < factionSlotViz.Length; i++)
            {
                factionSlotViz[i].SetActive(false);
            }
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

        public void HidenFactionHolder() => factionHolder.gameObject.SetActive(false);

    }
}


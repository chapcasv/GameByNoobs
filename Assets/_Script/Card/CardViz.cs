using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PH
{
    public class CardViz : MonoBehaviour
    {
        [SerializeField] GameObject synergyHolder;

        [SerializeField] Card card;

        [SerializeField] PropertiesUI[] basePropertiesViz;
        [SerializeField] GameObject[] synergySlotViz;

        private void Start()
        {
            LoadCard(card);
            
        }

        private void LoadCard(Card c)
        {
            if (card == null) return;
            card = c;

            c.OnSetSynergyViz(this);

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

        public void LoadSynergy(Synergy[] synergies)
        {
            synergyHolder.SetActive(true);

            if (synergies.Length <= synergySlotViz.Length)
            {
                HidenSynergiesSlot(); //Reload
                for (int i = 0; i < synergies.Length; i++)
                {
                    if (synergies[i].icon == null) continue;
                    synergySlotViz[i].GetComponent<Image>().sprite = synergies[i].icon;
                    synergySlotViz[i].SetActive(true);
                }
            } 
        }

        private void HidenSynergiesSlot()
        {
            for (int i = 0; i < synergySlotViz.Length; i++)
            {
                synergySlotViz[i].SetActive(false);
            }
        }

        public PropertiesUI GetBaseProperty(Element e)
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

        public void HidenSynergyHolder() => synergyHolder.gameObject.SetActive(false);

    }
}


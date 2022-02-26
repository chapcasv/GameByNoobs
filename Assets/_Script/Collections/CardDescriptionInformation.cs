using UnityEngine;

namespace PH
{
    public class CardDescriptionInformation : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        private RectTransform nonUnitSize;
        private UnityEngine.UI.ScrollRect scrollrect;
        private float maxSize = 343f; //size on display
        private float minSize = 200f;
        private float currentSize;
        private void Start()
        {
            nonUnitSize = GetComponent<RectTransform>();
            scrollrect = this.GetComponent<UnityEngine.UI.ScrollRect>();
            currentSize = minSize;
        }
        private void Update()
        {
            
            if(content.rect.height >= minSize)
            {
                currentSize = content.rect.height;
            }
            if (currentSize >= maxSize)
            {
                currentSize = maxSize;
                //scrollrect.enabled = true;
            }
            //else
            //{
            //    scrollrect.enabled = false;
            //}
            nonUnitSize.sizeDelta = new Vector2(nonUnitSize.rect.width, currentSize);
        }

    }

}

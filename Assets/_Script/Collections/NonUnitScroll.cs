using UnityEngine;

namespace PH
{
    public class NonUnitScroll : MonoBehaviour
    {
        [SerializeField] private RectTransform content;
        private RectTransform nonUnitSize;
        private UnityEngine.UI.ScrollRect scrollrect;
        private float maxSize = 452f; //size on display
        private float currentSize;
        private void Start()
        {
            nonUnitSize = GetComponent<RectTransform>();
            scrollrect = this.GetComponent<UnityEngine.UI.ScrollRect>();
            currentSize = content.rect.height;
        }
        private void Update()
        {
            currentSize = content.rect.height;
            if (currentSize >= maxSize)
            {
                currentSize = maxSize;
                scrollrect.enabled = true;
            }
            else
            {
                scrollrect.enabled = false;
            }
            nonUnitSize.sizeDelta = new Vector2(nonUnitSize.rect.width, currentSize);
        }

    }

}

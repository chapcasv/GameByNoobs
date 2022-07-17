using UnityEngine;
using UnityEngine.UI;


namespace PH
{
    public class Border : HealthBase
    {
        public Color Color
        {
            get => Image.color;

            set => Image.color = value;
        }
        protected Image Image => _image == null ? GetComponent<Image>() : _image;
        private Image _image;
        private void Awake()
        {
            _image = Image;
        }
    }

}

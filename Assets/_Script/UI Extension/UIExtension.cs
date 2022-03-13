using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{   
    public class UIExtension 
    {
       public static readonly Vector2 defaultScale = new Vector2(1.3f, 1.3f);

        public static void ScaleOnClick(RectTransform btnTransf, Vector2 scale, float speed = 0.15f)
        {
            btnTransf.DOScale(scale, speed).OnComplete( () => ScaleOne(btnTransf,speed));
        }

        private static void ScaleOne(RectTransform btnTransf,float speed)
        {
            btnTransf.DOScale(Vector2.one, speed);
        }

    }
}


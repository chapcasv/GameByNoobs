using UnityEngine;
using UnityEngine.UI;

namespace PH.Loader
{
    public class Loading : MonoBehaviour
    {
        [SerializeField] Image progressBar;
        private bool isFirstUpdate = true;  

        // Update is called once per frame
        void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = !isFirstUpdate;
                LoadSystem.LoaderCallback();
            }

            progressBar.fillAmount = Mathf.RoundToInt(LoadSystem.GetLoadingProgress());
        }
    }
}


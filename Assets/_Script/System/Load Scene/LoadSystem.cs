using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace PH.Loader
{
    public static class LoadSystem 
    {   
        private class LoadMono : MonoBehaviour { }

        private static event Action OnLoaderCallback;
        private static AsyncOperation loadingAO;

        public static void Load(SceneSelect scene)
        {
            OnLoaderCallback = () =>
            {
                GameObject obj = new GameObject();
                obj.AddComponent<LoadMono>().StartCoroutine(LoadSync(scene));
            };

            SceneManager.LoadScene(SceneSelect.Loading.ToString());
        }

        private static IEnumerator LoadSync(SceneSelect scene)
        {
            loadingAO = SceneManager.LoadSceneAsync(scene.ToString());

            while (!loadingAO.isDone)
            {
                yield return null;
            }
        }

        public static float GetLoadingProgress()
        {
            if(loadingAO!= null)
            {

                Debug.Log(loadingAO.progress * 100f);
                return loadingAO.progress;
            }
            else
            {
                return 1f;
            }
        }

        public static void LoaderCallback()
        {
            if (OnLoaderCallback != null)
            {
                OnLoaderCallback();
                OnLoaderCallback = null;
            }
        }
    }
}



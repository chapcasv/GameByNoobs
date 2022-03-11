using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using EasyUI.Progress;

namespace PH.Loader
{
    public static class LoadSystem
    {
        private class LoadMono : MonoBehaviour { }
        private static AsyncOperation loadingAO;

        public static void Load(SceneSelect scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }

        /// <summary>
        /// Use Progress UI on load
        /// </summary>

        public static void LoadAsync(SceneSelect scene)
        {
            GameObject obj = new GameObject();
            obj.AddComponent<LoadMono>().StartCoroutine(StartLoadAsync(scene));
        }

        private static IEnumerator StartLoadAsync(SceneSelect scene)
        {
            loadingAO = SceneManager.LoadSceneAsync(scene.ToString());

            while (!loadingAO.isDone)
            {
                yield return null;
            }
        }


       
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class Billboard : MonoBehaviour
    {
        private Camera cam;
        void Start()
        {
            cam = Camera.main;
        }

        private void FixedUpdate()
        {
            transform.LookAt(transform.position + cam.transform.forward);
        }
    }
}


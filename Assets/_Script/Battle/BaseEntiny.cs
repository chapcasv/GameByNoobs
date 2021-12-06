using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.Graph;

namespace PH
{
    public class BaseEntiny : MonoBehaviour
    {

        private Node curentNode;
        public Node CurentNode { get => curentNode; set => curentNode = value; }
    }
}


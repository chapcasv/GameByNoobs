using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    public class BaseUnit : MonoBehaviour
    {

        private Node currentNode;
        public Node CurrentNode { get => currentNode; set => currentNode = value; }

        private void OnEnable()
        {
            Debug.Log("Enable");
        }

        public void Setup(Node spawnNode)
        {
           
            CurrentNode = spawnNode;
            transform.position = spawnNode.WorldPosition;
            spawnNode.SetOccupied(true);
        }
    }
}


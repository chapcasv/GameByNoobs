using UnityEngine;

namespace PH.Graph
{
    public class Node
    {
        private int index;
        private Vector3 worldPosition;

        private bool occupied = false;
        public bool IsOccupided => occupied;

        public Vector3 WorldPosition { get => worldPosition; set => worldPosition = value; }
        public int Index { get => index; set => index = value; }

        public Node(int index, Vector3 worldPosition)
        {
            Index = index;
            WorldPosition = worldPosition;
            occupied = false;
        }
        public void SetOccupied(bool val)
        {
            occupied = val;
        }
    }
}



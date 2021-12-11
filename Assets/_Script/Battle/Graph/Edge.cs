using UnityEngine;

namespace PH.GraphSystem
{
    public class Edge
    {
        public Node from;
        public Node to;

        private float weight;

        public Edge(Node from, Node to, float weight)
        {
            this.from = from;
            this.to = to;
            this.weight = weight;
        }

        public float GetWeight()
        {
            if (to.IsOccupied)
            {
                return Mathf.Infinity;
            }
            return weight;
        }
    }
}



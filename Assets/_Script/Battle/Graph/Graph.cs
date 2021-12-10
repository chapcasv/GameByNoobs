using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PH.GraphSystem
{
    public class Graph
    {
        public List<Node> nodes;
        public List<Edge> edges;

        public Graph()
        {
            nodes = new List<Node>();
            edges = new List<Edge>();
        }

        public void AddNode(Vector3 worldPosition)
        {
            Vector3 setWorldPos = new Vector3(worldPosition.x, worldPosition.y + 0.2f, worldPosition.z);
            nodes.Add(new Node(nodes.Count, setWorldPos));
        }

        public void AddEdge(Node from, Node to, float weight = 1f)
        {
            edges.Add(new Edge(from, to, weight));
        }


        public bool Adjacent(Node from, Node to)
        {
            foreach (Edge e in edges)
            {
                if (e.from == from && e.to == to)
                    return true;
            }
            return false;
        }

 
        public List<Node> Neighbors(Node from)
        {
            List<Node> result = new List<Node>();
            foreach (Edge e in edges)
            {
                if (e.from == from)
                    result.Add(e.to);
            }
            return result;
        }

        public float Distance(Node from, Node to)
        {
            foreach (Edge e in edges)
            {
                if (e.from == from && e.to == to)
                    return e.GetWeight();
            }
            return Mathf.Infinity;
        }

 
        public virtual List<Node> GetShortestPath(Node start, Node end)
        {
            List<Node> path = new List<Node>();
            if (start == end)
            {
                path.Add(start);
                return path;
            }
            List<Node> openlist = new List<Node>();
            Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
            Dictionary<Node, float> distances = new Dictionary<Node, float>();

            for (int i = 0; i < nodes.Count; i++)
            {
                openlist.Add(nodes[i]);
                distances.Add(nodes[i], float.PositiveInfinity);  //default distance is infinity
            }

            distances[start] = 0f; //distance from the same node is zero

            while (openlist.Count > 0)
            {

                //Get the node with smaller distance
                openlist = openlist.OrderBy(x => distances[x]).ToList();
                Node current = openlist[0];
                openlist.Remove(current);

                if (current == end)
                {

                    //done!
                    while (previous.ContainsKey(current))
                    {
                        path.Insert(0, current);
                        current = previous[current];
                    }
                    path.Insert(0, current);
                    break;
                }
                foreach (Node neighbor in Neighbors(current))
                {
                    float distance = Distance(current, neighbor);

                    float candidateNewDistance = distances[current] + distance;

                    if (candidateNewDistance < distances[neighbor])
                    {
                        distances[neighbor] = candidateNewDistance;
                        previous[neighbor] = current;
                    }
                }

            }
            return path;
        }
    }
}



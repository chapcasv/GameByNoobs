using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH.GraphSystem
{
    public static class GridManager 
    {
        private static Graph graph = new Graph();
        public static List<Node> NodePlayerTeam { get; private set; }
 
        private static readonly float _horizontalEdge = 6f;
        private static readonly float _diagonalEdge = 9f;
        private static Dictionary<Team, int> _startPositionPerTeam;

        public static Node ConvertPositiontoNode(int Position_in_Data)
        {
            foreach (Node node in graph.nodes)
            {
                if (node.Index == Position_in_Data)
                    return node;
            }
            return null;
        }


        public static Node GetFreeNode(Team forteam)
        {
            int startIndex = _startPositionPerTeam[forteam];
            int currentIndex = startIndex;

            while (graph.nodes[currentIndex].IsOccupided)
            {
                if (startIndex == 0)
                {
                    currentIndex++;
                    if (currentIndex == graph.nodes.Count)
                        return null;
                }
                else
                {
                    currentIndex--;
                    if (currentIndex == -1)
                        return null;
                }
            }
            return graph.nodes[currentIndex];
        }

        public static Node GetNodeForTile(Tile t)
        {
            var allNodes = graph.nodes;

            for (int i = 0; i < allNodes.Count; i++)
            {
                if (t.transform.GetSiblingIndex() == allNodes[i].Index)
                {
                    return allNodes[i];
                }
            }

            return null;
        }

        public static List<Node> GetNodesCloseTo(Node to)
        {
            return graph.Neighbors(to);
        }

        public static List<Node> GetPath(Node from, Node to)
        {
            return graph.GetShortestPath(from, to);
        }


        public static void InitializeGraph(Transform tilesHolder )
        {
            if (graph.nodes.Count > 0) return;

            foreach (Transform tile in tilesHolder)
            {
                graph.AddNode(tile.position);
            }

            var allNodes = graph.nodes;
            foreach (Node from in allNodes)
            {
                foreach (Node to in allNodes)
                {
                    float distance = Vector3.Distance(from.WorldPosition, to.WorldPosition);
                    if (distance == _horizontalEdge && from != to)
                    {
                        graph.AddEdge(from, to, _horizontalEdge);
                    }
                    else if (_horizontalEdge < distance && distance <= _diagonalEdge && from != to)
                    {
                        graph.AddEdge(from, to, _diagonalEdge);
                    }
                }
            }
            SetNodePlayerTeam();

        }

        private static void SetNodePlayerTeam()
        {
            NodePlayerTeam = new List<Node>();
            foreach (Node node in graph.nodes)
            {   
                //Team PlayerLocal will start from botleft
                if (NodePlayerTeam.Count < 32)
                {
                    NodePlayerTeam.Add(node);
                }
            }
        }

        public static int fromIndex = 0;
        public static int toIndex = 0;


        private static void OnDrawGizmos()
        {


            var allNodes = graph.nodes;
            foreach (Node n in allNodes)
            {
                Gizmos.color = n.IsOccupided ? Color.red : Color.green;
                Gizmos.DrawSphere(n.WorldPosition, 0.1f);
            }

            if (fromIndex < allNodes.Count && toIndex < allNodes.Count)
            {
                List<Node> path = graph.GetShortestPath(allNodes[fromIndex], allNodes[toIndex]);
                if (path.Count > 1)
                {
                    for (int i = 1; i < path.Count; i++)
                    {
                        Debug.DrawLine(path[i - 1].WorldPosition, path[i].WorldPosition, Color.red, 1f);
                    }
                }
            }
        }
    }
}


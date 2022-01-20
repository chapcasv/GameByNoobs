using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH.GraphSystem
{
    public static class GridBoard 
    {   
        
        private static Graph graph = new Graph();
        public static List<Node> NodePlayerTeam { get; private set; }
        public static List<Node> NodeEnemyTeam { get; private set; }
 
        private static readonly float _horizontalEdge = 6f;
        private static readonly float _diagonalEdge = 9f;
        private static bool isInit = false;

        public static Node IntPositiontoNode(int Position_in_Data)
        {
            foreach (Node node in graph.nodes)
            {
                if (node.Index == Position_in_Data)
                    return node;
            }
            return null;
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

        public static List<Node> GetNodesCloseTo(Node to) => graph.Neighbors(to);
 
        public static List<Node> GetPath(Node from, Node to) => graph.GetShortestPath(from,to);

        public static void InitializeGraph(Transform tilesHolder )
        {
            if (isInit) return;

            AddNode(tilesHolder);     
            AddEdge();          
            SetNodePerTeam();

            isInit = true;
        }

        private static void AddEdge()
        {
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
        }

        private static void AddNode(Transform tilesHolder)
        {
            foreach (Transform tile in tilesHolder)
            {
                graph.AddNode(tile.position);
            }
        }

        private static void SetNodePerTeam()
        {
            NodePlayerTeam = new List<Node>();
            NodeEnemyTeam = new List<Node>();

            foreach (Node node in graph.nodes)
            {   
                //PlayerZone will start from botleft
                if (NodePlayerTeam.Count < 32)
                {
                    NodePlayerTeam.Add(node);
                }
                else
                {
                    NodeEnemyTeam.Add(node);
                }
            }
        }

        public static void Reset()
        {
            graph.nodes.Clear();
            graph.edges.Clear();
            isInit = false;
        }
    }
}


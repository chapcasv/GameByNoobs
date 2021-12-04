using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH.Graph
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] Transform tilesHolder;
        public Graph graph;
        public List<Node> nodeTeam1;
        public static GridManager instance;

        private float horizontal_edge = 6f;
        private float diagonal_edge = 8.5f;
        private Dictionary<Team, int> startPositionPerTeam;
        public float GetDiagonalEdge { get => diagonal_edge; }
        public float GetHorizontalEdge { get => horizontal_edge; }

        protected void Awake()
        {

            instance = this;
            InitializeGraph();
            startPositionPerTeam = new Dictionary<Team, int>();
            startPositionPerTeam.Add(Team.Team1, 31);
            startPositionPerTeam.Add(Team.Team2, graph.nodes.Count - 1);
            SetNodeTeam1();
        }


        public Node Convert_Position_toNode(int Position_in_Data)
        {
            foreach (Node node in graph.nodes)
            {
                if (node.Index == Position_in_Data)
                    return node;
            }
            return null;
        }


        public Node GetFreeNode(Team forteam)
        {
            int startIndex = startPositionPerTeam[forteam];
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

        public Node GetNodeForTile(Tile t)
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

        public List<Node> GetNodesCloseTo(Node to)
        {
            return graph.Neighbors(to);
        }

        public List<Node> GetPath(Node from, Node to)
        {
            return graph.GetShortestPath(from, to);
        }


        private void InitializeGraph()
        {
            graph = new Graph();

            foreach (Transform tile in tilesHolder)
            {
                graph.AddNode(tile.position);
            }

            var allNodes = graph.nodes;
            foreach (Node from in allNodes)
            {
                foreach (Node to in allNodes)
                {
                    if (Vector3.Distance(from.WorldPosition, to.WorldPosition) <= GetHorizontalEdge && from != to)
                    {
                        graph.AddEdge(from, to, GetHorizontalEdge);
                    }
                    else if (GetHorizontalEdge < Vector3.Distance(from.WorldPosition, to.WorldPosition) &&
                                Vector3.Distance(from.WorldPosition, to.WorldPosition) <= GetDiagonalEdge && from != to)
                    {
                        graph.AddEdge(from, to, GetDiagonalEdge);
                    }
                }
            }
        }

        private void SetNodeTeam1()
        {
            nodeTeam1 = new List<Node>();
            foreach (Node node in graph.nodes)
            {
                if (nodeTeam1.Count < 32)
                {
                    nodeTeam1.Add(node);
                }
            }
        }

        public int fromIndex = 0;
        public int toIndex = 0;


        private void OnDrawGizmos()
        {

            if (graph == null)
                return;

            var allEdges = graph.edges;

            if (allEdges == null)
                return;
            foreach (Edge e in allEdges)
            {
                if (e.GetWeight() <= horizontal_edge)
                {
                    Debug.DrawLine(e.from.WorldPosition, e.to.WorldPosition, Color.black, 100f);
                }
                else if (horizontal_edge < e.GetWeight() && e.GetWeight() <= diagonal_edge)
                {
                    Debug.DrawLine(e.from.WorldPosition, e.to.WorldPosition, Color.cyan, 100f);
                }

            }

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


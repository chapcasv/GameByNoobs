using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.Graph;

namespace PH
{
    public class BaseEntiny : MonoBehaviour
    {
        protected Team unitTeam;
        private Node curentNode;
        public Team UnitTeam { get => unitTeam; private set => unitTeam = value; }
        public Node CurentNode { get => curentNode; set => curentNode = value; }
    }
}


using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    public abstract class UnitFindTarget 
    {
        protected UnitTeam myTeam;
        protected Transform myTransform;
        
        public UnitFindTarget(UnitTeam team, Transform transform)
        {
            myTeam = team;
            myTransform = transform;
        }

        public abstract BaseUnit GetCurrentTarget();

        public abstract void GetInRange(BaseUnit currentTarget, Node currentNode, float moveSpeed,ref bool moving);
    }
}
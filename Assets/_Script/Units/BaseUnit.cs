using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using System.Linq;
using SO;

namespace PH
{
    public class BaseUnit : MonoBehaviour
    {
        protected UnitFinding Find;
        protected UnitMove Move;
        protected UnitAttack Atk;
        
        protected IUnitHealth Health;
        protected IUnitSkillPoint SkillPoint;
        
        protected Node currentNode;
        protected UnitTeam _myTeam;
        protected bool inTeamFight = false;
        protected BaseUnit currentTarget;
        protected Node destination;
        protected bool HasEnemy => currentTarget != null;      
        public Node CurrentNode { get => currentNode; set => currentNode = value; }
        public bool IsLive { get => Health.IsLive;}
        public bool InTeamFight {set => inTeamFight = value; }

        public virtual void Setup(Node spawnNode, CardUnit unit, UnitTeam team)
        {
            _myTeam = team;
            CurrentNode = spawnNode;
            transform.position = spawnNode.WorldPosition;
            spawnNode.SetOccupied(true);

            SetUpHealth(unit.Hp, team);
            SetUpSkillPoint(unit.SpMax, unit.SpStart, unit.SpRegen);
            SetUpAttack(unit);
            SetUpMove(unit);
            SetUpFindTarget(team);
        }

        private void SetUpFindTarget(UnitTeam team) => Find = new NormalFinding(team, transform);

        private void SetUpMove(CardUnit unit) => Move = new NormalUnitMove(unit.MoveSpeed, transform);

        private void SetUpAttack(CardUnit unit)
        {
            Atk = gameObject.AddComponent(typeof(NormalUnitAtk)) as NormalUnitAtk;
            Atk.Constructor(unit.AtkSpeed, unit.Range, unit.Str);
        }

        protected virtual void SetUpHealth(int maxHP,UnitTeam myTeam) 
        {
            Health = GetComponent<IUnitHealth>();
            Health.SetHP(maxHP, myTeam);
            Health.OnDie += Die;
        }

        protected virtual void SetUpSkillPoint(int maxSP, int startSP, int spRegen)
        {
            SkillPoint = GetComponent<IUnitSkillPoint>();
            SkillPoint.SetSP(maxSP, startSP, spRegen);
        }

        protected void GetInRange()
        {
            if (currentTarget == null)
                return;

            if (!Move.IsMoving)
            {    
                destination = Find.Destination(currentNode);
            }

            Move.IsMoving = !Move.MoveTowards(destination);

            if (!Move.IsMoving)
            {
                //Free previous node
                CurrentNode.SetOccupied(false);
                CurrentNode = destination;
            }
        }

        public virtual void Attack()
        {
            Atk.Attack(currentTarget);
            SkillPoint.IncreaseSP();
        }

        public virtual void TakeDamage(int amount)
        {
            Health.TakeDamage(amount);
            SkillPoint.IncreaseSP();
        }

        protected virtual void Die()
        {
            DictionaryTeamBattle.RemoveUnit(_myTeam, this);
            currentNode.SetOccupied(false);
            Destroy(gameObject);
        }
 
    }
}


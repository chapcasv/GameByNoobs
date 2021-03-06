using PH.GraphSystem;
using System.Collections;
using UnityEngine;

namespace PH
{
    [RequireComponent(typeof(UnitTransform))]
    public abstract class BaseUnit : MonoBehaviour
    {
        #region Properties
        protected UnitFinding Find;
        protected UnitMove Move;
        protected UnitTransform unitTransform;

        protected UnitAtkSystem Atk;

        protected UnitAtkLife Life;
        protected UnitEquipment Equipment;

        protected UnitSurvivalStat SurvivalStat;
        protected Animator anim;
        protected Rigidbody rb;
        protected ManaSystem Mana;

        protected TriggerOnBoard[] triggerOnBoards;
        protected Faction[] factions;

        protected UnitStatusEffect unitStatusEffect;

        protected Node currentNode;
        protected UnitTeam _myTeam;
        protected bool inTeamFight = false;
        protected BaseUnit currentTarget;
        protected Node destination;
        protected int baseID;

        protected bool HasEnemy => currentTarget != null && currentTarget.IsLive;
        public Node CurrentNode { get => currentNode; set => currentNode = value; }
        public bool IsLive { get => SurvivalStat.IsLive; }
        public bool InTeamFight { set => inTeamFight = value; }
        public UnitTeam GetTeam() => _myTeam;
        public int GetDmgLife => Life.GetDamageLife();
        public ManaSystem GetManaSystem => Mana;
        public UnitSurvivalStat GetUnitSurvivalStat => SurvivalStat;
        public UnitAtkSystem GetAtkSystem => Atk;
        public UnitMove GetMove => Move;
        public UnitStatusEffect GetUnitStatusEffect => unitStatusEffect;
        public UnitTransform GetUnitTransform => unitTransform;
        public BoxCollider Col { get; set; }
        public Faction[] GetFactions => factions;
        public int GetID => baseID;
        public UnitEquipment UnitEquipment { get => Equipment; }
        #endregion

        protected virtual void Update()
        {
            if (!inTeamFight || !IsLive) return;

            unitStatusEffect.Execute();

            if (!HasEnemy) currentTarget = Find.CurrentTarget();

            if (!Mana.IsFullMana)
            {
                AttackInRange();
            }
            else
            {
                CastAbilityInRange();
            }
        }

        #region SetUp
        public virtual void Setup(Node spawnNode, CardUnit unit, int cardID, UnitTeam team,
            DragLogic dragLogic, Transform enemyBase)
        {
            _myTeam = team;
            SetUpUnitTransform(spawnNode, enemyBase);
            SetUpEquipment();

            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            Col = GetComponent<BoxCollider>();

            SetUpFinding(team);
            SetUpAtkLife(unit, team);
            SetUpSurvivalStatSystem(unit, team);
            SetUpManaSystem(unit);
            SetUpAttack(unit);
            SetUpMove(unit, rb);
            SetUpFaction(unit);
            SetUpTriggerOnBoard(unit);
            SetUpDrag(dragLogic);
            SetUpStatusEffect();
            AddPlayerCacheUnitData();

            baseID = cardID;
        }

        private void SetUpUnitTransform(Node spawnNode, Transform enemyBase)
        {
            unitTransform = GetComponent<UnitTransform>();
            unitTransform.SetUp(this, spawnNode, enemyBase);
        }

        protected virtual void SetUpEquipment()
        {
            Equipment = GetComponent<UnitEquipment>();
            Equipment.SetUp();
        }
        protected virtual void SetUpFinding(UnitTeam team) => Find = new NormalFinding(team, transform);
        protected virtual void SetUpAtkLife(CardUnit unit, UnitTeam team) => Life = new UnitAtkLife(unit.DmgLife);
        protected virtual void SetUpSurvivalStatSystem(CardUnit unit, UnitTeam myTeam)
        {
            SurvivalStat = GetComponent<UnitSurvivalStat>();
            SurvivalStat.SetUp(unit.Hp, unit.Armor, unit.MagicResist, unit.NegativeBonusDamage, myTeam);
            SurvivalStat.OnDie += Die;
        }
        protected virtual void SetUpManaSystem(CardUnit unit)
        {
            Mana = GetComponent<ManaSystem>();
            Mana.SetMana(unit.ManaMax, unit.ManaStart, unit.ManaRegen);
        }
        protected abstract void SetUpAttack(CardUnit unit);
        protected virtual void SetUpMove(CardUnit unit, Rigidbody rb)
        {
            Move = GetComponent<UnitMove>();
            Move.SetUp(unit.MoveSpeed, transform, anim, rb);
        }
        private void SetUpFaction(CardUnit unit) => factions = unit.GetFaction();
        private void SetUpTriggerOnBoard(CardUnit unit)
        {
            DataTriggerOnBoard[] dt = unit.GetDataTriggerOnBoards;

            triggerOnBoards = new TriggerOnBoard[dt.Length];

            for (int i = 0; i < dt.Length; i++)
            {
                var logic = dt[i].GetLogic;
                var input = dt[i].GetInput;
                var readInput = dt[i].GetReadInput;

                TriggerOnBoard newTrigger = new TriggerOnBoard(input, readInput, logic);
                triggerOnBoards[i] = newTrigger;
            }
        }

        private void SetUpDrag(DragLogic dragLogic)
        {
            GetComponent<DragUnit>().SetLogic = dragLogic;
        }

        private void SetUpStatusEffect()
        {
            unitStatusEffect = GetComponent<UnitStatusEffect>();
            unitStatusEffect.SetUp(this);
        }

        private void AddPlayerCacheUnitData()
        {
            if (_myTeam == UnitTeam.Player) PlayerCacheUnitData.Add(this);
        }
        #endregion

        public void AddListernerTriggerOnBoard()
        {
            for (int i = 0; i < triggerOnBoards.Length; i++)
            {
                triggerOnBoards[i].AddListerner();
            }
        }

        public virtual void RemoveOneRoundAddOn()
        {
            Atk.RemoveOneRoundAddOn();
            Equipment.RemoveOneRoundAddOn();
            ResetTriggerOnBoard();
        }

        //Need move
        protected void ResetTriggerOnBoard()
        {
            for (int i = 0; i < triggerOnBoards.Length; i++)
            {
                triggerOnBoards[i].IsTriggered = false;
            }
        }

        public virtual int TakeDamage(BaseUnit sender, int rawDmg, DamageType dmgType, bool isCrit = false)
        {
            int dmgDeal = SurvivalStat.TakeDmg(rawDmg, dmgType, isCrit);
            Mana.IncreaseManaOnTakeDame();

            //On take dmg
            //Change current target if current target not in range and sender in range
            if (!Atk.IsInRange(currentTarget) && Atk.IsInRange(sender))
            {
                currentTarget = sender;
            }

            return dmgDeal;
        }

        public virtual bool Equip(CardItem item)
        {
            bool canEquip = Equipment.Equip(item);
            return canEquip;
        }

        public void RemoveCurrentTarget() => currentTarget = null;
        public void ResetMove() => Move.IsMoving = false;

        protected virtual void AttackInRange()
        {
            Atk.CurrentTarget = currentTarget;

            if (Atk.IsInRange(currentTarget) && !Move.IsMoving && currentTarget.IsLive)
            {
                if (Atk.CanAtk)
                {
                    Atk.BasicAtk();
                    Mana.IncreaseManaOnHit();
                }
            }
            else GetInRange();
        }

        protected void GetInRange()
        {
            if (currentTarget == null || !currentTarget.IsLive || !Move.CanMove || !Atk.CanAtk)
                return;

            if (!Move.IsMoving)
            {
                destination = Find.Destination(currentNode);

                if(destination == null)
                {
                    Debug.Log(gameObject.name + "Destination is null!!!");
                    return;
                }
            }

            //Unit is moving to next node?
            Move.IsMoving = !Move.MoveTowards(destination, currentTarget);

            //When unit stay in next node, free previous node
            if (!Move.IsMoving)
            {
                CurrentNode.SetOccupied(false);
                CurrentNode = destination;
            }
        }

        protected virtual void CastAbilityInRange()
        {
            if (Atk.IsInRangeAbility(currentTarget) && !Move.IsMoving && currentTarget.IsLive)
            {
                if (Atk.CanCastAbility && Atk.CanAtk)
                {
                    Atk.CastAbility(currentTarget, this);
                    Mana.CastSkill();
                }
            }
            else GetInRange();
        }

        protected virtual void Die()
        {
            currentNode.SetOccupied(false);
            anim.SetBool(AnimEnum.IsDie.ToString(), true);
            StartCoroutine(WaitAnimDie(3)); //deplay animation die
        }

        protected IEnumerator WaitAnimDie(float deplay)
        {
            yield return new WaitForSeconds(deplay);

            DictionaryTeamBattle.RemoveUnit(_myTeam, this);
            gameObject.SetActive(false);
        }

        protected virtual void OnDestroy()
        {
            RemoveTriggerOnBoard();
            SurvivalStat.OnDie -= Die;
        }

        protected void RemoveTriggerOnBoard()
        {
            for (int i = 0; i < triggerOnBoards.Length; i++)
            {
                triggerOnBoards[i].RemoveListerner();
            }
        }
    }
}


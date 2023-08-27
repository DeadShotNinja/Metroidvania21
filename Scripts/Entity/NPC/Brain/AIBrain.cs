using System;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(NPCEntity))]
    public class AIBrain : MonoBehaviour, IInputProvider
    {
        [Header("Set Up")]
        [SerializeField] private Transform _target;
        [SerializeField] private float _stopDistance = 3f;

        // I need to set this target from PlayerEntity in LevelManage
        public Transform Target => _target;
        public float StopDistance => _stopDistance;
        
        public NPCEntity NPC { get; private set; }
        public Vector2 MoveInput { get; set; }
        public InputState JumpInput { get; set; }
        public InputState DashInput { get; set; }
        public StateMachine<BaseBehaviourState> BehaviourStateMachine { get; private set; }

        #region Behaviour States

        public IdleBehaviourState IdleBehaviourState { get; private set; }
        public ChaseBehaviourState ChaseBehaviorState { get; private set; }
        public PatrolBehaviourState PatrolBehaviourState { get; private set; }

        #endregion

        private void Awake()
        {
            NPC = GetComponent<NPCEntity>();
            
            JumpInput = new InputState();

            BehaviourStateMachine = new StateMachine<BaseBehaviourState>();

            IdleBehaviourState = new IdleBehaviourState(this, BehaviourStateMachine);
            ChaseBehaviorState = new ChaseBehaviourState(this, BehaviourStateMachine);
            PatrolBehaviourState = new PatrolBehaviourState(this, BehaviourStateMachine);

            // TODO: Might not need LevelManager to be a singleton.
            //_target = LevelManager.Instance.PlayerEntity.transform;
            BehaviourStateMachine.Initialize(IdleBehaviourState);
        }

        private void Update()
        {
            BehaviourStateMachine.CurrentState.LogicUpdate();
        }
    }
}
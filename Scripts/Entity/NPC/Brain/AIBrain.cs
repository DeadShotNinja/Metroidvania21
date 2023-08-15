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

        public Transform Target => _target;
        public float StopDistance => _stopDistance;
        
        public NPCEntity NPC { get; private set; }
        public Vector2 MoveInput { get; set; }
        public InputState JumpInput { get; set; }
        public StateMachine<BaseBehaviourState> BehaviourStateMachine { get; private set; }

        #region Behaviour States

        public IdleBehaviourState IdleBehaviourState { get; private set; }
        public ChaseBehaviourState ChaseBehaviorState { get; private set; }

        #endregion

        private void Awake()
        {
            NPC = GetComponent<NPCEntity>();
            
            JumpInput = new InputState();

            BehaviourStateMachine = new StateMachine<BaseBehaviourState>();

            IdleBehaviourState = new IdleBehaviourState(this, BehaviourStateMachine);
            ChaseBehaviorState = new ChaseBehaviourState(this, BehaviourStateMachine);
            
            BehaviourStateMachine.Initialize(IdleBehaviourState);
        }

        private void Update()
        {
            BehaviourStateMachine.CurrentState.LogicUpdate();
        }
    }
}
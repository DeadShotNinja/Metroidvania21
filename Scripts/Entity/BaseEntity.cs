using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

namespace Metro
{
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    [RequireComponent(typeof(EntityHorizontalMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseEntity : MonoBehaviour
    {
        [InfoBox("Physics")]
        [SerializeField] private EntityCollision _collision;
        [SerializeField] private EntityGravity _gravity;

        [Header("Debugging")]
        public TMP_Text StateText;
       
        public EntityCollision Collision => _collision;
        public EntityGravity Gravity => _gravity;
        
        public Rigidbody2D EntityRigidbody { get; protected set; }
        public EntityComponent[] EntityComponents { get; protected set; }
        public IInputProvider InputProvider { get; protected set; }

        public StateMachine<BaseMovementState> MovementStateMachine { get; private set; }

        #region Movement States

        // Airborne
        public JumpAirborneState JumpAirborneState { get; private set; }
        public FallAirborneState FallAirborneState { get; private set; }
        public WallSlideAirborneState WallSlideAirborneState { get; private set; }
        // Grounded
        public IdleGroundedState IdleGroundedState { get; private set; }
        public MoveGroundedState MoveGroundedState { get; private set; }
        // Both
        public DashMovementState DashMovementState { get; private set; }

        #endregion

        protected virtual void Awake()
        {
            EntityRigidbody = GetComponent<Rigidbody2D>();
            
            _collision.Initialize(this);
            _gravity.Initialize(this);
            EntityComponents = GetComponents<EntityComponent>();
            InitializeComponents();
            
            MovementStateMachine = new StateMachine<BaseMovementState>();
            
            // Airborne
            JumpAirborneState = new JumpAirborneState(this, MovementStateMachine);
            FallAirborneState = new FallAirborneState(this, MovementStateMachine);
            WallSlideAirborneState = new WallSlideAirborneState(this, MovementStateMachine);
            // Grounded
            IdleGroundedState = new IdleGroundedState(this, MovementStateMachine);
            MoveGroundedState = new MoveGroundedState(this, MovementStateMachine);
            // Both
            DashMovementState = new DashMovementState(this, MovementStateMachine);
            
            MovementStateMachine.Initialize(IdleGroundedState);
        }
        
        protected virtual void Start()
        {
            
        }
        
        protected virtual void Update()
        {
            _collision.RunCollisionChecks();
            
            UpdateComponents();
            MovementStateMachine.CurrentState.LogicUpdate();
        }

        protected void FixedUpdate()
        {
            _gravity.CalculateJumpApex();
            _gravity.ApplyGravity();
            
            MovementStateMachine.CurrentState.PhysicsUpdate();
        }

        private void InitializeComponents()
        {
            foreach (EntityComponent component in EntityComponents)
            {
                component.Initialize(this);
            }
        }
        
        private void UpdateComponents()
        {
            foreach (EntityComponent component in EntityComponents)
            {
                component.LogicUpdate();
            }
        }

        private void OnDrawGizmosSelected()
        {
            _collision.GizmosToDraw(transform.position);
        }

        private void OnDestroy()
        {
            foreach (EntityComponent component in EntityComponents)
            {
                component.CleanUp();
            }
        }
    }
}
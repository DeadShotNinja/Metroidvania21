using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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
       
        public EntityCollision Collision => _collision;
        public EntityGravity Gravity => _gravity;
        
        public Rigidbody2D EntityRigidbody { get; protected set; }
        public EntityComponent[] EntityComponents { get; protected set; }
        public IInputProvider InputProvider { get; protected set; }

        public StateMachine<BaseMovementState> MovementStateMachine { get; private set; }

        #region Movement States

        public IdleMovementState IdleMovementState { get; private set; }
        public MoveMovementState MoveMovementState { get; private set; }

        #endregion

        protected virtual void Awake()
        {
            EntityRigidbody = GetComponent<Rigidbody2D>();
            
            _collision.Initialize(this);
            _gravity.Initialize(this);
            EntityComponents = GetComponents<EntityComponent>();
            InitializeComponents();
            
            MovementStateMachine = new StateMachine<BaseMovementState>();
            
            IdleMovementState = new IdleMovementState(this, MovementStateMachine);
            MoveMovementState = new MoveMovementState(this, MovementStateMachine);
            
            MovementStateMachine.Initialize(IdleMovementState);
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
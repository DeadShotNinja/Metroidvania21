using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace Metro
{
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public abstract class BaseEntity : MonoBehaviour, IDamagable, IAttachable
    {
        [InfoBox("Physics")]
        [SerializeField] protected EntityCollision _collision;
        [SerializeField] private EntityGravity _gravity;

        [BoxGroup("Respawn")]
        [Tooltip("Time to wait before respawning the entity.")]
        [SerializeField] private float _respawnDelay = 1f;
        
        [Header("MMFeedbacks")]
        [SerializeField] private FeedbacksData _feedbacks;
        
        [Header("Debugging")]
        public TMP_Text StateText;
       
        public EntityCollision Collision => _collision;
        public EntityGravity Gravity => _gravity;
        
        public Rigidbody2D EntityRigidbody { get; protected set; }
        public Collider2D EntityCollider { get; protected set; }
        public EntityComponent[] EntityComponents { get; protected set; }
        public IInputProvider InputProvider { get; protected set; }

        public bool IsDead { get; protected set; }
        public bool IsAttached { get; set; }

        public StateMachine<BaseMovementState> MovementStateMachine { get; private set; }

        #region Movement States

        // Airborne
        public JumpAirborneState JumpAirborneState { get; private set; }
        public FallAirborneState FallAirborneState { get; private set; }
        public WallSlideWallingState WallSlideWallingState { get; private set; }
        public WallJumpWallingState WallJumpWallingState { get; private set; }
        // Grounded
        public IdleGroundedState IdleGroundedState { get; private set; }
        public MoveGroundedState MoveGroundedState { get; private set; }
        // Both
        public DashMovementState DashMovementState { get; private set; }

        #endregion

        public event Action EntityDiedAction;
        public event Action EntityRespawnedAction;

        protected virtual void Awake()
        {
            EntityRigidbody = GetComponent<Rigidbody2D>();
            EntityCollider = GetComponent<Collider2D>();
            
            _collision.Initialize(this);
            _gravity.Initialize(this);
            EntityComponents = GetComponentsInChildren<EntityComponent>();
            InitializeComponents();
            
            MovementStateMachine = new StateMachine<BaseMovementState>();
            
            // Airborne
            JumpAirborneState = new JumpAirborneState(this, _feedbacks.JumpFeedbacks, MovementStateMachine);
            FallAirborneState = new FallAirborneState(this, _feedbacks.FallFeedbacks, MovementStateMachine);
            WallSlideWallingState = new WallSlideWallingState(this, _feedbacks.WallSlideFeedbacks, MovementStateMachine);
            WallJumpWallingState = new WallJumpWallingState(this, _feedbacks.WallJumpFeedbacks, MovementStateMachine);
            // Grounded
            IdleGroundedState = new IdleGroundedState(this, _feedbacks.IdleGroundedFeedbacks, MovementStateMachine);
            MoveGroundedState = new MoveGroundedState(this, _feedbacks.MoveGroundedFeedbacks, MovementStateMachine);
            // Both
            DashMovementState = new DashMovementState(this, _feedbacks.DashFeedbacks, MovementStateMachine);
            
            MovementStateMachine.Initialize(FallAirborneState);
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

        public virtual void TakeDamage()
        {
            EntityRigidbody.velocity = Vector2.zero;
            EntityRigidbody.bodyType = RigidbodyType2D.Static;
            EntityCollider.enabled = false;
            IsDead = true;
            EntityDiedAction?.Invoke();
        }
        
        public void EntityRespawn(Vector3 respawnPos)
        {
            StartCoroutine(Respawn_Coroutine(respawnPos));
        }
        
        protected virtual IEnumerator Respawn_Coroutine(Vector3 respawnPos)
        {
            yield return new WaitForSeconds(_respawnDelay);
            
            EntityRigidbody.bodyType = RigidbodyType2D.Dynamic;
            EntityCollider.enabled = true;
            IsDead = false;
            transform.position = respawnPos;
            EntityRespawnedAction?.Invoke();
        }
        
        public void AddToTransformPosition(Vector2 positionOffset)
        {
            transform.position += (Vector3)positionOffset;
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
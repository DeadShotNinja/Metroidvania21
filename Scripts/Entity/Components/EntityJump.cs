using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Metro
{
    /// <summary>
    /// Entity component for Jumping
    /// </summary>
    public class EntityJump : EntityComponent
    {
        [Header("Jumping")]
        [Tooltip("The number of jumps this entity can perform before needing to land.")]
        [SerializeField] private int _allowedJumps = 1;
        [Tooltip("Maximum height the entity can achieve during a jump.")]
        [SerializeField] private float _jumpSpeed = 30f;
        [Tooltip("Duration after leaving a platform during which the entity can still jump.")]
        [SerializeField] private float _coyoteTimeThreshold = 0.1f;
        [Tooltip("Duration for which a jump input is considered 'buffered' and valid.")]
        [SerializeField] private float _jumpBuffer = 0.1f;

        [Header("Wall Jumping")]
        [Tooltip("If enabled, will allow this entity to wall jump.")]
        [SerializeField] private bool _allowWallJumping = true;
        [Tooltip("How high UP can this entity jump.")]
        [ShowIf(nameof(_allowWallJumping))]
        [SerializeField] private float _wallJumpHeight = 30f;
        [Tooltip("How far to the SIDE can this entity jump.")]
        [ShowIf(nameof(_allowWallJumping))]
        [SerializeField] private float _wallJumpDistance = 20f;
        [Tooltip("Minimum time this entity has to be in a jumping state.")]
        [ShowIf(nameof(_allowWallJumping))]
        [SerializeField] private float _wallJumpMinDuration = 0.25f;

        private Rigidbody2D _rb;

        public bool CanUseCoyote => _entity.Collision.IsCoyoteUsable 
                                     && !_entity.Collision.IsGrounded 
                                     && _entity.Collision.TimeLeftGrounded + _coyoteTimeThreshold > Time.time;
        public bool HasBufferedJump => _entity.Collision.IsGrounded 
                                        && LastJumpPressed + _jumpBuffer > Time.time;

        public float WallJumpMinDuration => _wallJumpMinDuration;
        public bool AllowWalljumping => _allowWallJumping;
        public int AllowedJumps => _allowedJumps;
        
        public int JumpCount { get; set; } = 0;
        public float LastJumpPressed { get; set; }
        
        public override void Initialize(BaseEntity entity)
        {
            base.Initialize(entity);

            _rb = _entity.EntityRigidbody;
            Type = ComponentType.Jump;
            
            _entity.Collision.OnGrounded += Event_OnGrounded;
        }
        
        public override void CleanUp()
        {
            _entity.Collision.OnGrounded -= Event_OnGrounded;
        }
        
        public void PerformJump()
        {
            //LastJumpPressed = Time.time;
            
            JumpCount++;
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
            _entity.Gravity.EndedJumpEarly = false;
            _entity.Collision.IsCoyoteUsable = false;
            _entity.Collision.TimeLeftGrounded = float.MinValue;
        }
        
        public void PerformWallJump()
        {
            float jumpDirection = _entity.Collision.IsWallLeft ? 1f : -1f;
            _rb.velocity = new Vector2(_wallJumpDistance * jumpDirection, _wallJumpHeight);
        }
        
        public void JumpReleased()
        {
            if (!_entity.Collision.IsGrounded && !_entity.Gravity.EndedJumpEarly && _rb.velocity.y > 0)
            {
                _entity.Gravity.EndedJumpEarly = true;
            }
        }
        
        private void Event_OnGrounded()
        {
            JumpCount = 0;
        }
    }
}
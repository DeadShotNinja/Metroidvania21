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

        private Rigidbody2D _rb;
        private float _lastJumpPressed;
        private bool _jumpingThisFrame;
        private int _jumpCount = 0;

        private bool CanUseCoyote => _entity.Collision.IsCoyoteUsable 
                                     && !_entity.Collision.IsGrounded 
                                     && _entity.Collision.TimeLeftGrounded + _coyoteTimeThreshold > Time.time;
        private bool HasBufferedJump => _entity.Collision.IsGrounded 
                                        && _lastJumpPressed + _jumpBuffer > Time.time;
        
        public override void Initialize(BaseEntity entity)
        {
            base.Initialize(entity);

            _rb = _entity.EntityRigidbody;
            
            _entity.Collision.OnGrounded += Event_OnGrounded;
        }
        
        public override void CleanUp()
        {
            _entity.Collision.OnGrounded -= Event_OnGrounded;
        }
        
        public void TryJump() // TRY JUMP is being attempted early when we hit space bar? maybe somehow being reset and unabalable on next space bar press?
        {
            _lastJumpPressed = Time.time;
            
            if (CanUseCoyote || HasBufferedJump || _jumpCount < _allowedJumps)
            {
                _jumpCount++;
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
                _entity.Gravity.EndedJumpEarly = false;
                _entity.Collision.IsCoyoteUsable = false;
                _entity.Collision.TimeLeftGrounded = float.MinValue;
                _jumpingThisFrame = true;
            }
            else
            {
                _jumpingThisFrame = false;
            }
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
            _jumpCount = 0;
        }
    }
}
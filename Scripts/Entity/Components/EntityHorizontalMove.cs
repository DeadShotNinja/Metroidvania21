using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// Entity movement component.
    /// </summary>
    public class EntityHorizontalMove : EntityComponent
    {
        [Header("Movement")]
        [Tooltip("Acceleration rate of the entity when moving horizontally.")]
        [SerializeField] private float _acceleration = 90f;
        [Tooltip("Maximum speed at which the entity can move horizontally.")]
        [SerializeField] private float _moveClamp = 13f;
        [Tooltip("Deceleration rate of the entity when it stops moving horizontally.")]
        [SerializeField] private float _deAcceleration = 60f;
        [Tooltip("Bonus speed applied when the entity is at the apex of a jump or fall.")]
        [SerializeField] private float _apexBonus = 2f;

        [Header("Dash")]
        [Tooltip("Determines if the entity is allowed to dash.")]
        [SerializeField] private bool _allowDash = true;
        [ShowIf(nameof(_allowDash))]
        [Tooltip("Speed at which the entity dashes.")]
        [SerializeField] private float _dashSpeed = 40f;
        [ShowIf(nameof(_allowDash))]
        [Tooltip("Duration for which the dash lasts.")]
        [SerializeField] private float _dashDuration = 0.2f;

        private Rigidbody2D _rb;
        private float _lastXMovement;
        private bool _dashNeedsReset = false;
        
        public float DashDuration => _dashDuration;
        public bool DashNeedsRest => _dashNeedsReset;
        
        public override void Initialize(BaseEntity entity)
        {
            base.Initialize(entity);

            _rb = _entity.EntityRigidbody;
            Type = ComponentType.HorizontalMove;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_dashNeedsReset && _entity.Collision.IsGrounded) _dashNeedsReset = false;
        }

        public void ApplyMovement(float xMovement)
        {
            if (xMovement != 0f)
            {
                _lastXMovement = xMovement;
                float newHorizontalSpeed = _rb.velocity.x + (xMovement * _acceleration * Time.fixedDeltaTime);
                newHorizontalSpeed = Mathf.Clamp(newHorizontalSpeed, -_moveClamp, _moveClamp);
                
                float apexBonus = Mathf.Sign(xMovement) * _apexBonus * _entity.Gravity.ApexPoint;
                newHorizontalSpeed += apexBonus * Time.fixedDeltaTime;

                _rb.velocity = new Vector2(newHorizontalSpeed, _rb.velocity.y);
            }
            else
            {
                float newHorizontalSpeed = Mathf.MoveTowards(_rb.velocity.x, 0f, _deAcceleration * Time.fixedDeltaTime);
                _rb.velocity = new Vector2(newHorizontalSpeed, _rb.velocity.y);
            }
            
            if ((_rb.velocity.x > 0f && _entity.Collision.IsWallRight) || (_rb.velocity.x < 0f && _entity.Collision.IsWallLeft))
            {
                _rb.velocity = new Vector2(0f, _rb.velocity.y);
            }
        }

        public void ApplyDash(float xDirection)
        {
            // TODO: account for player not selecting a direction.
            //if (xDirection == 0f) xDirection = transform.localScale.x;
            float newDirection;
            if (xDirection == 0f) newDirection = _lastXMovement;
            else newDirection = xDirection;

            _rb.velocity = new Vector2(_dashSpeed * newDirection, 0f);
        }
        
        public void CompleteDash()
        {
            _dashNeedsReset = true;
        }
    }
}
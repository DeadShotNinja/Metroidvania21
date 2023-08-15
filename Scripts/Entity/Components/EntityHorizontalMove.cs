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
        [SerializeField] private bool _allowDash = true;

        private Rigidbody2D _rb;
        
        public override void Initialize(BaseEntity entity)
        {
            base.Initialize(entity);

            _rb = _entity.EntityRigidbody;
            Type = ComponentType.HorizontalMove;
        }

        public void ApplyMovement(float xMovement)
        {
            if (xMovement != 0f)
            {
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
    }
}
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// Entity movement component.
    /// </summary>
    public class EntityHorizontalMove : EntityComponent
    {
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _maxAcceleration = 10f, _maxAirAcceleration = 1f;


        private Rigidbody2D _rb;

        public override void Initialize(BaseEntity entity)
        {
            base.Initialize(entity);

            _rb = _entity.EntityRigidbody;
            Type = ComponentType.HorizontalMove;
        }

        public void ApplyMovement(float xMovement)
        {
            //float move = Mathf.Abs(xMovement) < 0.1f ? 0 : Mathf.Sign(xMovement);

            float acceleration = _entity.Collision.IsGrounded ? _maxAcceleration : _maxAirAcceleration;
            float maxSpeedChange = acceleration * Time.deltaTime;

            _rb.velocity = new Vector2(Mathf.MoveTowards(_rb.velocity.x, xMovement * _maxSpeed, maxSpeedChange), _rb.velocity.y);
        }
    }
}
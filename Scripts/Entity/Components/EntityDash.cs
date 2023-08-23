using UnityEngine;
namespace Metro
{
    /// <summary>
    /// Entity component for Jumping
    /// </summary>
    public class EntityDash : EntityComponent
    {
        [Tooltip("How long we dash for")]
        [SerializeField] private float _dashDuration;
        [Tooltip("How fast we dash")]
        [SerializeField] private float _dashSpeed;
        [Tooltip("Amount of time after dash we cool down for")]
        [SerializeField] private float _dashCooldown;

        private float _dashCount;
        private float _timeSinceLastDash;

        public float TimeSinceLastDash { get => _timeSinceLastDash;}
        public float DashDuration { get => _dashDuration; }

        public override void Initialize(BaseEntity entity)
        {
            base.Initialize(entity);

            _timeSinceLastDash = Time.time;
        }
        private void Update()
        {
            if(_entity.Collision.IsGrounded && TimeSinceLastDash + DashDuration < Time.time)
                _dashCount = 0;
        }
        public bool CanDash()
        {
            if (_dashCount > 0)
                return false;
            if (_timeSinceLastDash + _dashDuration + _dashCooldown > Time.time)
                return false;
            if (_entity.InputProvider.MoveInput.magnitude == 0)
                return false;

            return true;
        }
        public void Dash()
        {
            _timeSinceLastDash = Time.time;
            _dashCount++;
        }
        public void ApplyDash(Vector2 dir)
        {
            dir.y = 0;
            dir.Normalize();

            _entity.EntityRigidbody.velocity = dir * _dashSpeed;
        }
    }
}

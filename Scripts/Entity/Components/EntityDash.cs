using UnityEngine;
namespace Metro
{
    /// <summary>
    /// Entity component for Jumping
    /// </summary>
    public class EntityDash : EntityComponent
    {
        [Header("Dash Setup")]
        [Tooltip("How long we dash for")]
        [SerializeField] private float _dashDuration;
        [Tooltip("How fast we dash")]
        [SerializeField] private float _dashSpeed;
        [Tooltip("Amount of time after dash we cool down for")]
        [SerializeField] private float _dashCooldown;

        [Header("Dash Break Ability")]
        [SerializeField] private LayerMask _breakableLayer;

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
        
        public BreakableProp CheckForBreakable(Vector2 dir)
        {
            Vector2 boxSize = _entity.EntityCollider.bounds.size;
            Vector2 boxOrigin = (Vector2)_entity.transform.position + _entity.EntityCollider.offset;
            Vector2 dashDirection = new Vector2(dir.x, 0f);
            float dashDistance = _dashSpeed * _dashDuration;

            RaycastHit2D hit = Physics2D.BoxCast(boxOrigin, boxSize, 0f, dashDirection, dashDistance, _breakableLayer);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out BreakableProp breakable))
                {
                    return breakable;
                }
            }

            return null;
        }
    }
}

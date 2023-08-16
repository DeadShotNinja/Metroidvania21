using System;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class EntityGravity
    {
        [Tooltip("The maximum negative vertical speed an entity can have (maximum downward speed).")]
        [SerializeField] private float _fallClamp = -40f;
        [Tooltip("Minimum speed at which the entity will start to fall.")]
        [SerializeField] private float _minFallSpeed = 80f;
        [Tooltip("Maximum speed at which the entity will reach during fall.")]
        [SerializeField] private float _maxFallSpeed = 120f;
        [Space(10)]
        [Tooltip("The threshold to determine the jump apex point. Used to calculate falling speed.")]
        [SerializeField] private float _jumpApexThreshold = 10f;
        [Tooltip("Gravity modifier applied when a jump ends early.")]
        [SerializeField] private float _jumpEndEarlyGravityModifier = 3f;

        private BaseEntity _entity;
        private Rigidbody2D _rb;
        private float _fallSpeed;
        private float _apexPoint;

        public bool GravityActive { get; set; } = true;
        public bool EndedJumpEarly { get; set; } = true;
        public float ApexPoint => _apexPoint;
        
        public void Initialize(BaseEntity entity)
        {
            _entity = entity;
            _rb = _entity.EntityRigidbody;
        }
        
        public void CalculateJumpApex()
        {
            if (!_entity.Collision.IsGrounded) 
            {
                _apexPoint = Mathf.InverseLerp(_jumpApexThreshold, 0, Mathf.Abs(_rb.velocity.y));
                _fallSpeed = Mathf.Lerp(_minFallSpeed, _maxFallSpeed, _apexPoint);
            }
            else {
                _apexPoint = 0;
            }
        }
        
        public void ApplyGravity()
        {
            if (GravityActive && !_entity.Collision.IsGrounded) // && !_entity.Collision.IsTouchingWall)
            {
                float fallSpeed = EndedJumpEarly && _rb.velocity.y > 0
                    ? _fallSpeed * _jumpEndEarlyGravityModifier
                    : _fallSpeed;
            
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - fallSpeed * Time.deltaTime);
            
                if (_rb.velocity.y < _fallClamp) _rb.velocity = new Vector2(_rb.velocity.x, _fallClamp);
            }
        }
    }
}
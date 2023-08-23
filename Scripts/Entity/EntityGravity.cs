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
        [Tooltip("Gravity modifier")]
        [SerializeField] private float _Gravity = 80;
        [Tooltip("Quick fall gravity modifier that will be added on top of normal gravity.")]
        [SerializeField] private float _QuickFallGravity = 120;

        private BaseEntity _entity;
        private Rigidbody2D _rb;
        
        public void Initialize(BaseEntity entity)
        {
            _entity = entity;
            _rb = _entity.EntityRigidbody;
        }
        
        public void ApplyGravity()
        {
            _rb.velocity -= new Vector2(0,_Gravity * Time.deltaTime);

            if (!_entity.InputProvider.JumpInput.Held && _rb.velocity.y > -1)
                _rb.velocity -= new Vector2(0, _QuickFallGravity * Time.deltaTime);
        }
    }
}
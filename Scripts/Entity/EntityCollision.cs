using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class EntityCollision
    {
        [Tooltip("The size of the entity that should detect collision.")]
        [SerializeField] private Bounds _entityBounds;
        [Tooltip("The layer mask to look for when checking for ground.")]
        [SerializeField] private LayerMask _groundLayer;
        [Tooltip("The layer mask to look for when checking for wall.")]
        [SerializeField] private LayerMask _wallLayer;
        [Tooltip("The number of rays to shoot from each side, more is better at the cost of performance.")]
        [SerializeField] private int _detectorCount = 5;
        [Tooltip("How far out to check for collision from the entity.")]
        [SerializeField] private float _detectionRayLength = 0.1f;
        [Tooltip("Prevents side detectors from hitting the ground.")]
        [SerializeField] [Range(0.1f, 0.3f)] private float _rayBuffer = 0.1f;
        //[SerializeField, ReadOnly] private bool _isGrounded = false;
        [SerializeField, ReadOnly] private bool _colRight, _colDown, _colLeft; // _colUp 
        
        private BaseEntity _entity;
        private bool _landingThisFrame;
        private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;
        private bool _wasGrounded = false;

        //public Bounds EntityBounds => _entityBounds;
        //public bool IsCollidingUp => _colUp;
        public bool IsGrounded => _colDown;
        public bool IsWallRight => _colRight;
        public bool IsWallLeft => _colLeft;
        //public LayerMask GroundLayer => _groundLayer;
        //public bool IsGrounded => _isGrounded;
        
        public float TimeLeftGrounded { get; set; }
        public bool IsCoyoteUsable { get; set; }

        public event Action OnGrounded;
        
        public void Initialize(BaseEntity entity)
        {
            _entity = entity;
        }
        
        // public void LogicUpdate()
        // {
        //     RunCollisionChecks();
        // }

        // public void PhysicsUpdate()
        // {
        //     RunCollisionChecks();
        //     UpwardsCollisionChecks();
        // }
        
        // public void LateLogicUpdate()
        // {
        //     UpwardsCollisionChecks();
        // }
        
        public void RunCollisionChecks()
        {
             CalculateRayRanged(_entity.transform.position);
        
             _landingThisFrame = false; 
             bool groundedCheck = RunDetection(_raysDown, _groundLayer);
             if (_colDown && !groundedCheck) TimeLeftGrounded = Time.time; 
             else if (!_colDown && groundedCheck)
             {
                 IsCoyoteUsable = true;
                 _landingThisFrame = true;
             }
            
             _colDown = groundedCheck;
             //_colUp = RunDetection(_raysUp);
             _colLeft = RunDetection(_raysLeft, _wallLayer);
             _colRight = RunDetection(_raysRight, _wallLayer);

             //_isGrounded = _colDown;
             if (!_wasGrounded && _colDown)
             {
                 _wasGrounded = true;
                 OnGrounded?.Invoke();
             }
             else if (_wasGrounded && !_colDown) _wasGrounded = false;

             return;
             bool RunDetection(RayRange range, LayerMask mask)
             {
                 return EvaluateRayPositions(range).Any(point =>
                     Physics2D.Raycast(point, range.Dir, _detectionRayLength, mask));
             }
        }
        
        // private void UpwardsCollisionChecks()
        // {
        //     if (IsCollidingUp)
        //     {
        //         if (_entity.Physics.CurrentVerticalSpeed > 0) _entity.Physics.CurrentVerticalSpeed = 0;
        //     }
        // }
        
        private void CalculateRayRanged(Vector3 entityPosition)
        {
            Bounds bounds = new Bounds(entityPosition + _entityBounds.center, _entityBounds.size);
            
            _raysDown = new RayRange(bounds.min.x + _rayBuffer, bounds.min.y, bounds.max.x - _rayBuffer, bounds.min.y, Vector2.down);
            _raysUp = new RayRange(bounds.min.x + _rayBuffer, bounds.max.y, bounds.max.x - _rayBuffer, bounds.max.y, Vector2.up);
            _raysLeft = new RayRange(bounds.min.x, bounds.min.y + _rayBuffer, bounds.min.x, bounds.max.y - _rayBuffer, Vector2.left);
            _raysRight = new RayRange(bounds.max.x, bounds.min.y + _rayBuffer, bounds.max.x, bounds.max.y - _rayBuffer, Vector2.right);
        }
        
        private IEnumerable<Vector2> EvaluateRayPositions(RayRange range)
        {
            for (int i = 0; i < _detectorCount; i++)
            {
                float t = (float)i / (_detectorCount - 1);
                yield return Vector2.Lerp(range.Start, range.End, t);
            }
        }
        
        public void GizmosToDraw(Vector3 entityPosition)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(entityPosition + _entityBounds.center, _entityBounds.size);
        
            if (!Application.isPlaying) {
                CalculateRayRanged(entityPosition);
                Gizmos.color = Color.blue;
                foreach (RayRange range in new List<RayRange> { _raysUp, _raysRight, _raysDown, _raysLeft }) {
                    foreach (Vector2 point in EvaluateRayPositions(range)) {
                        Gizmos.DrawRay(point, range.Dir * _detectionRayLength);
                    }
                }
            }
        
            if (!Application.isPlaying) return;
        
            Gizmos.color = Color.red;
            Vector3 move = new Vector3(_entity.EntityRigidbody.velocity.x, _entity.EntityRigidbody.velocity.y) * Time.deltaTime;
            Gizmos.DrawWireCube(entityPosition + _entityBounds.center + move, _entityBounds.size);
        }
    }
    
    public struct RayRange
    {
        public readonly Vector2 Start, End, Dir;
        
        public RayRange(float x1, float y1, float x2, float y2, Vector2 dir)
        {
            Start = new Vector2(x1, y1);
            End = new Vector2(x2, y2);
            Dir = dir;
        }
    }
}
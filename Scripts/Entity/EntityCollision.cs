using System;
using System.Collections.Generic;
using System.Linq;
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
        [Tooltip("Checking this true will show the rays in play mode. (Have to select the GameObject)")]
        [SerializeField] private bool _showDebugRays = false;
        
        private BaseEntity _entity;
        private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;
        private bool _groundColRight, _groundColLeft, _groundColDown;
        private bool _wallColRight, _wallColLeft, _wallColDown;
        private bool _wasGrounded;
        
        public bool IsGrounded => _groundColDown || _wallColDown;
        public bool IsGroundRight => _groundColRight;
        public bool IsGroundLeft => _groundColLeft;
        public bool IsTouchingWall => _wallColLeft || _wallColRight;
        public bool IsWallRight => _wallColRight;
        public bool IsWallLeft => _wallColLeft;
        
        public float TimeLeftGrounded { get; set; }
        public bool IsCoyoteUsable { get; set; }
        public bool GroundedThisFrame { get; private set; }
        public bool LeavingGroundedThisFrame { get; private set; }

        public event Action OnGrounded;
        
        public void Initialize(BaseEntity entity)
        {
            _entity = entity;
        }
        
        public void RunCollisionChecks()
        {
             CalculateRayRanged(_entity.transform.position);
        
             GroundedThisFrame = false;
             LeavingGroundedThisFrame = false;
             bool groundDownCheck = RunDetection(_raysDown, _groundLayer);
             bool wallDownCheck = RunDetection(_raysDown, _wallLayer);
             if (IsGrounded && (!groundDownCheck || !wallDownCheck))
             {
                 TimeLeftGrounded = Time.time;
                 LeavingGroundedThisFrame = true;
             }
             else if (!IsGrounded && (groundDownCheck || wallDownCheck))
             {
                 IsCoyoteUsable = true;
                 GroundedThisFrame = true;
             }

             _groundColDown = groundDownCheck;
             _groundColLeft = RunDetection(_raysLeft, _groundLayer);
             _groundColRight = RunDetection(_raysRight, _groundLayer);

             _wallColDown = wallDownCheck;
             _wallColLeft = RunDetection(_raysLeft, _wallLayer);
             _wallColRight = RunDetection(_raysRight, _wallLayer);

             if (!_wasGrounded && _groundColDown)
             {
                 _wasGrounded = true;
                 OnGrounded?.Invoke();
             }
             else if (_wasGrounded && !_groundColDown) _wasGrounded = false;

             return;
             bool RunDetection(RayRange range, LayerMask mask)
             {
                 return EvaluateRayPositions(range).Any(point =>
                     Physics2D.Raycast(point, range.Dir, _detectionRayLength, mask));
             }
        }
        
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
        
            if (_showDebugRays || !Application.isPlaying) {
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
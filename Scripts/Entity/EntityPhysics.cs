using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class EntityPhysics
    {
        // [Tooltip("Raising this value increases collision accuracy at the cost of performance.")]
        // [SerializeField] private int _freeColliderIterations = 10;
        // [SerializeField, ReadOnly] private Vector2 _velocity;
        //
        // private BaseEntity _entity;
        // private Vector3 _lastPosition;
        //
        // public float CurrentHorizontalSpeed { get; set; }
        // public float CurrentVerticalSpeed { get; set; }
        // public Vector2 Velocity => _velocity;
        //
        // public void Initialize(BaseEntity entity)
        // {
        //     _entity = entity;
        // }
        //
        // public void LogicUpdate()
        // {
        //     _velocity = (_entity.transform.position - _lastPosition) / Time.deltaTime;
        //     _lastPosition = _entity.transform.position;
        // }
        //
        // public void LateLogicUpdate()
        // {
        //     ApplyPhysics();
        // }
        //
        // private void ApplyPhysics()
        // {
        //     Vector3 pos = _entity.transform.position + _entity.Collision.EntityBounds.center;
        //     //RawMovement = new Vector3(_currentHorizontalSpeed, _currentVerticalSpeed);
        //     Vector3 rawMovement = new Vector3(_entity.Physics.CurrentHorizontalSpeed, _entity.Physics.CurrentVerticalSpeed);
        //     Vector3 move = rawMovement * Time.deltaTime;
        //     Vector3 furthestPoint = pos + move;
        //     
        //     Collider2D hit = Physics2D.OverlapBox(furthestPoint, _entity.Collision.EntityBounds.size, 0f, _entity.Collision.GroundLayer);
        //     if (!hit)
        //     {
        //         _entity.transform.position += move;
        //         return;
        //     }
        //     
        //     Vector3 positionToMoveTo = _entity.transform.position;
        //     for (int i = 0; i < _freeColliderIterations; i++)
        //     {
        //         float t = (float)i / _freeColliderIterations;
        //         Vector2 posToTry = Vector2.Lerp(pos, furthestPoint, t);
        //     
        //         if (Physics2D.OverlapBox(posToTry, _entity.Collision.EntityBounds.size, 0f, _entity.Collision.GroundLayer))
        //         {
        //             _entity.transform.position = positionToMoveTo;
        //             
        //             // Landed on corner or hit top of collider on ledge, move entity slightly
        //             if (i == 1)
        //             {
        //                 if (_entity.Physics.CurrentVerticalSpeed < 0) _entity.Physics.CurrentVerticalSpeed = 0;
        //                 Vector3 dir = _entity.transform.position - hit.transform.position;
        //                 _entity.transform.position += dir.normalized * move.magnitude;
        //             }
        //     
        //             return;
        //         }
        //     
        //         positionToMoveTo = posToTry;
        //     }
        // }
    }
}
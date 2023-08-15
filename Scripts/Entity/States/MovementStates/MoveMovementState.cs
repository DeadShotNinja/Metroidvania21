using System;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class MoveMovementState : BaseMovementState
    {
        private bool _isMoving = false;
        
        public MoveMovementState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (_horizontalMove == null || _entity.InputProvider.MoveInput.x == 0f)
            {
                _isMoving = false;
                _entity.MovementStateMachine.ChangeState(_entity.IdleMovementState);
                return;
            }
            
            if (Mathf.Abs(_entity.InputProvider.MoveInput.x) > 0f)
            {
                _isMoving = true;
            }
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (_isMoving)
            {
                _horizontalMove.ApplyMovement(_entity.InputProvider.MoveInput.x);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
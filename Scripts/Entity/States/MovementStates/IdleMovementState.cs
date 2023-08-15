using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class IdleMovementState : BaseMovementState
    {
        public IdleMovementState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_entity.InputProvider.MoveInput.x != 0f)
            {
                _entity.MovementStateMachine.ChangeState(_entity.MoveMovementState);
            }
            else
            {
                _horizontalMove.ApplyMovement(0f);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
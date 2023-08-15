using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class IdleGroundedState : SuperGroundedState
    {
        public IdleGroundedState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _entity.StateText.SetText("IDLE");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_entity.InputProvider.MoveInput.x != 0f)
            {
                _entity.MovementStateMachine.ChangeState(_entity.MoveGroundedState);
            }
            else if (_horizontalMove != null)
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
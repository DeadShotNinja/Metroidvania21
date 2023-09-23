using MoreMountains.Feedbacks;

namespace Metro
{
    /// <summary>
    /// Idle state for when the entity is not moving.
    /// </summary>
    public class IdleGroundedState : SuperGroundedState
    {
        public IdleGroundedState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            
            _entity.StateText.SetText("IDLE");
            if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.IdleBool, true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (ShouldSwitchToMove())
            {
                _entity.MovementStateMachine.ChangeState(_entity.MoveGroundedState);
                return;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetFloat(_entity.AnimatorData.XInputFloat, 0f);
            _horizontalMove.ApplyMovement(0f);
        }

        public override void Exit()
        {
            base.Exit();
            
            if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.IdleBool, false);
        }

        private bool ShouldSwitchToMove()
        {
            if (!_colLeft && _entity.InputProvider.MoveInput.x < 0f)
                return true;

            if (!_colRight && _entity.InputProvider.MoveInput.x > 0f)
                return true;

            return false;
        }
    }
}
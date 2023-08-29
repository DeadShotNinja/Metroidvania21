using MoreMountains.Feedbacks;

namespace Metro
{
    /// <summary>
    /// Idle state for when the entity is not moving.
    /// </summary>
    public class IdleGroundedState : SuperGroundedState
    {
        public IdleGroundedState(BaseEntity entity, MMFeedbacks feedbacks, 
            StateMachine<BaseMovementState> stateMachine) : base(entity, feedbacks, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            
            _entity.StateText.SetText("IDLE");
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
            
            _horizontalMove.ApplyMovement(0f);
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
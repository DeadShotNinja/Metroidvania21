namespace Metro
{
    /// <summary>
    /// Moving state while on the ground.
    /// </summary>
    public class MoveGroundedState : SuperGroundedState
    {
        public MoveGroundedState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            
            _entity.StateText.SetText("MOVING");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (ShouldSwitchToIdle())
            {
                _entity.MovementStateMachine.ChangeState(_entity.IdleGroundedState);
                return;
            }
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            _horizontalMove.ApplyMovement(_entity.InputProvider.MoveInput.x);
        }
        
        private bool ShouldSwitchToIdle()
        {
            if (_colLeft && _entity.InputProvider.MoveInput.x < 0f)
                return true;
            
            if (_colRight && _entity.InputProvider.MoveInput.x > 0f)
                return true;

            return _entity.InputProvider.MoveInput.x == 0f;
        }
    }
}
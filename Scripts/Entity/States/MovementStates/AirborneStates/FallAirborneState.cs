namespace Metro
{
	public class FallAirborneState : SuperAirborneState
	{
		public FallAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("FALLING");
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
		
		private bool ShouldSwitchToIdle()
		{
			return _entity.Collision.IsGrounded;
		}
	}
}
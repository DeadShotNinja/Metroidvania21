namespace Metro
{
	public class JumpAirborneState : SuperAirborneState
	{
		public JumpAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();

			_entity.StateText.SetText("JUMPING");

            _jump.PerformJump();
        }

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (ShouldSwitchToFall())
			{
				_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		private bool ShouldSwitchToFall()
		{
			return _entity.EntityRigidbody.velocity.y < 0f;
		}
	}
}
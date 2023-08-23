namespace Metro
{
	public class WallJumpWallingState : SuperWallingState
	{
		public WallJumpWallingState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();

			_entity.StateText.SetText("WALL-JUMPING");

            _jump.PerformWallJump();
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

		public override void Exit()
		{
			base.Exit();
		}

		private bool ShouldSwitchToFall()
		{
            return _entity.EntityRigidbody.velocity.y < 0f;
        }
	}
}
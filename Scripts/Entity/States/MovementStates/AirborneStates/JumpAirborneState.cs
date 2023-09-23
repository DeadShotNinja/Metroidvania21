using System.Diagnostics;

namespace Metro
{
	public class JumpAirborneState : SuperAirborneState
	{
		public JumpAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();

			_entity.StateText.SetText("JUMPING");
			if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.JumpingBool, true);
            if (GameDatabase.Instance != null) GameDatabase.Instance.GetEntityAudioEvent(EntityAudioType.Play_PlayerJump)?.Post(_entity.gameObject);

            if (ShouldJump())
			{
				_jump.PerformJump();
			}
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
			
			if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.JumpingBool, false);
		}

		private bool ShouldJump()
		{
			if (!_entity.InputProvider.JumpInput.Pressed && _entity.EntityRigidbody.velocity.y > 0f)
				return false;

			return true;
		}

		private bool ShouldSwitchToFall()
		{
			return _entity.EntityRigidbody.velocity.y <= 0f;
		}
	}
}
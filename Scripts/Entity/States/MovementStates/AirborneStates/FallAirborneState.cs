using MoreMountains.Feedbacks;

namespace Metro
{
	public class FallAirborneState : SuperAirborneState
	{
		public FallAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("FALLING");
			if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.FallingBool, true);
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (ShouldSwitchToIdle())
			{
				// TODO: this might need to be designed a bit different. Maybe one for start and one for end?
				if (_entity.Feedbacks.FallFeedbacks != null) _entity.Feedbacks.FallFeedbacks.PlayFeedbacks();

                if (GameDatabase.Instance != null)
                    GameDatabase.Instance.GetEntityAudioEvent(EntityAudioType.Play_PlayerLand)?.Post(_entity.gameObject);

                _entity.MovementStateMachine.ChangeState(_entity.IdleGroundedState);
				return;
			}
		}

		public override void Exit()
		{
			base.Exit();
			
			if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.FallingBool, false);
		}

		private bool ShouldSwitchToIdle()
		{
			return _entity.Collision.IsGrounded;
		}
	}
}
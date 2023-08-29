using MoreMountains.Feedbacks;

namespace Metro
{
	public class FallAirborneState : SuperAirborneState
	{
		public FallAirborneState(BaseEntity entity, MMFeedbacks feedbacks, 
			StateMachine<BaseMovementState> stateMachine) : base(entity, feedbacks, stateMachine) { }

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
				// TODO: this might need to be designed a bit different. Maybe one for start and one for end?
				if (_feedbacks != null) _feedbacks.PlayFeedbacks();
				
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
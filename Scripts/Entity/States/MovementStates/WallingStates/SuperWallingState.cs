using MoreMountains.Feedbacks;
using UnityEngine;

namespace Metro
{
	public class SuperWallingState : BaseMovementState
	{
		public SuperWallingState(BaseEntity entity, MMFeedbacks feedbacks, 
			StateMachine<BaseMovementState> stateMachine) : base(entity, feedbacks, stateMachine) { }
		
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
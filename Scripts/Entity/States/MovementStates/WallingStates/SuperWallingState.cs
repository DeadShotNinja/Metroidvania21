using UnityEngine;

namespace Metro
{
	public class SuperWallingState : BaseMovementState
	{
		public SuperWallingState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }
		
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
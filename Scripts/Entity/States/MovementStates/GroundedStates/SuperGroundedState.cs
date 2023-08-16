using UnityEngine;

namespace Metro
{
	public abstract class SuperGroundedState : BaseMovementState
	{
		protected bool _colLeft, _colRight;
		
		protected SuperGroundedState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			_colRight = _entity.Collision.IsWallRight || _entity.Collision.IsGroundRight;
			_colLeft = _entity.Collision.IsWallLeft || _entity.Collision.IsGroundLeft;
			
			if (ShouldSwitchToJump())
			{
				Debug.Log("Switching to jump");
				_entity.MovementStateMachine.ChangeState(_entity.JumpAirborneState);
				return;
			}
			// else if (_horizontalMove != null && _entity.InputProvider.DashInput.Pressed)
			// {
			// 	_entity.MovementStateMachine.ChangeState(_entity.DashMovementState);
			// 	return;
			// }
			if (ShouldSwitchToFall())
			{
				_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
				return;
			}
		}
		
		private bool ShouldSwitchToJump()
		{
			// Can do a celling collision check here too.
			
			
			
			return _entity.InputProvider.JumpInput.Pressed;
		}
		
		private bool ShouldSwitchToFall()
		{
			if (_entity.Collision.IsGrounded)
				return false;
			
			return _entity.EntityRigidbody.velocity.y < 0f;
		}
	}
}
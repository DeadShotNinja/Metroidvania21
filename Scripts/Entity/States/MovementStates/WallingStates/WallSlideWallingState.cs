using UnityEngine;

namespace Metro
{
	public class WallSlideWallingState : SuperWallingState
	{
		public WallSlideWallingState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("SLIDING");
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			// bool notPressingTowardsWall = (_entity.Collision.IsWallRight && _entity.InputProvider.MoveInput.x <= 0f) 
			//                               || (_entity.Collision.IsWallLeft && _entity.InputProvider.MoveInput.x >= 0f);
			// bool notTouchingWall = !_entity.Collision.IsTouchingWall;
			//
			// if (_entity.Collision.IsGrounded)
			// {
			// 	_entity.MovementStateMachine.ChangeState(_entity.IdleGroundedState);
			// }
			// else if (notPressingTowardsWall || notTouchingWall)
			// {
			// 	_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
			// }
			// else if (_entity.InputProvider.JumpInput.Pressed)
			// {
			// 	_entity.MovementStateMachine.ChangeState(_entity.WallJumpWallingState);
			// }
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			// if (_wallSlide != null) _wallSlide.ApplySlide();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
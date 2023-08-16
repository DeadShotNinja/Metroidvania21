using UnityEngine;

namespace Metro
{
	public class WallSlideWallingState : SuperWallingState
	{
		private bool _colUp;
		
		public WallSlideWallingState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("SLIDING");
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			_colUp = _entity.Collision.IsWallUp || _entity.Collision.IsGroundUp;
			
			if (ShouldSwitchToFall())
			{
				_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
				return;
			}
			
			if (ShouldSwitchToWallJump())
			{
				_entity.MovementStateMachine.ChangeState(_entity.WallJumpWallingState);
				return;
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			_wallSlide.ApplySlide();
		}
		
		private bool ShouldSwitchToFall()
		{
			if (_entity.Collision.IsWallRight && _entity.InputProvider.MoveInput.x <= 0f)
				return true;

			if (_entity.Collision.IsWallLeft && _entity.InputProvider.MoveInput.x >= 0f)
				return true;

			return !_entity.Collision.IsTouchingWall;
		}
		
		private bool ShouldSwitchToWallJump()
		{
			if (!_jump.AllowWalljumping)
				return false;
			
			if (!_entity.InputProvider.JumpInput.Pressed)
				return false;

			if (_colUp)
				return false;

			if (_entity.Collision.IsGrounded)
				return false;
			
			return _entity.Collision.IsTouchingWall || _entity.Collision.OnWallThisFrame;
		}
	}
}
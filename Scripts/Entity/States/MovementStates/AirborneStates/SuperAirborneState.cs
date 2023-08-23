using UnityEngine;

namespace Metro
{
	public abstract class SuperAirborneState : BaseMovementState
	{
		protected bool _colUp;
		
		public SuperAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			_colUp = _entity.Collision.IsWallUp || _entity.Collision.IsGroundUp;
			
			if (ShouldSwitchToWallSlide())
			{
				_entity.MovementStateMachine.ChangeState(_entity.WallSlideWallingState);
				return;
			}
			
			if (ShouldSwitchToDash())
			{
				_entity.MovementStateMachine.ChangeState(_entity.DashMovementState);
				return;
			}
			
			if (ShouldSwitchToJump())
			{
				_entity.MovementStateMachine.ChangeState(_entity.JumpAirborneState);
				return;
			}
			
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			// This needs to be modified for airborne movement.
			_horizontalMove.ApplyMovement(_entity.InputProvider.MoveInput.x);
		}
		
		private bool ShouldSwitchToDash()
		{
			if (!_dash.CanDash())
				return false;
			
			return _entity.InputProvider.DashInput.Pressed;
		}
		
		private bool ShouldSwitchToJump()
		{
			if (!_entity.InputProvider.JumpInput.Pressed)
				return false;
			_jump.LastJumpPressed = Time.time;

			if (_colUp)
				return false;

			if (_jump.CanUseCoyote && _jump.JumpCount == 0)
				return true;
				
			if (!_jump.CanUseCoyote && _jump.JumpCount == 0 && _jump.AllowedJumps > 1)
			{
				_jump.JumpCount++;
				return true;
			}

			if (_jump.JumpCount > 0 && _jump.JumpCount < _jump.AllowedJumps)
				return true;
			
			return _entity.Collision.GroundedThisFrame;
		}
		
		private bool ShouldSwitchToWallSlide()
		{
			if (_entity.Collision.IsWallLeft && _entity.InputProvider.MoveInput.x < 0f)
				return true;

			if (_entity.Collision.IsWallRight && _entity.InputProvider.MoveInput.x > 0f)
				return true;

			return false;
		}
	}
}
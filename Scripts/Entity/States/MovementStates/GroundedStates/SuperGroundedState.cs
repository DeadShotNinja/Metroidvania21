using MoreMountains.Feedbacks;
using UnityEngine;

namespace Metro
{
	public abstract class SuperGroundedState : BaseMovementState
	{
		protected bool _colLeft, _colRight, _colUp;
		
		protected SuperGroundedState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();
			
			if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.GroundedBool, true);            

            if (Time.time - _jump.LastJumpPressed <= _jump.JumpPreBufferTime)
			{
				_entity.MovementStateMachine.ChangeState(_entity.JumpAirborneState);
				return;
			}
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			_colRight = _entity.Collision.IsWallRight || _entity.Collision.IsGroundRight;
			_colLeft = _entity.Collision.IsWallLeft || _entity.Collision.IsGroundLeft;
			_colUp = _entity.Collision.IsWallUp || _entity.Collision.IsGroundUp;
			
			if (ShouldSwitchToFall())
			{
				_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
				return;
			}
			
			if (ShouldSwitchToJump())
			{
				_entity.MovementStateMachine.ChangeState(_entity.JumpAirborneState);
				return;
			}
			
			if (ShouldSwitchToDash())
			{
				_entity.MovementStateMachine.ChangeState(_entity.DashMovementState);
				return;
			}
		}

		public override void Exit()
		{
			base.Exit();
			
			if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.GroundedBool, false);
		}

		private bool ShouldSwitchToJump()
		{
			if (!_entity.Collision.IsGrounded && _entity.EntityRigidbody.velocity.y > 0f)
				return true;
			
			if (!_entity.InputProvider.JumpInput.Pressed)
				return false;
			_jump.LastJumpPressed = Time.time;

			if (_colUp)
				return false;

			if (_entity.Collision.GroundedThisFrame)
				return true;
			
			return _jump.HasBufferedJump;
		}
		
		private bool ShouldSwitchToDash()
		{
			if (!_dash.CanDash() || !_dash.AllowDash)
				return false;

			return _entity.InputProvider.DashInput.Pressed;
		}
		
		private bool ShouldSwitchToFall()
		{
			if (_entity.Collision.IsGrounded)
				return false;
			
			return _entity.EntityRigidbody.velocity.y < 0f;
		}
	}
}
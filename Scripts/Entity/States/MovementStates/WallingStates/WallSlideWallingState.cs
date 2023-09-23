using MoreMountains.Feedbacks;

namespace Metro
{
	public class WallSlideWallingState : SuperWallingState
	{
		public WallSlideWallingState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("SLIDING");
			if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.WallSlidingBool, true);
            if (GameDatabase.Instance != null) GameDatabase.Instance.GetEntityAudioEvent(EntityAudioType.Play_PlayerSlideLoop)?.Post(_entity.gameObject);
        }

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
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
			
			if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetFloat(_entity.AnimatorData.XInputFloat, 
				_entity.InputProvider.MoveInput.x);
			_wallSlide.ApplySlide();
		}

		public override void Exit()
		{
			base.Exit();
			
			if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetBool(_entity.AnimatorData.WallSlidingBool, false);
            if (GameDatabase.Instance != null) GameDatabase.Instance.GetEntityAudioEvent(EntityAudioType.Stop_PlayerSlideLoop)?.Post(_entity.gameObject);
        }

		private bool ShouldSwitchToFall()
		{
			if (_entity.Collision.IsWallRight && _entity.InputProvider.MoveInput.x < 0f)
				return true;

			if (_entity.Collision.IsWallLeft && _entity.InputProvider.MoveInput.x > 0f)
				return true;

			return !_entity.Collision.IsTouchingWall;
		}
		
		private bool ShouldSwitchToWallJump()
		{
			if (!_jump.AllowWalljumping)
				return false;
			
			if (!_entity.InputProvider.JumpInput.Pressed)
				return false;

			if (_entity.Collision.IsWallUp || _entity.Collision.IsGroundUp)
				return false;

			if (_entity.Collision.IsGrounded)
				return false;
			
			return _entity.Collision.IsTouchingWall || _entity.Collision.OnWallThisFrame;
		}
	}
}
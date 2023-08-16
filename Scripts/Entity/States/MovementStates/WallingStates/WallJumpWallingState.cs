using UnityEngine;

namespace Metro
{
	public class WallJumpWallingState : SuperWallingState
	{
		private bool _wallJumpTriggered;
		private float _stateLockTimer;
		private float _wallDirection;
		private float _checkBuffer;
		
		public WallJumpWallingState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();

			_entity.StateText.SetText("WALL-JUMPING");
			_wallJumpTriggered = true;
			_stateLockTimer = _jump.WallJumpMinDuration + Time.time;
			_checkBuffer = Time.time + 0.1f;
			//_entity.Gravity.GravityActive = false;

			if (_entity.Collision.IsWallLeft) _wallDirection = -1f;
			else if (_entity.Collision.IsWallRight) _wallDirection = 1f;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (ShoulSwitchToFall())
			{
				_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			if (_wallJumpTriggered)
			{
				_jump.PerformWallJump();
				_wallJumpTriggered = false;
			}
		}

		public override void Exit()
		{
			base.Exit();

			//_entity.Gravity.GravityActive = true;
		}

		private bool ShoulSwitchToFall()
		{
			if (_wallJumpTriggered)
				return false;

			if (_checkBuffer < Time.time && _entity.EntityRigidbody.velocity.x == 0f)
				return true;

			return _stateLockTimer <= Time.time;
		}
	}
}
using UnityEngine;

namespace Metro
{
	public class WallJumpWallingState : SuperWallingState
	{
		private bool _wallJumpTriggered = false;
		private float _stateLockTimer;
		private float _bufferTimer;
		
		public WallJumpWallingState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();

			_entity.StateText.SetText("WALL-JUMPING");
			// _wallJumpTriggered = true;
			// _bufferTimer = _stateEvalBuffer + Time.time;
			// if (_jump != null) _stateLockTimer = _jump.WallJumpMinDuration + Time.time;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			// if (_bufferTimer > Time.time) return;
			//
			// if (_entity.Collision.IsGrounded)
			// {
			// 	_entity.MovementStateMachine.ChangeState(_entity.IdleGroundedState);
			// }
			// else if (_entity.EntityRigidbody.velocity.x == 0f || _stateLockTimer < Time.time)
			// {
			// 	_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
			// }
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			// if (_wallJumpTriggered && _jump != null)
			// {
			// 	_jump.TryWallJump();
			// 	_wallJumpTriggered = false;
			// }
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
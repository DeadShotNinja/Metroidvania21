using UnityEngine;

namespace Metro
{
	public abstract class SuperAirborneState : BaseMovementState
	{
		private float _bufferTimer;
		
		public SuperAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();

			_bufferTimer = _stateEvalBuffer + Time.time;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (_bufferTimer > Time.time) return;
			
			if (_entity.Collision.IsWallLeft && _entity.InputProvider.MoveInput.x < 0f
			    || _entity.Collision.IsWallRight && _entity.InputProvider.MoveInput.x > 0f)
			{
				_entity.MovementStateMachine.ChangeState(_entity.WallSlideWallingState);
				return;
			}
			
			if (_jump != null && _entity.InputProvider.JumpInput.Pressed)
			{
				_entity.MovementStateMachine.ChangeState(_entity.JumpAirborneState);
				return;
			}
			
			if (_entity.Collision.IsGrounded || _entity.EntityRigidbody.velocity.y == 0f)
			{
				if (Mathf.Abs(_entity.InputProvider.MoveInput.x) > 0f)
				{
					_entity.MovementStateMachine.ChangeState(_entity.MoveGroundedState);
					return;
				}
				_entity.MovementStateMachine.ChangeState(_entity.IdleGroundedState);
				return;
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			// This needs to be modified for airborne movement.
			_horizontalMove.ApplyMovement(_entity.InputProvider.MoveInput.x);
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
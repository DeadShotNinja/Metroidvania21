using UnityEngine;

namespace Metro
{
	public class DashMovementState : BaseMovementState
	{
		private float _dashTimer;
		private float _checkBuffer;
		
		public DashMovementState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("DASHING");
			_dashTimer = _horizontalMove.DashDuration + Time.time;
			_checkBuffer = Time.time + 0.1f;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (ShouldSwitchToIdle())
			{
				_entity.MovementStateMachine.ChangeState(_entity.IdleGroundedState);
				return;
			}
			
			if (ShouldSwitchToFall())
			{
				_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
				return;
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();

			if (_dashTimer > Time.time)
			{
				_horizontalMove.ApplyDash(_entity.InputProvider.MoveInput.x);
			}
		}

		public override void Exit()
		{
			base.Exit();
			
			if (_horizontalMove != null)
			{
				_horizontalMove.CompleteDash();
			}
		}
		
		private bool ShouldSwitchToFall()
		{
			if (_checkBuffer < Time.time && _entity.EntityRigidbody.velocity.x == 0f
			    && !_entity.Collision.IsGrounded)
				return true;

			return _dashTimer < Time.time && !_entity.Collision.IsGrounded;
		}
		
		private bool ShouldSwitchToIdle()
		{
			if (_checkBuffer < Time.time && _entity.EntityRigidbody.velocity.x == 0f
			    && _entity.Collision.IsGrounded)
				return true;

			return _dashTimer < Time.time && _entity.Collision.IsGrounded;
		}
	}
}
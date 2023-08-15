using UnityEngine;

namespace Metro
{
	public class DashMovementState : BaseMovementState
	{
		private float _dashTimer;
		
		public DashMovementState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("DASHING");
			_dashTimer = _horizontalMove.DashDuration + Time.time;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (_dashTimer < Time.time || (_horizontalMove != null && _horizontalMove.DashNeedsRest))
			{
				if (_entity.Collision.IsGrounded)
				{
					_entity.MovementStateMachine.ChangeState(_entity.IdleGroundedState);
					return;
				}
				else
				{
					_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
					return;
				}
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			if (_horizontalMove != null && !_horizontalMove.DashNeedsRest)
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
	}
}
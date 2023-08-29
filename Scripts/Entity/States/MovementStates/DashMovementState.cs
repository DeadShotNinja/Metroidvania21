using MoreMountains.Feedbacks;
using UnityEngine;

namespace Metro
{
	public class DashMovementState : BaseMovementState
	{
		private Vector2 _dashDir;
		private BreakableProp _breakableProp;
		
		public DashMovementState(BaseEntity entity, MMFeedbacks feedbacks, 
			StateMachine<BaseMovementState> stateMachine) : base(entity, feedbacks, stateMachine) { }

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("DASHING");
			_dash.Dash();
			_dashDir = _entity.InputProvider.MoveInput;

			_breakableProp = _dash.CheckForBreakable(_dashDir);
			if (_breakableProp != null)
			{
				_breakableProp.HandleBreakable(_entity.gameObject);
			}
			
            _dash.ApplyDash(_dashDir);
        }

		public override void LogicUpdate()
		{
			base.LogicUpdate();



			if (ShouldDash())
				return;

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

			_dash.ApplyDash(_dashDir);
		}

		public override void Exit()
		{
			base.Exit();
			
			if (_breakableProp != null)
			{
				_breakableProp.ResetCollider();
			}
		}
		
		private bool ShouldSwitchToFall()
		{
			if (_entity.EntityRigidbody.velocity.x == 0f
			    && !_entity.Collision.IsGrounded)
				return true;

			return !_entity.Collision.IsGrounded;
		}
		
		private bool ShouldSwitchToIdle()
		{
			if (_entity.EntityRigidbody.velocity.x == 0f
			    && _entity.Collision.IsGrounded)
				return true;

			return _entity.Collision.IsGrounded;
		}
		private bool ShouldDash()
		{
            if (_entity.EntityRigidbody.velocity.magnitude < 0.2f)
                return false;

            if (_dash.TimeSinceLastDash + _dash.DashDuration > Time.time)
                return true;

			return false;
        }
	}
}
using UnityEngine;

namespace Metro
{
	public abstract class SuperGroundedState : BaseMovementState
	{
		protected SuperGroundedState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (_entity.EntityRigidbody.velocity.y > 0f)
			{
				_entity.MovementStateMachine.ChangeState(_entity.JumpAirborneState);
				return;
			}
			else if (_entity.EntityRigidbody.velocity.y < 0f)
			{
				_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
				return;
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
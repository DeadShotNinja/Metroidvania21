using UnityEngine;

namespace Metro
{
	public class DashMovementState : BaseMovementState
	{
		public DashMovementState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("DASHING");
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
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
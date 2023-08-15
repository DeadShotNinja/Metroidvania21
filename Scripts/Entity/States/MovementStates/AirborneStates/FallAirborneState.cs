using UnityEngine;

namespace Metro
{
	public class FallAirborneState : SuperAirborneState
	{
		public FallAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("FALLING");
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
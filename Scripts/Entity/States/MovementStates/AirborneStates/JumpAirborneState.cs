using UnityEngine;

namespace Metro
{
	public class JumpAirborneState : SuperAirborneState
	{
		private bool _jumpTriggered;
		
		public JumpAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

		public override void Enter()
		{
			base.Enter();

			_entity.StateText.SetText("JUMPING");
			_jumpTriggered = true;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (_entity.InputProvider.JumpInput.Released)
			{
				_jump.JumpReleased();
			}
			
			if (ShouldSwitchToFall())
			{
				Debug.Log("Switching to fall");
				_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			if (_jumpTriggered)
			{
				//_jump.TryJump();
				_jump.PerformJump();
				_jumpTriggered = false;
			}
		}

		private bool ShouldSwitchToFall()
		{
			if (_jumpTriggered)
				return false;

			return _entity.EntityRigidbody.velocity.y < 0f;
		}
	}
}
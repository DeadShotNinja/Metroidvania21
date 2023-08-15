using UnityEngine;

namespace Metro
{
	public class JumpAirborneState : SuperAirborneState
	{
		private bool _jumpTriggered = false;
		
		public JumpAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();

			_entity.StateText.SetText("JUMPING");
			_jumpTriggered = true;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (_jump != null && _entity.InputProvider.JumpInput.Released)
			{
				_jump.JumpReleased();
			}
			
			// if (!_jumpTriggered && (_entity.Collision.IsWallLeft && _entity.InputProvider.MoveInput.x < 0f
			// 					|| _entity.Collision.IsWallRight && _entity.InputProvider.MoveInput.x > 0f))
			// {
			// 	_entity.MovementStateMachine.ChangeState(_entity.WallSlideAirborneState);
			// 	return;
			// }
			
			if (_entity.EntityRigidbody.velocity.y < 0f)
			{
				_entity.MovementStateMachine.ChangeState(_entity.FallAirborneState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			if (_jumpTriggered)
			{
				_jump.TryJump();
				_jumpTriggered = false;
			}
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
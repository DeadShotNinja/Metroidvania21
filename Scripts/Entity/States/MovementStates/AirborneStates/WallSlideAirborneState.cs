using UnityEngine;

namespace Metro
{
	public class WallSlideAirborneState : SuperAirborneState
	{
		public WallSlideAirborneState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			
			_entity.StateText.SetText("SLIDING");
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (!_entity.Collision.IsWallLeft && _entity.InputProvider.MoveInput.x < 0f
			    && !_entity.Collision.IsWallRight && _entity.InputProvider.MoveInput.x > 0f)
			{
				_entity.MovementStateMachine.ChangeState(_entity.MovementStateMachine.PreviousState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
			if (_wallSlide != null) _wallSlide.ApplySlide();
		}

		public override void Exit()
		{
			base.Exit();
		}
	}
}
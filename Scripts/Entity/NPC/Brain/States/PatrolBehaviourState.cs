using UnityEngine;

namespace Metro
{
	public class PatrolBehaviourState : BaseBehaviourState
	{
		private float _moveDirection;
		
		public PatrolBehaviourState(AIBrain brain, StateMachine<BaseBehaviourState> stateMachine) : base(brain, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();

			_moveDirection = 1f;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			ValidateDirection();
			
			_brain.MoveInput = new Vector2(_moveDirection, 0f);
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		public override void Exit()
		{
			base.Exit();
		}
		
		private void ValidateDirection()
		{
			if (_moveDirection == 1f && !_brain.NPC.Collision.IsRightRayGrounded)
			{
				_moveDirection = -1f;
			}
			else if (_moveDirection == -1f && !_brain.NPC.Collision.IsLeftRayGrounded)
			{
				_moveDirection = 1f;
			}
			else if (!_brain.NPC.Collision.IsLeftRayGrounded && !_brain.NPC.Collision.IsRightRayGrounded)
			{
				_moveDirection = 0f;
			}
		}
	}
}
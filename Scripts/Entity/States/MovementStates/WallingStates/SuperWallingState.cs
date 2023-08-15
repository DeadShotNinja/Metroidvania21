using UnityEngine;

namespace Metro
{
	public class SuperWallingState : BaseMovementState
	{
		public SuperWallingState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine)
		{
		}
	}
}
using UnityEngine;

namespace Metro
{
	public abstract class BaseLevelState : BaseState<BaseLevelState>
	{
		protected BaseLevelState(StateMachine<BaseLevelState> stateMachine) : base(stateMachine)
		{
		}
	}
}
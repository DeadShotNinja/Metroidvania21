using UnityEngine;

namespace Metro
{
	public class PauseLevelState : BaseLevelState
	{
		public PauseLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine)
		{
		}
	}
}
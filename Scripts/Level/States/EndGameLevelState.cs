using UnityEngine;

namespace Metro
{
	public class EndGameLevelState : BaseLevelState
	{
		public EndGameLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine)
		{
		}
	}
}
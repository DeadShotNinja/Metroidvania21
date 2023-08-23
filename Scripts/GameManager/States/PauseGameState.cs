using UnityEngine;

namespace Metro
{
	public class PauseGameState : BaseGameState
	{
		public PauseGameState(GameManager gameManager, StateMachine<BaseGameState> stateMachine) : base(gameManager, stateMachine)
		{
		}
	}
}
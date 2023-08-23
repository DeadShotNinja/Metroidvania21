using UnityEngine;

namespace Metro
{
	public class LoseGameState : BaseGameState
	{
		public LoseGameState(GameManager gameManager, StateMachine<BaseGameState> stateMachine) : base(gameManager, stateMachine)
		{
		}
	}
}
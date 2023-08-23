using UnityEngine;

namespace Metro
{
	public class WinGameState : BaseGameState
	{
		public WinGameState(GameManager gameManager, StateMachine<BaseGameState> stateMachine) : base(gameManager, stateMachine)
		{
		}
	}
}
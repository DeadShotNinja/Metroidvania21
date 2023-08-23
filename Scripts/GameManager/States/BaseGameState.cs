using UnityEngine;

namespace Metro
{
	public abstract class BaseGameState : BaseState<BaseGameState>
	{
		protected GameManager _gameManager;
		
		public BaseGameState(GameManager gameManager, StateMachine<BaseGameState> stateMachine) : base(stateMachine)
		{
			_gameManager = gameManager;
		}
	}
}
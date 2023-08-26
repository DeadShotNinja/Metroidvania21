using UnityEngine;

namespace Metro
{
	public abstract class BaseLevelState : BaseState<BaseLevelState>
	{
		protected LevelManager _levelManager;
		
		protected BaseLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(stateMachine)
		{
			_levelManager = levelManager;
		}
		
		public virtual void DrawGizmosWhenSelected()
		{
			
		}
	}
}
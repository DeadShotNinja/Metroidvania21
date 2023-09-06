using UnityEngine;

namespace Metro
{
	public interface IMovementStrategyFactory
	{
		public IMovementStrategy Create(MovementType type);
	}
}
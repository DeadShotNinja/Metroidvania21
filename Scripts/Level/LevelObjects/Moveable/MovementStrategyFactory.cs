using System;
using UnityEngine;

namespace Metro
{
	public enum MovementType { OneWay, Loop, PingPong }
	
	public class MovementStrategyFactory : IMovementStrategyFactory
	{
		public IMovementStrategy Create(MovementType type)
		{
			return type switch
			{
				MovementType.OneWay => new OneWayMovementStrategy(),
				MovementType.Loop => new LoopMovementStrategy(),
				MovementType.PingPong => new PingPongMovementStrategy(),
				_ => throw new ArgumentException("Invalid movement type: ", nameof(type))
			};
		}
	}
}
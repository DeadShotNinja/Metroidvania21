using UnityEngine;

namespace Metro
{
	public class LoopMovementStrategy : IMovementStrategy
	{
		public Vector2 CalculateMovement(Vector2 currentPosition, Vector2 startingPosition, float speed, float deltaTime,
			ref int currentWaypoint, ref bool forward, Vector2[] waypoints)
		{
			currentWaypoint %= waypoints.Length;
			Vector2 target = startingPosition + waypoints[currentWaypoint];
			Vector2 newPosition = Vector2.MoveTowards(currentPosition, target, speed * deltaTime);
			
			if (newPosition == target)
			{
				currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
			}

			return newPosition - currentPosition;
		}
	}
}
using UnityEngine;

namespace Metro
{
	public class OneWayMovementStrategy : IMovementStrategy
	{
		public Vector2 CalculateMovement(Vector2 currentPosition, Vector2 startingPosition, float speed, float deltaTime,
			ref int currentWaypoint, ref bool forward, Vector2[] waypoints)
		{
			if (currentWaypoint >= waypoints.Length)
			{
				return Vector2.zero;
			}
			
			Vector2 target = startingPosition + waypoints[currentWaypoint];
			Vector2 newPosition = Vector2.MoveTowards(currentPosition, target, speed * deltaTime);
			
			if (newPosition == target)
			{
				currentWaypoint++;
			}

			return newPosition - currentPosition;
		}
	}
}
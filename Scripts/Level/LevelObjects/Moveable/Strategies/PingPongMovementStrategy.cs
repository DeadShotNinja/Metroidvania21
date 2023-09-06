using UnityEngine;

namespace Metro
{
	public class PingPongMovementStrategy : IMovementStrategy
	{
		public Vector2 CalculateMovement(Vector2 currentPosition, Vector2 startingPosition, float speed, float deltaTime,
			ref int currentWaypoint, ref bool forward, Vector2[] waypoints)
		{
			if (forward)
			{
				if (currentWaypoint >= waypoints.Length)
				{
					forward = false;
					currentWaypoint = waypoints.Length - 2;
				}
			}
			else
			{
				if (currentWaypoint < 0)
				{
					forward = true;
					currentWaypoint = 1;
				}
			}
			
			Vector2 target = startingPosition + waypoints[currentWaypoint];
			Vector2 newPosition = Vector2.MoveTowards(currentPosition, target, speed * deltaTime);
		
			if (newPosition == target)
			{
				currentWaypoint += forward ? 1 : -1;
			}

			return newPosition - currentPosition;
		}
	}
}
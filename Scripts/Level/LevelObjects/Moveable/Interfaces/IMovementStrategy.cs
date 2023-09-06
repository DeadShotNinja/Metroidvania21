using UnityEngine;

namespace Metro
{
	public interface IMovementStrategy
	{
		Vector2 CalculateMovement(Vector2 currentPosition, Vector2 startingPosition, float speed, float deltaTime,
			ref int currentWaypoint, ref bool forward, Vector2[] waypoints);
	}
}
using UnityEngine;

namespace Metro
{
	public struct ObjectiveUpdatedEvent
	{
		public int ObjectiveID;
		
		public ObjectiveUpdatedEvent(int objectiveID)
		{
			ObjectiveID = objectiveID;
		}
	}
}
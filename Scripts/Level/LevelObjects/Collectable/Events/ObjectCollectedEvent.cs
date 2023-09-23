namespace Metro
{
	public struct ObjectCollectedEvent
	{
		public int CollectableID;
		
		public ObjectCollectedEvent(int collectableID)
		{
			CollectableID = collectableID;
		}
	}
}
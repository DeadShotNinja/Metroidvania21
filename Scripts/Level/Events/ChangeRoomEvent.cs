namespace Metro
{
	public struct ChangeRoomEvent
	{
		public int TargetRoomID;
		public int TargetSpawnID;

		public ChangeRoomEvent(int targetRoomID, int targetSpawnID)
		{
			TargetRoomID = targetRoomID;
			TargetSpawnID = targetSpawnID;
		}
	}
}
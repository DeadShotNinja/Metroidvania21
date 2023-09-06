namespace Metro
{
	public struct InteractableTriggerEvent
	{
		public int ListenerID;
		public bool ShouldActivate;
		
		public InteractableTriggerEvent(int listenerID, bool shouldActivate)
		{
			ListenerID = listenerID;
			ShouldActivate = shouldActivate;
		}
	}
}
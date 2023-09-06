namespace Metro
{
	public struct PlayerControlsEvent
	{
		public bool ControlsEnabled;
		
		public PlayerControlsEvent(bool isEnabled)
		{
			ControlsEnabled = isEnabled;
		}
	}
}
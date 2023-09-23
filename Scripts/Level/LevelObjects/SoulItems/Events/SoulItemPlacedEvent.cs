namespace Metro
{
	public class SoulItemPlacedEvent
	{
		public SoulItemDataSO SoulItemData; 
		
		public SoulItemPlacedEvent(SoulItemDataSO soulItemData)
		{
			SoulItemData = soulItemData;
		}
	}
}
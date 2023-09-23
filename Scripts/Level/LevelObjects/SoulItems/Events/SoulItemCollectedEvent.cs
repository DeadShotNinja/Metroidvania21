using UnityEngine;

namespace Metro
{
	public class SoulItemCollectedEvent
	{
		public SoulItemDataSO SoulItemData; 
		
		public SoulItemCollectedEvent(SoulItemDataSO soulItemData)
		{
			SoulItemData = soulItemData;
		}
	}
}
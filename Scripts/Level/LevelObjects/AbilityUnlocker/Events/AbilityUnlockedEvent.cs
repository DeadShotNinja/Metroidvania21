namespace Metro
{
	public struct AbilityUnlockedEvent
	{
		public AbilityType AbilityType;
		
		public AbilityUnlockedEvent(AbilityType abilityType)
		{
			AbilityType = abilityType;
		}
	}
}
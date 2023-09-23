using UnityEngine;

namespace Metro
{
	public class DialogEventHandler : MonoBehaviour
	{
		public void Event_UnlockDoubleJump()
		{
			EventManager.TriggerEvent(new AbilityUnlockedEvent(AbilityType.DoubleJump));
		}
		
		public void Event_UnlockDash()
		{
			EventManager.TriggerEvent(new AbilityUnlockedEvent(AbilityType.Dash));
		}
		
		public void Event_UnlockWallJump()
		{
			EventManager.TriggerEvent(new AbilityUnlockedEvent(AbilityType.WallJump));
		}
		
		public void Event_UnlockClimb()
		{
			EventManager.TriggerEvent(new AbilityUnlockedEvent(AbilityType.Climb));
		}
		
		public void Event_TurnInSoulItem(int soulItemIndex)
		{
			if (GameDatabase.Instance == null)
			{
				Debug.LogError("Database was null", this);
				return;
			}

			if (GameDatabase.Instance.TryGetSoulItem(soulItemIndex, out SoulItemDataSO soulItemData))
			{
                EventManager.TriggerEvent(new SoulItemPlacedEvent(soulItemData));
            }
			else
			{
                Debug.LogError($"Index {soulItemIndex} is not valid. Check GameDatabase component and make sure index exists!", this);
            }			
		}

		public void Event_BarnabasFadeStart()
		{
			EventManager.TriggerEvent(new BarnabasFadeEvent());
		}
	}
}
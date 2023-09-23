using System;
using UnityEngine;

namespace Metro
{
	public class AbilityUnlockTrigger : MonoBehaviour
	{
		[Header("Unlock Setup")]
		[SerializeField] private AbilityType _abilityToUnlock;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.TryGetComponent(out PlayerEntity player))
			{
				//player.AbilityManager.UnlockAbility(_abilityToUnlock);
				EventManager.TriggerEvent(new AbilityUnlockedEvent(_abilityToUnlock));
				Destroy(gameObject);
			}
		}
	}
}
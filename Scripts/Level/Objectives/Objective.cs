using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
	public class Objective : MonoBehaviour
	{
		[Header("Objective Setup")]
		[SerializeField] private ObjectiveType _objectiveType;
		[ShowIf(nameof(_objectiveType), ObjectiveType.SouldItem)]
		[SerializeField] private Sprite _soulItemSprite;
		[ShowIf(nameof(_objectiveType), ObjectiveType.Ability)]
		[SerializeField] private int _objectiveID;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.TryGetComponent(out PlayerEntity player))
			{
				EventManager.TriggerEvent(new ObjectiveUpdatedEvent(_objectiveID));
				Destroy(gameObject);
			}
		}
	}
}
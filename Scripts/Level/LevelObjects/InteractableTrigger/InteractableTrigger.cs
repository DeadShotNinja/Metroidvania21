using System;
using UnityEngine;

namespace Metro
{
	public class InteractableTrigger : MonoBehaviour
	{
		[Header("Setup")]
		[SerializeField] private int _targetListenerID;
		[SerializeField] private bool _requireInput;
		[SerializeField] private bool _oneTimeUse;

		private bool _wasTriggered;
		private bool _isActive;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!_requireInput && other.gameObject.TryGetComponent(out PlayerEntity player))
			{
				Interact();
			}
		}
		
		public void Interact()
		{
			if (_oneTimeUse && _wasTriggered) return;
			
			if (_isActive)
			{
				DeactivateListeners();
			}
			else
			{
				ActivateListeners();
			}
			
			_wasTriggered = true;
			_isActive = !_isActive;
		}
		
		private void ActivateListeners()
		{
			EventManager.TriggerEvent(new InteractableTriggerEvent(_targetListenerID, true));
		}
		
		private void DeactivateListeners()
		{
			EventManager.TriggerEvent(new InteractableTriggerEvent(_targetListenerID, false));
		}
	}
}
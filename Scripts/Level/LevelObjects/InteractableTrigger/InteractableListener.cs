using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
	public class InteractableListener : MonoBehaviour
	{
		private enum TriggerMode
		{
			Toggle,
			OnlyActivate,
			OnlyDeactivate
		}
		
		[Header("Setup")]
		[Tooltip("The unique ID for this listener. Must match with the ID set in the trigger.")]
		[SerializeField] private int _listenerID;
		[Tooltip("The mode that determines how this listener responds to triggers.")]
		[SerializeField] private TriggerMode _triggerMode = TriggerMode.Toggle;
		[Tooltip("Whether multiple triggers are required to activate or deactivate.")]
		[SerializeField] private bool _requireMultipleTriggers;
		[ShowIf(nameof(_requireMultipleTriggers))]
		[Tooltip("The number of triggers required to activate or deactivate.")]
		[SerializeField] private int _requiredTriggers = 1;
		
		[Header("Debugging")]
		[Tooltip("The current count of triggers that have been activated or deactivated.")]
		[SerializeField, ReadOnly] private int _triggerCount;
		
		private IInteractableTarget[] _targets;

		private void Awake()
		{
			Initialize();
		}
		
		private void Initialize()
		{
			_targets = GetComponents<IInteractableTarget>();
			_requiredTriggers = _requireMultipleTriggers ? _requiredTriggers : 1;
		}

		private void OnEnable()
		{
			EventManager.StartListening<InteractableTriggerEvent>(OnInteractableTriggered);
		}
		
		private void OnInteractableTriggered(InteractableTriggerEvent eventData)
		{
			if (eventData.ListenerID != _listenerID) return;

			foreach (IInteractableTarget target in _targets)
			{
				HandleTrigger(target, eventData.ShouldActivate);
			}
		}
		
		private void HandleTrigger(IInteractableTarget target, bool shouldActivate)
		{
			switch (_triggerMode)
			{
				case TriggerMode.Toggle:
					ToggleTrigger(target, shouldActivate);
					break;
				case TriggerMode.OnlyActivate:
					ActivateTrigger(target);
					break;
				case TriggerMode.OnlyDeactivate:
					DeactivateTrigger(target);
					break;
			}
		}
		
		private void ToggleTrigger(IInteractableTarget target, bool shouldActivate)
		{
			if (shouldActivate)
			{
				_triggerCount++;
				if (_triggerCount >= _requiredTriggers) target.Activate();
			}
			else
			{
				_triggerCount--;
				if (_triggerCount < 0) _triggerCount = 0;
				if (_triggerCount < _requiredTriggers) target.Deactivate();
			}
		}
		
		private void ActivateTrigger(IInteractableTarget target)
		{
			target.Activate();
		}
		
		private void DeactivateTrigger(IInteractableTarget target)
		{
			target.Deactivate();
		}

		private void OnDestroy()
		{
			EventManager.StopListening<InteractableTriggerEvent>(OnInteractableTriggered);
		}
	}
}
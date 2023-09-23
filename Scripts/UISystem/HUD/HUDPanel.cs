using System;
using UnityEngine;
using UnityEngine.UI;

namespace Metro
{
	public class HUDPanel : MonoBehaviour
	{
		private SoulItemHUD _soulItemHUD;
		
		private void Awake()
		{
			_soulItemHUD = GetComponentInChildren<SoulItemHUD>();
		}

		private void OnEnable()
		{
			EventManager.StartListening<SoulItemCollectedEvent>(OnSoulItemCollected);
			EventManager.StartListening<SoulItemPlacedEvent>(OnSoulItemPlaced);
		}

		private void OnSoulItemCollected(SoulItemCollectedEvent eventData)
		{
			_soulItemHUD.SetFirstAvailableImage(eventData.SoulItemData);
		}
		
		private void OnSoulItemPlaced(SoulItemPlacedEvent eventData)
		{
			_soulItemHUD.ClearImage(eventData.SoulItemData.ItemID);
		}

		private void OnDestroy()
		{
			EventManager.StopListening<SoulItemCollectedEvent>(OnSoulItemCollected);
			EventManager.StopListening<SoulItemPlacedEvent>(OnSoulItemPlaced);
		}
	}
}
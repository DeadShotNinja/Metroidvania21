using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Metro
{
	public class SoulItemHUD : MonoBehaviour
	{
		private Image[] _soulItemImages;
		private Dictionary<int, Image> _soulItemDictionary = new Dictionary<int, Image>();
		
		private void Awake()
		{
			Initialize();
		}
		
		private void Initialize()
		{
			_soulItemImages = GetComponentsInChildren<Image>(true);
			if (_soulItemImages.Length < 5)
			{
				Debug.LogError("Missing Soul Item Image Prefabs", this);
			}
		}
		
		public void SetFirstAvailableImage(SoulItemDataSO itemData)
		{
			foreach (Image image in _soulItemImages)
			{
				if (image.sprite == null)
				{
					image.sprite = itemData.ItemSprite;
					image.gameObject.SetActive(true);
					_soulItemDictionary.Add(itemData.ItemID, image);
					return;
				}
			}
		}
		
		public void ClearImage(int itemID)
		{
			if (_soulItemDictionary.ContainsKey(itemID))
			{
				_soulItemDictionary[itemID].sprite = null;
				_soulItemDictionary[itemID].gameObject.SetActive(false);
				_soulItemDictionary.Remove(itemID);
			}
			else
			{
				Debug.LogWarning($"Item with ID {itemID} not found in SoulItemDictionary", this);
			}
		}
	}
}
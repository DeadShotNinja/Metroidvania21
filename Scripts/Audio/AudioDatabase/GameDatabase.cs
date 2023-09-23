using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Metro
{
	public class GameDatabase : Singleton<GameDatabase>
	{
        [Header("Soulitems")]
        [Tooltip("List of soulitems in the game. (Note: Must be in correct order!")]
        [SerializeField] private SoulItemDataSO[] _soulItems;

		[Header("Audio")]
		[SerializeField] private EntityAudioCollectionSO _entityAudioCollection;
        [SerializeField] private AmbienceAudioCollectionSO _ambienceAudioCollection;
        [SerializeField] private EnvironmentAudioCollectionSO _environmentAudioCollection;
        [SerializeField] private ItemAudioCollectionSO _itemAudioCollection;
        [SerializeField] private UIAudioCollectionSO _uiAudioCollection;
        [SerializeField] private EnemyAudioCollectionSO _enemyAudioCollection;

        #region Items

        public bool TryGetSoulItem(int itemID, out SoulItemDataSO soulItemData)
        {
            int index = itemID - 1;

            if (index >= 0 && index < _soulItems.Length)
            {
                soulItemData = _soulItems[index];
                return true;
            }

            soulItemData = null;
            return false;
        }

        #endregion

        #region Audio

        // Returns Wwise Event in specified Collection.
        public AK.Wwise.Event GetEntityAudioEvent(EntityAudioType audioType)
		{
			foreach (var audioData in _entityAudioCollection.EntityAudio)
			{
				if (audioData.AudioType == audioType)
				{
					return audioData.AudioEvent;
				}
			}

			Debug.LogError($"Audio Event for {audioType} not found!");
			return null;
		}
        // Returns Wwise Event in specified Collection.
        public AK.Wwise.Event GetAmbienceAudioEvent(AmbienceAudioType audioType)
        {
            foreach (var audioData in _ambienceAudioCollection.AmbienceAudio)
            {
                if (audioData.AudioType == audioType)
                {
                    return audioData.AudioEvent;
                }
            }

            Debug.LogError($"Audio Event for {audioType} not found!");
            return null;
        }
        // Returns Wwise Event in specified Collection.
        public AK.Wwise.Event GetEnvironmentAudioEvent(EnvironmentAudioType audioType)
        {
            foreach (var audioData in _environmentAudioCollection.EnvironemntAudio)
            {
                if (audioData.AudioType == audioType)
                {
                    return audioData.AudioEvent;
                }
            }

            Debug.LogError($"Audio Event for {audioType} not found!");
            return null;
        }
        // Returns Wwise Event in specified Collection.
        public AK.Wwise.Event GetItemAudioEvent(ItemAudioType audioType)
        {
            foreach (var audioData in _itemAudioCollection.ItemAudio)
            {
                if (audioData.AudioType == audioType)
                {
                    return audioData.AudioEvent;
                }
            }

            Debug.LogError($"Audio Event for {audioType} not found!");
            return null;
        }
        // Returns Wwise Event in specified Collection.
        public AK.Wwise.Event GetUIAudioEvent(UIAudioType audioType)
        {
            foreach (var audioData in _uiAudioCollection.UIAudio)
            {
                if (audioData.AudioType == audioType)
                {
                    return audioData.AudioEvent;
                }
            }

            Debug.LogError($"Audio Event for {audioType} not found!");
            return null;
        }
        // Returns Wwise Event in specified Collection.
        public AK.Wwise.Event GetEnemyAudioEvent(EnemyAudioType audioType)
        {
            foreach (var audioData in _enemyAudioCollection.EnemyAudio)
            {
                if (audioData.AudioType == audioType)
                {
                    return audioData.AudioEvent;
                }
            }

            Debug.LogError($"Audio Event for {audioType} not found!");
            return null;
        }
        #endregion
    }
}
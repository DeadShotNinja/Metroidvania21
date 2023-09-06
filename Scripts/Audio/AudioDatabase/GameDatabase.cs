using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	public class GameDatabase : Singleton<GameDatabase>
	{
		[Header("Entity Audio Events")]
		[SerializeField] private EntityAudioCollectionSO _entityAudioCollection;
		
		//[Header("Level Events")]
		//[SerializeField] private List<AK.Wwise.Event> _levelAudio = new List<AK.Wwise.Event>();
		
		public AK.Wwise.Event GetAudioEvent(EntityAudioType audioType)
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
	}
}
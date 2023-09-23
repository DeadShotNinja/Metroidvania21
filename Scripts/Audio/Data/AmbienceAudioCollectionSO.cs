using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
    // Creates Scriptable Object for Ambience Wwise Events to be defined and held. Reusable Tool for adding in sounds that can be hot-swapped.
    [CreateAssetMenu(menuName = "Database/Audio/Collections/Ambience", fileName = "New Ambience Collection")]
	public class AmbienceAudioCollectionSO : ScriptableObject
	{
		[Header("Ambience Audio Collection")]
		[SerializeField] private List<AudioData<AmbienceAudioType>> _ambienceAudio = new List<AudioData<AmbienceAudioType>>();
		
		public List<AudioData<AmbienceAudioType>> AmbienceAudio => _ambienceAudio;
	}
}
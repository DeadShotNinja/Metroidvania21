using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	// Creates Scriptable Object for UI Wwise Events to be defined and held. Reusable Tool for adding in sounds that can be hot-swapped.
	[CreateAssetMenu(menuName = "Database/Audio/Collections/UI", fileName = "New UI Collection")]
	public class UIAudioCollectionSO : ScriptableObject
	{
		[Header("UI Audio Collection")]
		[SerializeField] private List<AudioData<UIAudioType>> _uIAudio = new List<AudioData<UIAudioType>>();
		
		public List<AudioData<UIAudioType>> UIAudio => _uIAudio;
	}
}
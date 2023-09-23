using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	// Creates Scriptable Object for Item Wwise Events to be defined and held. Reusable Tool for adding in sounds that can be hot-swapped.
	[CreateAssetMenu(menuName = "Database/Audio/Collections/Item", fileName = "New Item Collection")]
	public class ItemAudioCollectionSO : ScriptableObject
	{
		[Header("Item Audio Collection")]
		[SerializeField] private List<AudioData<ItemAudioType>> _itemAudio = new List<AudioData<ItemAudioType>>();
		
		public List<AudioData<ItemAudioType>> ItemAudio => _itemAudio;
	}
}
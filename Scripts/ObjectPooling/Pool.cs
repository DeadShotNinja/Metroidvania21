using UnityEngine;

namespace Metro
{
	/// <summary>
	/// Obect used to define the type of pool prefab and the amount of objects to pool of that type at the start of the game.
	/// </summary>
	[System.Serializable]
	public class Pool
	{
		[Tooltip("The prefab that will be used to spawn the specific GameObject to pool")]
		[SerializeField] private GameObject m_Prefab;
		[Tooltip("Starting size of the pool to help performance.")]
		[SerializeField] private int m_InitialSize;

		public GameObject Prefab => m_Prefab;
		public int InitialSize => m_InitialSize;
	}
}
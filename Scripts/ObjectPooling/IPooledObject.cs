using UnityEngine;

namespace Metro
{
	/// <summary>
	/// Interface to maintain the OriginalPrefab of the specific GO used in the pool system.
	/// </summary>
	public interface IPooledObject
	{
		public GameObject OriginalPrefab { get; set; }
		public void ResetPooledObject();
	}
}
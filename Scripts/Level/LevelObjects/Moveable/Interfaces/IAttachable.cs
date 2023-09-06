using UnityEngine;

namespace Metro
{
	/// <summary>
	/// This interface is used to mark objects that can be attached to a MovableProp.
	/// </summary>
	public interface IAttachable
	{
		public bool IsAttached { get; set; }
		public void AddToTransformPosition(Vector2 positionOffset);
	}
}
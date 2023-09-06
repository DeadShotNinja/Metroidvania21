using UnityEngine;

namespace Metro
{
	public interface IInteractableTarget
	{
		public void Activate();
		public void Deactivate();
	}
}
using UnityEngine;

namespace Metro
{
	public interface IAttachableHandler
	{
		public void HandleAttach(IAttachable attachable);
		public void HandleDetach(IAttachable attachable);
	}
}
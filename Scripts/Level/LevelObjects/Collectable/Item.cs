using System;
using UnityEngine;

namespace Metro
{
	public class Item : MonoBehaviour
	{
		[Header("General Info")]
		[SerializeField] private string _itemName;
		[SerializeField, TextArea] private string _itemDescription;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.TryGetComponent(out PlayerEntity player))
			{
				
			}
		}
	}
}
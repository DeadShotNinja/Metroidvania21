using UnityEngine;

namespace Metro
{
	[CreateAssetMenu(menuName = "Items/SoulItem", fileName = "New Soul Item")]
	public class SoulItemDataSO : ScriptableObject
	{
		[Header("Setup")]
		[SerializeField] private int _itemID;
		[SerializeField] private string _itemName;
		[SerializeField] private Sprite _itemSprite;

		public int ItemID => _itemID;
		public string ItemName => _itemName;
		public Sprite ItemSprite => _itemSprite;
	}
}
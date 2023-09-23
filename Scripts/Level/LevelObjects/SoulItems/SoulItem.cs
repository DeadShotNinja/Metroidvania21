using UnityEngine;

namespace Metro
{
	public class SoulItem : MonoBehaviour
	{
		[Header("Setup")]
		[SerializeField] private int _soulItemIndex;

		private SpriteRenderer _spriteRenderer;
		private SoulItemDataSO _soulItemData;

        AK.Wwise.Event stopAudioEvent = null;

        private void Awake()
        {
            Initialize();
        }        

        private void Initialize()
		{
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            if (GameDatabase.Instance == null)
            {
                Debug.LogError("No GameDatase instance found, Soul Item will not work", this);
                return;
            }

            if (GameDatabase.Instance.TryGetSoulItem(_soulItemIndex, out SoulItemDataSO soulItemData))
            {
                _soulItemData = soulItemData;
                if (_spriteRenderer != null) _spriteRenderer.sprite = _soulItemData.ItemSprite;
                else Debug.LogError("Model Image not found.", this);
            }
            else
            {
                Debug.LogError($"Index {_soulItemIndex} is not valid. Check GameDatabase component and make sure index exists!");
            }
        }

        private void OnEnable()
        {
            StartAudioLoop();            
        }

        private void StartAudioLoop()
        {
            if (GameDatabase.Instance == null || _soulItemData  == null) return;

            GameDatabase inst = GameDatabase.Instance;
            AK.Wwise.Event audioEvent = null;

            switch (_soulItemData.ItemID)
            {
                case 1:
                    audioEvent = inst.GetItemAudioEvent(ItemAudioType.Play_SoulboundRecipeBookLoop);
                    stopAudioEvent = inst.GetItemAudioEvent(ItemAudioType.Stop_SoulboundRecipeBookLoop);
                    break;
                case 2:
                    audioEvent = inst.GetItemAudioEvent(ItemAudioType.Play_SoulboundCapfeather_Loop);
                    stopAudioEvent = inst.GetItemAudioEvent(ItemAudioType.Stop_SoulboundCapfeather_Loop);
                    break;
                case 3:
                    audioEvent = inst.GetItemAudioEvent(ItemAudioType.Play_SoulboundCompass_Loop);
                    stopAudioEvent = inst.GetItemAudioEvent(ItemAudioType.Stop_SoulboundCompass_Loop);
                    break;
                case 4:
                    audioEvent = inst.GetItemAudioEvent(ItemAudioType.Play_SoulboundGun_Loop);
                    stopAudioEvent = inst.GetItemAudioEvent(ItemAudioType.Stop_SoulboundGun_Loop);
                    break;
                case 5:
                    audioEvent = inst.GetItemAudioEvent(ItemAudioType.Play_SoulboundLocketLoop);
                    stopAudioEvent = inst.GetItemAudioEvent(ItemAudioType.Stop_SoulboundLocketLoop);
                    break;
                default:
                    Debug.LogWarning("Soulbound index does not exist. sound will not play.");
                    break;
            }

            audioEvent?.Post(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.TryGetComponent(out PlayerEntity player))
			{
                if (_soulItemData == null) { return; }

                if (GameDatabase.Instance != null) 
                    GameDatabase.Instance.GetItemAudioEvent(ItemAudioType.Play_SoulboundItemPickup)?.Post(gameObject);                

                EventManager.TriggerEvent(new SoulItemCollectedEvent(_soulItemData));
                Destroy(gameObject);	
			}
		}

        private void OnDisable()
        {
            stopAudioEvent?.Post(gameObject);
        }
    }
}
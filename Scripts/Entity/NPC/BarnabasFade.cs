using AllIn1SpriteShader;
using DG.Tweening;
using UnityEngine;

namespace Metro
{
    [RequireComponent(typeof(Collider2D))]
    public class BarnabasFade : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private float _floatSpeed = 0.5f;
        [SerializeField] private float _floatDistance = 5f;

        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void OnEnable()
        {
            EventManager.StartListening<BarnabasFadeEvent>(OnBarnabasFade);
        }

        private void OnBarnabasFade(BarnabasFadeEvent eventData)
        {
            EventManager.TriggerEvent(new PlayerControlsEvent(false));

            _collider.enabled = false;

            Vector3 endPosition = transform.position + new Vector3(0f, _floatDistance, 0f);
            transform.DOMove(endPosition, _floatDistance / _floatSpeed).SetEase(Ease.OutSine);

            if (_spriteRenderer == null) return;

            DOTween.To(
                () => _spriteRenderer.material.GetFloat("_FadeAmount"),
                x => _spriteRenderer.material.SetFloat("_FadeAmount", x),
                1f,
                _fadeDuration
                ).OnComplete(() => Destroy(gameObject));
        }

        private void OnDisable()
        {
            EventManager.StopListening<BarnabasFadeEvent>(OnBarnabasFade);
        }

        private void OnDestroy()
        {
            EventManager.TriggerEvent(new WinGameEvent());
        }
    }
}

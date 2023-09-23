using DG.Tweening;
using PixelCrushers;
using UnityEngine;
using UnityEngine.UI;

namespace Metro
{
    [RequireComponent(typeof(Image))]
    public class TimeIndicator : MonoBehaviour
    {
        [Header("Pendants")]
        [SerializeField] private Sprite _pastPendant;
        [SerializeField] private Sprite _presentPendant;

        private Image _pendantImage;
        private Tween _flipTween;

        private void Awake()
        {
            _pendantImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            EventManager.StartListening<TimeChangedEvent>(OnTimeFrameChanged);
        }

        public void OnTimeFrameChanged(TimeChangedEvent eventData)
        {
            _flipTween?.Kill();

            if (_pendantImage.rectTransform.eulerAngles.x == 0f)
            {
                FlipPendantTo90(eventData.SwapDuration);
            }
            else
            {
                SwapPendantSprite(eventData.IsPresent);
                FlipPendantTo0(eventData.SwapDuration);
            }
        }

        private void FlipPendantTo90(float duration)
        {
            _flipTween = _pendantImage.rectTransform.DORotate(new Vector3(90f, 0f, 0f), duration);
        }

        private void SwapPendantSprite(bool isPresent)
        {
            _pendantImage.sprite = isPresent ? _presentPendant : _pastPendant;
        }

        private void FlipPendantTo0(float duration)
        {
            _flipTween = _pendantImage.rectTransform.DORotate(new Vector3(0f, 0f, 0f), duration);
        }

        private void OnDestroy()
        {
            EventManager.StopListening<TimeChangedEvent>(OnTimeFrameChanged);
        }
    }
}

using UnityEngine;
using TMPro;
using PixelCrushers.DialogueSystem;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class NPCEntity : BaseEntity
    {
        public TMP_Text AIStateText;

        private AnimatorLocator _animLocator;
        
        protected override void Start()
        {
            base.Start();
            
            // TODO: Better way to set this.
            InputProvider = GetComponent<AIBrain>();

            AnimatorLocator animLocator = GetComponentInChildren<AnimatorLocator>();
            if (animLocator == null)
            {
                Debug.LogError("No AnimatorLocator found in children of " + gameObject.name +
                               ". Animations will not work.");
            }
            else
            {
                _animLocator = animLocator;
            }
        }

        protected override void Update()
        {
            base.Update();

            if (_animLocator == null) return;

            if (InputProvider.MoveInput.x > 0f && _animLocator.transform.localScale.x < 0f)
            {
                Vector3 scale = _animLocator.transform.localScale;
                scale.x *= -1f;
                _animLocator.transform.localScale = scale;
            }
            else if (InputProvider.MoveInput.x < 0f && _animLocator.transform.localScale.x > 0f)
            {
                Vector3 scale = _animLocator.transform.localScale;
                scale.x *= -1f;
                _animLocator.transform.localScale = scale;
            }
        }
    }
}
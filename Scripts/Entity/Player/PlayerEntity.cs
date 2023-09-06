using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerEntity : BaseEntity
    {
        [BoxGroup("Respawn")]
        [Tooltip("The speed at which the screen will fade in/out.")]
        [SerializeField] private float _fadeSpeed;
        
        protected override void Start()
        {
            base.Start();

            // TODO: This should probably be provided through instantiation
            InputProvider = GameManager.Instance.PlayerInputHandler;
        }
        
        public override void TakeDamage()
        {
            base.TakeDamage();
            
            EventManager.TriggerEvent(new PlayerDiedEvent());
            EventManager.TriggerEvent(new ScreenFadeEvent(ScreenFadeType.In, _fadeSpeed));
        }
        
        protected override IEnumerator Respawn_Coroutine(Vector3 respawnPosition)
        {
            yield return base.Respawn_Coroutine(respawnPosition);
            
            EventManager.TriggerEvent(new ScreenFadeEvent(ScreenFadeType.Out, _fadeSpeed));
            EventManager.TriggerEvent(new PlayerRespawnedEvent());
        }
    }
}
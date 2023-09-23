using System;
using System.Collections;
using QFSW.QC;
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
        
        public AbilityManager AbilityManager { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            EventManager.StartListening<AbilityUnlockedEvent>(OnUnlockAbility);
        }

        protected override void Start()
        {
            base.Start();

            AbilityManager = new AbilityManager(this);
            
            // TODO: This should probably be provided through instantiation
            InputProvider = GameManager.Instance.PlayerInputHandler;
        }
        
        public override void TakeDamage()
        {
            if (IsInvulerable) { return; }

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

        private void OnUnlockAbility(AbilityUnlockedEvent eventData)
        {
            AbilityManager.UnlockAbility(eventData.AbilityType);
        }
        
        [Command("unlock-all", "Unlocks all abilities", MonoTargetType.All)]
        private void UnlockAllAbilities()
        {
            AbilityManager.UnlockAbility(AbilityType.Dash);
            AbilityManager.UnlockAbility(AbilityType.DoubleJump);
            AbilityManager.UnlockAbility(AbilityType.Climb);
            AbilityManager.UnlockAbility(AbilityType.WallJump);
        }

        [Command("god-mode", "Prevents damage", MonoTargetType.All)]
        private void SetInvulnerable(bool isInvulnerable)
        {
            IsInvulerable = isInvulnerable;

            //BoxCollider2D collider;
            //if (EntityCollider is BoxCollider2D)
            //{
            //    collider = (BoxCollider2D)EntityCollider;
            //}
            //else { return; }

            //int currentMask = collider.GetLayerCollisionMask()

            int hazardLayerMask = 1 << LayerMask.NameToLayer("Hazard");
            int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");

            if (IsInvulerable)
            {
                EntityCollider.excludeLayers = hazardLayerMask | enemyLayerMask;
            }
            else
            {
                EntityCollider.excludeLayers = 0;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            EventManager.StopListening<AbilityUnlockedEvent>(OnUnlockAbility);
        }
    }
}
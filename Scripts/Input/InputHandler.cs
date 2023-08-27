using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// Handles receiving input from input system and feeding it to the required classes.
    /// </summary>
    public class InputHandler : MonoBehaviour, IInputProvider
    {
        private PlayerInputActions _playerInputActions;

        #region Player Inputs

        // Vector2 Values
        public Vector2 MoveInput { get; set; }
        
        // Buttons
        public InputState JumpInput { get; set; }
        public InputState DashInput { get; set; }

        #endregion

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();

            JumpInput = new InputState(_playerInputActions.Player.Jump);
            DashInput = new InputState(_playerInputActions.Player.Dash);
        }

        private void OnEnable()
        {
            _playerInputActions.Player.Enable();
            EventManager.StartListening<PlayerDiedEvent>(OnPlayerDied);
            EventManager.StartListening<PlayerRespawnedEvent>(OnPlayerRespawned);
        }

        private void Update()
        {
            MoveInput = _playerInputActions.Player.Move.ReadValue<Vector2>();
        }

        private void LateUpdate()
        {
            JumpInput.Reset();
            DashInput.Reset();
        }
        
        private void OnPlayerDied(PlayerDiedEvent eventData)
        {
            _playerInputActions.Disable();
        }
        
        private void OnPlayerRespawned(PlayerRespawnedEvent eventData)
        {
            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
            EventManager.StopListening<PlayerDiedEvent>(OnPlayerDied);
            EventManager.StopListening<PlayerRespawnedEvent>(OnPlayerRespawned);
        }
    }
}
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
        public InputState TimeSwapInput { get; set; }
        public InputState InteractInput { get; set; }

        #endregion

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();

            JumpInput = new InputState(_playerInputActions.Player.Jump);
            DashInput = new InputState(_playerInputActions.Player.Dash);
            TimeSwapInput = new InputState(_playerInputActions.Player.TimeSwap);
            InteractInput = new InputState(_playerInputActions.Player.Interact);
        }

        private void OnEnable()
        {
            _playerInputActions.Player.Enable();
            EventManager.StartListening<PlayerDiedEvent>(OnPlayerDied);
            EventManager.StartListening<PlayerRespawnedEvent>(OnPlayerRespawned);
            
            // TODO: Other events proably should change to this one
            EventManager.StartListening<PlayerControlsEvent>(OnPlayerControlsToggled);
        }

        private void Update()
        {
            MoveInput = _playerInputActions.Player.Move.ReadValue<Vector2>();
        }

        private void LateUpdate()
        {
            JumpInput.Reset();
            DashInput.Reset();
            TimeSwapInput.Reset();
            InteractInput.Reset();
        }

        public void ChangeControllType(bool isPauseMenu)
        {
            if (isPauseMenu)
            {
                _playerInputActions.Player.Disable();
                _playerInputActions.UI.Enable();
            }
            else
            {
                _playerInputActions.Player.Enable();
                _playerInputActions.UI.Disable();
            }
        }

        private void OnPlayerControlsToggled(PlayerControlsEvent eventData)
        {
            if (eventData.ControlsEnabled)
            {
                _playerInputActions.Player.Enable();
            }
            else
            {
                _playerInputActions.Player.Disable();
            }
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
            
            // TODO: Other events proably should change to this one
            EventManager.StopListening<PlayerControlsEvent>(OnPlayerControlsToggled);
        }
    }
}
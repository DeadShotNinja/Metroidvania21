using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
    public enum TimeState
    {
        Present,
        Pest
    }

    /// <summary>
    /// Manages overall game and game states.
    /// </summary>
    [RequireComponent(typeof(InputHandler))]
    public class GameManager : PersistantSingleton<GameManager>
    {
        public InputHandler PlayerInputHandler { get; private set; }
        public TimeState CurrentTimeState { get; set; } = TimeState.Present;

        public StateMachine<BaseGameState> GameStateMachine { get; private set; }
        
        #region Game States

        public InitializeGameState InitializeGameState { get; private set; }
        public PlayGameState PlayGameState { get; private set; }
        public PauseGameState PauseGameState { get; private set; }
        public WinGameState WinGameState { get; private set; }
        public LoseGameState LoseGameState { get; private set; }

        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            
            //if (_playerInScene) TryFindPlayer();
            PlayerInputHandler = GetComponent<InputHandler>();

            GameStateMachine = new StateMachine<BaseGameState>();

            InitializeGameState = new InitializeGameState(this, GameStateMachine);
            PlayGameState = new PlayGameState(this, GameStateMachine);
            PauseGameState = new PauseGameState(this, GameStateMachine);
            WinGameState = new WinGameState(this, GameStateMachine);
            LoseGameState = new LoseGameState(this, GameStateMachine);
            
            GameStateMachine.Initialize(InitializeGameState);
        }

        private void Update()
        {
            // TODO: this will get moved to player state (maybe ability or special ability?)
            if (Input.GetKeyDown(KeyCode.E))
            {
                EventManager.TriggerEvent(new ChangePeriodEvent());
            }
            
            GameStateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            GameStateMachine.CurrentState.PhysicsUpdate();
        }
        
        protected override void OnApplicationQuit()
        {
            EventManager.Reset();
            
            base.OnApplicationQuit();
        }
    }
}
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// Manages overall game functionalities (input, saves, settings, persistant data, etc)
    /// </summary>
    [RequireComponent(typeof(InputHandler))]
    public class GameManager : Singleton<GameManager>
    {
        [Header("Game Dependencies")]
        [SerializeField] private GameObject _consoleGO;
        
        private GameState _currentState;

        public InputHandler PlayerInputHandler { get; private set; }        
        
        protected override void Awake()
        {
            base.Awake();
            
            PlayerInputHandler = GetComponent<InputHandler>();
            _currentState = GameState.Playing;
            InitializeConsole();
        }
        
        private void InitializeConsole()
        {
            if (_consoleGO != null)
            {
                Instantiate(_consoleGO);
            }
        }

        private void Update()
        {
            // TODO: this should move to the responsability of proper input.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_currentState == GameState.Playing)
                {
                    ChangeGameState(GameState.Paused);
                }
                else
                {
                    ChangeGameState(GameState.Playing);
                }
            }
        }

        public void ChangeGameState(GameState newState)
        {
            _currentState = newState;

            switch (newState)
            {
                case GameState.Playing:
                    Time.timeScale = 1f;
                    PlayerInputHandler.ChangeControllType(false);
                    EventManager.TriggerEvent(new GameStateChangedEvent(GameState.Playing));
                    break;
                case GameState.Paused:
                    PlayerInputHandler.ChangeControllType(true);
                    EventManager.TriggerEvent(new GameStateChangedEvent(GameState.Paused));
                    Time.timeScale = 0f;
                    break;
            }
        }

        protected override void OnApplicationQuit()
        {
            EventManager.Reset();
            
            base.OnApplicationQuit();
        }
    }
}
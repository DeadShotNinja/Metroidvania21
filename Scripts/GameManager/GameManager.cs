using UnityEngine;

namespace Metro
{
    /// <summary>
    /// Manages overall game functionalities (input, saves, settings, persistant data, etc)
    /// </summary>
    [RequireComponent(typeof(InputHandler))]
    public class GameManager : PersistantSingleton<GameManager>
    {
        public InputHandler PlayerInputHandler { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            PlayerInputHandler = GetComponent<InputHandler>();
        }

        // private void Update()
        // {
        //     // TODO: this will get moved to player state (maybe ability or special ability?)
        //     // if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(1))
        //     // {
        //     //     EventManager.TriggerEvent(new RequestPeriodChangeEvent());
        //     // }
        // }
        
        protected override void OnApplicationQuit()
        {
            EventManager.Reset();
            
            base.OnApplicationQuit();
        }
    }
}
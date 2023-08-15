using UnityEngine;

namespace Metro
{
    /// <summary>
    /// Manages game and game states.
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
    }
}
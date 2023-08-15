using UnityEngine.InputSystem;

namespace Metro
{
    /// <summary>
    /// Object to create different input states for buttons (Pressed, Held, Released)
    /// and register them to an InputAction listener.
    /// </summary>
    public class InputState
    {
        private bool _pressed;
        private bool _held;
        private bool _released;

        #region Properties

        public bool Pressed => _pressed;
        public bool Held => _held;
        public bool Released => _released;

        #endregion

        public InputState(InputAction inputAction)
        {
            inputAction.started += ctx => OnStarted();
            inputAction.performed += ctx => OnPerformed();
            inputAction.canceled += ctx => OnCanceled();
        }

        public InputState() { }

        public void OnStarted() => _held = true;
        public void OnPerformed() => _pressed = true;
        public void OnCanceled()
        {
            _held = false;
            _released = true;
        }

        public void Reset()
        {
            _pressed = false;
            _released = false;
        }
    }
}
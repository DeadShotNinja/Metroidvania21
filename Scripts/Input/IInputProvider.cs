using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInputProvider
    {
        public Vector2 MoveInput { get; set; }
        public InputState JumpInput { get; set; }
        public InputState DashInput { get; set; }
        public InputState TimeSwapInput { get; set; }
        public InputState InteractInput { get; set; }
    }
}
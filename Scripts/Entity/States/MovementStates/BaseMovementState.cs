using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseMovementState : BaseState<BaseMovementState>
    {
        protected BaseEntity _entity;
        protected EntityHorizontalMove _horizontalMove;
        protected EntityJump _jump;
        protected EntityWallSlide _wallSlide;
        protected EntityDash _dash;

        protected BaseMovementState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(stateMachine)
        {
            _entity = entity;
            foreach (EntityComponent component in _entity.EntityComponents)
            {
                if (component is EntityHorizontalMove move)
                {
                    _horizontalMove = move;
                }
                else if (component is EntityJump jump)
                {
                    _jump = jump;
                }
                else if (component is EntityWallSlide wallSlide)
                {
                    _wallSlide = wallSlide;
                }
                else if (component is EntityDash dash)
                {
                    _dash = dash;
                }
            }
        }
    }
}
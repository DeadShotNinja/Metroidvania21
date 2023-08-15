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
        //protected EntityDash _dash;
        protected EntityWallSlide _wallSlide;

        protected float _stateEvalBuffer = 0.05f;
        
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
                // else if (component is EntityDash dash)
                // {
                //     _dash = dash;
                // }
                else if (component is EntityWallSlide wallSlide)
                {
                    _wallSlide = wallSlide;
                }
            }
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // if (_jump != null && _entity.InputProvider.JumpInput.Pressed)
            // {
            //     _entity.MovementStateMachine.ChangeState(_entity.JumpAirborneState);
            //     return;
            // }
            
            // Check for dash
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
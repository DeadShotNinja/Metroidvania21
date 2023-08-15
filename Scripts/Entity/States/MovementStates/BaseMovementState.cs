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

        private bool _jumpTriggered = false;
        
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
            }
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (_jump != null)
            {
                if (_entity.InputProvider.JumpInput.Pressed)
                {
                    _jumpTriggered = true;
                }
                else if (_entity.InputProvider.JumpInput.Released)
                {
                    _jump.JumpReleased();
                }
            }
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            if (_jumpTriggered)
            {
                _jump.TryJump();
                _jumpTriggered = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
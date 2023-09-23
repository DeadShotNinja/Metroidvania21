using Cinemachine;
using MoreMountains.Feedbacks;

namespace Metro
{
    /// <summary>
    /// Moving state while on the ground.
    /// </summary>
    public class MoveGroundedState : SuperGroundedState
    {
        private CinemachineBrain _camBrain;
        
        public MoveGroundedState(BaseEntity entity, StateMachine<BaseMovementState> stateMachine) : base(entity, stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            
            _entity.StateText.SetText("MOVING");

            _camBrain = LevelManager.Instance.PlayerCamera.GetComponent<CinemachineBrain>();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (ShouldSwitchToIdle())
            {
                _entity.MovementStateMachine.ChangeState(_entity.IdleGroundedState);
                return;
            }
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
            UpdateCameraIfPlayer();
            
            if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetFloat(_entity.AnimatorData.XInputFloat, 
                _entity.InputProvider.MoveInput.x);
            _horizontalMove.ApplyMovement(_entity.InputProvider.MoveInput.x);
        }

        public override void Exit()
        {
            base.Exit();
            
            UpdateCameraOnExitIfPlayer();
            if (_entity.EntityAnimator != null) _entity.EntityAnimator.SetFloat(_entity.AnimatorData.XInputFloat, 0f);
        }

        private bool ShouldSwitchToIdle()
        {
            if (_colLeft && _entity.InputProvider.MoveInput.x < 0f)
                return true;
            
            if (_colRight && _entity.InputProvider.MoveInput.x > 0f)
                return true;

            return _entity.InputProvider.MoveInput.x == 0f;
        }
        
        // TODO: these two if player functions are not appropriate, if there is time they can be refactored.
        private void UpdateCameraIfPlayer()
        {
            if (_entity is PlayerEntity)
            {
                if (_entity.IsAttached && _camBrain.m_UpdateMethod != CinemachineBrain.UpdateMethod.FixedUpdate)
                {
                    _camBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.FixedUpdate;
                }
                if (!_entity.IsAttached && _camBrain.m_UpdateMethod != CinemachineBrain.UpdateMethod.LateUpdate)
                {
                    _camBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
                }
            }
        }
        
        private void UpdateCameraOnExitIfPlayer()
        {
            if (_entity is PlayerEntity)
            {
                _camBrain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
            }
        }
    }
}
using UnityEngine;

namespace Metro
{
    public class IntroLevelState : BaseLevelState
    {
        public IntroLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            GameManager.Instance.PlayerInputHandler.ChangeControllType(true);
        }        

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();

            GameManager.Instance.PlayerInputHandler.ChangeControllType(false);
        }
    }
}

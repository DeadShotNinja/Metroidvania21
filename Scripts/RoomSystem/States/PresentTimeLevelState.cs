using System;
using Cinemachine;
using UnityEngine;

namespace Metro
{
	public class PresentTimeLevelState : BaseLevelState
	{
		public PresentTimeLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine)
		{
		}

        public override void Enter()
        {
            base.Enter();

            _levelManager.CurrentRoom.ChangeRoomPeriod(TimePeriod.Present);

            _levelManager.PostProcessVolume.profile = _levelManager.PresentProfile;

            // Spawn any enemies that need to spawn
            
            EventManager.TriggerEvent(new LevelReadyEvent());
            
            EventManager.StartListening<ChangePeriodEvent>(OnChangePeriod);
            EventManager.StartListening<ChangeRoomEvent>(OnChangeRoom);
        }        

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // Maybe have some periodic things happen here?
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();

            // Do any cleanup (maybe disabling enemies, but it should be taken care of when room disables, unless enemies will not 
            // be under the room itself but in a seperate enemy spawner?
            
            EventManager.StopListening<ChangePeriodEvent>(OnChangePeriod);
            EventManager.StopListening<ChangeRoomEvent>(OnChangeRoom);
        }
        
        private void OnChangePeriod(ChangePeriodEvent eventData)
        {
            _stateMachine.ChangeState(_levelManager.PastTimeLevelState);
        }
        
        private void OnChangeRoom(ChangeRoomEvent eventData)
        {
            Room holder = _levelManager.RoomHolders.Find(x => x.RoomID == eventData.TargetRoomID);
            if (holder == null)
            {
                Debug.LogError("Room ID does not exist in collection of rooms.");
                return;
            }
            
            _levelManager.CurrentRoom.HideAllVariants();
            _levelManager.CurrentRoom = holder;

            // TODO: don't need to change time period, just need to make sure it shows the right room.
            _levelManager.CurrentRoom.ChangeRoomPeriod(TimePeriod.Present);
            
            SpawnPoint newSpawn = Array.Find(_levelManager.CurrentRoom.CurrentRoomVariant.SpawnPoints,
                x => x.SpawnID == eventData.TargetSpawnID);
            Vector3 newPos;
            if (newSpawn == null)
            {
                Debug.LogError("Specified target spawn point did not exist, defaulting to first spawn found.");
                newPos = _levelManager.CurrentRoom.GetFirstSpawnPoint();
            }
            else
            {
                newPos = newSpawn.transform.position; 
            }
            newPos = newSpawn.transform.position;
            _levelManager.PlayerEntity.transform.position = newPos;
            
            // TODO; need better setup for this....
            CinemachineConfiner2D confiner = _levelManager.PlayerCamera.GetComponentInChildren<CinemachineConfiner2D>();
            confiner.m_BoundingShape2D = _levelManager.CurrentRoom.CurrentRoomVariant.CameraConfiner;
        }
    }
}
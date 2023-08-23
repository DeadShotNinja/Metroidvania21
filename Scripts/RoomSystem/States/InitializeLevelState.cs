using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

namespace Metro
{
	public class InitializeLevelState : BaseLevelState
	{
		public InitializeLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();

			Debug.Log("LevelManager State: Initialize");
			
			SetupAllRooms();
			SetupPlayer();
			SetupCamera();
			SetupPostProcessing();
			// TODO: Prevent game from starting if any of the above don't set up?
			_stateMachine.ChangeState(_levelManager.PresentTimeLevelState);
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
		}

        private void SetupAllRooms()
        {
	        foreach (GameObject holderPrefab in _levelManager.RoomHolderPrefabs)
	        {
		        GameObject go = Object.Instantiate(holderPrefab, Vector3.zero, Quaternion.identity,
			        _levelManager.transform);
		        
		        if (go.TryGetComponent(out Room roomHolder))
		        {
			        _levelManager.RoomHolders.Add(roomHolder);
			        roomHolder.ShowPresentVariant();
			        if (roomHolder.RoomID == _levelManager.StartingRoomID) { _levelManager.CurrentRoom = roomHolder; }
			        else { roomHolder.HideAllVariants(); }
		        }
		        else
			        Debug.LogError("A room prefab is missing a Room component.");
	        }
        }
        
        private void SetupPlayer()
        {
	        GameObject go;
	        if (!_levelManager.PlayerInScene)
		        go = Object.Instantiate(_levelManager.PlayerPrefab);
	        else
		        go = _levelManager.PlayerPrefab;

            if (go.TryGetComponent(out PlayerEntity player))
	            _levelManager.PlayerEntity = player;
            else
                Debug.LogError("Missing PlayerEntity component on player prefab");

            SpawnPoint newSpawn = Array.Find(_levelManager.CurrentRoom.CurrentRoomVariant.SpawnPoints,
	            x => x.SpawnID == _levelManager.StartingSpawnID);

            Vector3 newPos;
            if (newSpawn == null)
            {
	            Debug.LogError("Specified starting spawn point did not exist, defaulting to first spawn found.");
	            newPos = _levelManager.CurrentRoom.GetFirstSpawnPoint();
            }
            else
            {
	            newPos = newSpawn.transform.position; 
            }

            newPos = newSpawn.transform.position;
	        go.transform.position = newPos;
        }
        
        private void SetupCamera()
        {
	        CinemachineVirtualCamera virtualCam = _levelManager.PlayerCamera.GetComponentInChildren<CinemachineVirtualCamera>();
	        virtualCam.Follow = _levelManager.PlayerEntity.transform;
	        CinemachineConfiner2D confiner = _levelManager.PlayerCamera.GetComponentInChildren<CinemachineConfiner2D>();
	        confiner.m_BoundingShape2D = _levelManager.CurrentRoom.CurrentRoomVariant.CameraConfiner;
        }
        
        private void SetupPostProcessing()
        {
	        GameObject go;
	        if (!_levelManager.VolumeInScene)
		        go = Object.Instantiate(_levelManager.PostProcessPrefab);
	        else
		        go = _levelManager.PostProcessPrefab;

	        if (go.TryGetComponent(out Volume vol))
	        {
		        vol.profile = null;
		        _levelManager.PostProcessVolume = vol;
	        }
	        else
		        Debug.LogError("Missing Volume component on PostProcessing prefab.");
        }
    }
}
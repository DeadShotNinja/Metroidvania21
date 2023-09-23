using System;
using System.Collections.Generic;

namespace Metro
{
	public class AbilityManager
	{
		private PlayerEntity _player;
		private Dictionary<AbilityType, Action> _abilityUnlockStrategies;
		
		public AbilityManager(PlayerEntity player)
		{
			_player = player;
			_abilityUnlockStrategies = new Dictionary<AbilityType, Action>
			{
				{ AbilityType.Dash, UnlockDash },
				{ AbilityType.DoubleJump, UnlockDoubleJump },
				{ AbilityType.Climb, UnlockClimb },
				{ AbilityType.WallJump, UnlockWallJump }
			};
		}
		
		public void UnlockAbility(AbilityType abilityType)
		{
			if (_abilityUnlockStrategies.ContainsKey(abilityType))
			{
				_abilityUnlockStrategies[abilityType].Invoke();
			}
		}
		
		private void UnlockDash()
		{
			EntityDash dash = _player.GetComponentInChildren<EntityDash>();
			if (dash == null) return;
			if (!dash.AllowDash) dash.SetDashAllowed(true);
		}
		
		private void UnlockDoubleJump()
		{
			EntityJump jump = _player.GetComponentInChildren<EntityJump>();
			if (jump == null) return;
			if (jump.AllowedJumps < 2) jump.ModifyJumps(1);
		}
		
		private void UnlockClimb()
		{
			EntityClimb climb = _player.GetComponentInChildren<EntityClimb>();
			if (climb == null) return;
			//if (!climb.AllowWallSlide) climb.ModifyAllowClimb(true);
		}
		
		private void UnlockWallJump()
		{
			EntityJump jump = _player.GetComponentInChildren<EntityJump>();
			if (jump == null) return;
			if (!jump.AllowWalljumping) jump.ModifyAllowWalljump(true);
		}
	}
}
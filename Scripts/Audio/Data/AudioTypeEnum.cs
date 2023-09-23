namespace Metro
{
	// All Player Wwise Events
	public enum EntityAudioType
	{
        Play_PlayerDash,
		Play_PlayerGrappleHook,
		Play_PlayerJump,
		Play_PlayerLand,
		Play_PlayerSlideLoop,
        Stop_PlayerSlideLoop
    }
	// All Ambience Wwise Events
	public enum AmbienceAudioType
	{
		Play_Ambience,
		Stop_Ambience
	}
	// All Environment Wwise Events
    public enum EnvironmentAudioType
    {
        Play_CeilingSpikeFall,
        Play_CeilingSpikeImpact,
        Play_DimensionShift,
		Play_FloorSpikeHit,
		Play_CannonShot,
        Play_DetroyTrapPlatform,
        Play_DestoryTrapWall,
        Play_AlterAmbienceLoop,
        Stop_AlterAmbienceLoop,
        Play_AlterInteract
    }
    // All UI Wwise Events
    public enum UIAudioType
    {
        Play_HUDClick,
        Play_PaperFlip,
        Play_UIClicks,
        Play_UIMapClose,
        Play_UIMapOpen
    }
     // All UI Wwise Events
    public enum ItemAudioType
    {
        Play_ItemGenericPickup,
        Play_ItemGunFire,
        Play_SoulboundCapfeather_Loop,
        Play_SoulboundCompass_Loop,
        Play_SoulboundGun_Loop,
        Play_SoulboundLocketLoop,
        Play_SoulboundRecipeBookLoop,
        Stop_SoulboundCapfeather_Loop,
        Stop_SoulboundCompass_Loop,
        Stop_SoulboundGun_Loop,
        Stop_SoulboundLocketLoop,
        Stop_SoulboundRecipeBookLoop,
        Play_SoulboundItemPickup,
    }
    // All Enemy Wwise Events
    public enum EnemyAudioType
    {
        Play_EnemyRatDeath,
        Play_EnemyRatIdle
    }
}
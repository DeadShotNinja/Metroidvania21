/*
 * @ Jacob Wolfe '23
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Metro;
using UnityEngine;

public class AudioStateManager : MonoBehaviour
{
    [Header("General Game State Variables")]
    [SerializeField] private AK.Wwise.State Game_Gameplay;
    [SerializeField] private AK.Wwise.State Game_MainMenu;
    [SerializeField] private AK.Wwise.State Game_GameLose;
    [SerializeField] private AK.Wwise.State Game_GameWin;
    [SerializeField] private AK.Wwise.State Game_GamePaused;
    [SerializeField] private AK.Wwise.State Game_None;

    [Header("General Music State Variables")]
    [SerializeField] private AK.Wwise.State Music_MainMenu;
    [SerializeField] private AK.Wwise.State Music_LevelStart;
    [SerializeField] private AK.Wwise.State Music_LevelWin;
    [SerializeField] private AK.Wwise.State Music_LevelLose;
    [SerializeField] private AK.Wwise.State Music_PauseMenu;
    [SerializeField] private AK.Wwise.State Music_None;

    [Header("Player Ship Level Location State Variables")]
    [SerializeField] private AK.Wwise.State PlayerLocationLevel_None;
    [SerializeField] private AK.Wwise.State PlayerLocationLevel_Level1;
    [SerializeField] private AK.Wwise.State PlayerLocationLevel_Level2;
    [SerializeField] private AK.Wwise.State PlayerLocationLevel_Level3;
    [SerializeField] private AK.Wwise.State PlayerLocationLevel_Level4;

    [Header("Dimension State Variables")]
    [SerializeField] private AK.Wwise.State Dimension_None;
    [SerializeField] private AK.Wwise.State Dimension_Past;
    [SerializeField] private AK.Wwise.State Dimension_Present;

    [Header("Player Ability Search Variables")]
    [SerializeField] private AK.Wwise.State PlayerAbilitySearch_None;
    [SerializeField] private AK.Wwise.State PlayerAbilitySearch_Ability1;
    [SerializeField] private AK.Wwise.State PlayerAbilitySearch_Ability2;
    [SerializeField] private AK.Wwise.State PlayerAbilitySearch_Ability3;
    [SerializeField] private AK.Wwise.State PlayerAbilitySearch_Ability4;
    [SerializeField] private AK.Wwise.State PlayerAbilitySearch_Ability5;

    // Add Soundbanks from Inspector
    [Header("Soundbanks to Load on Initialization")]
    [SerializeField] private List<AK.Wwise.Bank> Soundbanks;

    // Main Music Play/Stop Events
    [Header("Wwise Music Events")]
    [SerializeField] private AK.Wwise.Event PlayMainMusic;
    [SerializeField] private AK.Wwise.Event StopMainMusic;

    // Start is called before the first frame updates
    void Start()
    {
        // Update States Accordingly
    }

    // TODO: EXAMPLE
    private void OnEnable()
    {
        EventManager.StartListening<WinGameEvent>(OnGameWon);
        EventManager.StartListening<PlayerDiedEvent>(OnPlayerDied);
        EventManager.StartListening<PlayerRespawnedEvent>(OnPlayerRespawn);
    }
    // On Game Won
    private void OnGameWon(WinGameEvent eventData)
    {
        Game_GameWin.SetValue();
        Music_LevelWin.SetValue();
    }
    // On Player Died
    private void OnPlayerDied(PlayerDiedEvent eventData)
    {
        Game_GameLose.SetValue();
        Music_LevelLose.SetValue();
    }
    // On Player Respawn
    private void OnPlayerRespawn(PlayerRespawnedEvent eventData)
    {
        Game_Gameplay.SetValue();
    }
    private void OnDestroy()
    {
        EventManager.StopListening<WinGameEvent>(OnGameWon);
        EventManager.StopListening<PlayerDiedEvent>(OnPlayerDied);
        EventManager.StopListening<PlayerRespawnedEvent>(OnPlayerRespawn);
    }
    // TODO: Put load soundbanks in a singleton.
    void LoadSoundbanks()
    {
        if (Soundbanks.Count > 0)
        {
            foreach (AK.Wwise.Bank bank in Soundbanks)
            {
                bank.Load();
            }
            Debug.Log("Startup Soundbanks have been loaded.");
        }
        else
        {
            Debug.LogWarning("No Soundbanks Loaded! Are Banks assigned in Audio State Manager?");
        }
    }
}
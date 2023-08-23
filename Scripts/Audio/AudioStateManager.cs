/*
 * Singleton Class that controls Wwise State Management for Music and Sound Effects through globally accessible enums.
 * @ Jacob Wolfe '23
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global enum for General Game State
public enum WwiseGameState
{
    Gameplay, MainMenu, GameOver, GamePaused, None
}
// Global enum for General Music State
public enum WwiseMusicState
{
    MainMenu, LevelStart, LevelWin, LevelLose, PauseMenu, None
}
// Global enum for Dimension State
public enum WwiseDimensionState
{
    Past, Present, Future, None
}
// Global enum for Music Ability Search State
public enum WwiseMusicAbilitySearchState
{
    AbilitySearch1, AbilitySearch2, AbilitySearch3, AbilitySearch4, AbilitySearch5, None
}
// Global enum for Player Ship Level Location State
public enum WwisePlayerShipLevelLocation
{
    Location1, Location2, Location3, Location4, None
}

public class AudioStateManager : MonoBehaviour
{
    public static AudioStateManager Instance;

    private bool isInitialized;

    // Used to check respective current State, and won't run function code if it's trying to set the same State as the one already set.
    private WwiseGameState currentGameState;
    private WwiseMusicState currentMusicState;
    private WwisePlayerShipLevelLocation currentPlayerShipLevelLocationState;
    private WwiseDimensionState currentDimensionState;
    private WwiseMusicAbilitySearchState currentAbilitySearchState;

    [Header("General Game State Variables")]
    [SerializeField] private AK.Wwise.State Game_Gameplay;
    [SerializeField] private AK.Wwise.State Game_MainMenu;
    [SerializeField] private AK.Wwise.State Game_GameOver;
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
    [SerializeField] private AK.Wwise.State Dimension_Future;

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

    // Call Initialize in Awake
    private void Awake()
    {
        Initialize();
    }

    // Start is called before the first frame updates
    void Start()
    {
        // Update States Accordingly
        SetWwiseGameState(WwiseGameState.MainMenu);
        SetWwiseMusicState(WwiseMusicState.MainMenu);
        SetWwiseDimensionState(WwiseDimensionState.Present);
        SetWwiseMusicAbilitySearchState(WwiseMusicAbilitySearchState.None);
        SetWwisePlayerShipLevelLocation(WwisePlayerShipLevelLocation.Location1);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    // Init Singleton
    void Initialize()
    {
        // Singleton Logic
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.LogWarning("Audio State Manager already Exists! Destroying new instance.");
            Destroy(this);
        }
        // Initialize Soundbanks
        if (!isInitialized)
        {
            LoadSoundbanks();
        }

        isInitialized = true;

        // Set All Wwise States to None
        SetWwiseGameState(WwiseGameState.None);
        SetWwiseMusicState(WwiseMusicState.None);
        SetWwiseDimensionState(WwiseDimensionState.None);
        SetWwiseMusicAbilitySearchState(WwiseMusicAbilitySearchState.None);
        SetWwisePlayerShipLevelLocation(WwisePlayerShipLevelLocation.None);
    }
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
    public void SetWwiseGameState(WwiseGameState gameState)
    {
        if (gameState == currentGameState)
        {
            Debug.LogWarning("Game State value " + gameState + "already set!");
            return;
        }
        switch (gameState)
        {
            case (WwiseGameState.MainMenu):
                Game_MainMenu.SetValue();
                break;
            case (WwiseGameState.GamePaused):
                Game_GamePaused.SetValue();
                break;
            case (WwiseGameState.GameOver):
                Game_GameOver.SetValue();
                break;
            case (WwiseGameState.Gameplay):
                Game_Gameplay.SetValue();
                break;
            case (WwiseGameState.None):
                Game_None.SetValue();
                break;
        }
        currentGameState = gameState;
        Debug.Log("New Wwise Game State: " + gameState + ".");
    }
    public void SetWwiseMusicState(WwiseMusicState musicState)
    {
        if (musicState == currentMusicState)
        {
            Debug.LogWarning("Music State value " + musicState + "already set!");
            return;
        }
        switch (musicState)
        {
            case WwiseMusicState.MainMenu:
                Music_MainMenu.SetValue();
                break;
            case WwiseMusicState.PauseMenu:
                Music_PauseMenu.SetValue();
                break;
            case WwiseMusicState.LevelStart:
                Music_LevelStart.SetValue();
                break;
            case WwiseMusicState.LevelLose:
                Music_LevelLose.SetValue();
                break;
            case WwiseMusicState.LevelWin:
                Music_LevelWin.SetValue();
                break;
            case WwiseMusicState.None:
                Music_None.SetValue();
                break;
        }
        currentMusicState = musicState;
        Debug.Log("New Wwise Music State: " + musicState + ".");
    }
    public void SetWwisePlayerShipLevelLocation(WwisePlayerShipLevelLocation playerShipLevelLocation)
    {
        if (playerShipLevelLocation == currentPlayerShipLevelLocationState)
        {
            Debug.LogWarning("Player Ship Level Location State value " + playerShipLevelLocation + "already set!");
            return;
        }
        switch (playerShipLevelLocation)
        {
            case (WwisePlayerShipLevelLocation.Location1):
                PlayerLocationLevel_Level1.SetValue();
                break;
            case (WwisePlayerShipLevelLocation.Location2):
                PlayerLocationLevel_Level2.SetValue();
                break;
            case (WwisePlayerShipLevelLocation.Location3):
                PlayerLocationLevel_Level3.SetValue();
                break;
            case (WwisePlayerShipLevelLocation.Location4):
                PlayerLocationLevel_Level4.SetValue();
                break;
            case (WwisePlayerShipLevelLocation.None):
                PlayerLocationLevel_None.SetValue();
                break;
        }
        currentPlayerShipLevelLocationState = playerShipLevelLocation;
        Debug.Log("New Wwise Player Ship Level Location State: " + playerShipLevelLocation + ".");
    }
    public void SetWwiseDimensionState(WwiseDimensionState dimenstionState)
    {
        if (dimenstionState == currentDimensionState)
        {
            Debug.LogWarning("Dimension State value " + dimenstionState + "already set!");
            return;
        }
        switch (dimenstionState)
        {
            case (WwiseDimensionState.Past):
                Dimension_Past.SetValue();
                break;
            case (WwiseDimensionState.Present):
                Dimension_Present.SetValue();
                break;
            case (WwiseDimensionState.Future):
                Dimension_Future.SetValue();
                break;
            case (WwiseDimensionState.None):
                Dimension_None.SetValue();
                break;
        }
        currentDimensionState = dimenstionState;
        Debug.Log("New Wwise Dimension Location State: " + dimenstionState + ".");
    }
    public void SetWwiseMusicAbilitySearchState(WwiseMusicAbilitySearchState musicAbilitySearchState)
    {
        if (musicAbilitySearchState == currentAbilitySearchState)
        {
            Debug.LogWarning("Music Ability Search State value " + musicAbilitySearchState + "already set!");
            return; 
        }
        switch (musicAbilitySearchState)
        {
            case (WwiseMusicAbilitySearchState.None):
                PlayerAbilitySearch_None.SetValue();
                break;
            case (WwiseMusicAbilitySearchState.AbilitySearch1):
                PlayerAbilitySearch_Ability1.SetValue();
                break;
            case (WwiseMusicAbilitySearchState.AbilitySearch2):
                PlayerAbilitySearch_Ability2.SetValue();
                break;
            case (WwiseMusicAbilitySearchState.AbilitySearch3):
                PlayerAbilitySearch_Ability3.SetValue();
                break;
            case (WwiseMusicAbilitySearchState.AbilitySearch4):
                PlayerAbilitySearch_Ability4.SetValue();
                break;
            case (WwiseMusicAbilitySearchState.AbilitySearch5):
                PlayerAbilitySearch_Ability5.SetValue();
                break;
        }
        currentAbilitySearchState = musicAbilitySearchState;
        Debug.Log("Music Ability Search State: " + musicAbilitySearchState + ".");
    }
}
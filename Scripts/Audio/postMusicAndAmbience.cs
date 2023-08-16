using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
//prevents building for some reason
//using UnityEditor.SceneManagement;


// @ Jacob Wolfe's Hacky Code '22
// THIS NEEDS TO GET REWRITTEN AND REFACTORED.




public enum MusicSwitcher : uint 
{ 
    Credits = 0,
    LevelA = 50,
    LevelB = 100,
    LevelC = 150,
    MainMenu = 200
}

public enum MenuState : uint 
{ 
    Yes = 0,
    No = 50
}
public class postMusicAndAmbience : MonoBehaviour
{
    public UnityEvent OnAmbienceStarted;
    public UnityEvent OnMusicStarted;

    public UnityEvent OnDestroyEvent;
    public MusicSwitcher musicSwitcher;
    public MenuState menuState;
    // Start is called before the first frame update

    private void Awake() 
    {
        switch (menuState)
        { 
            case MenuState.Yes:
            { 
                AkSoundEngine.SetRTPCValue("SfxMenuVolume", 0.0f);
                AkSoundEngine.SetState("Menu_Screen", "Yes");
                break;
            }
            case MenuState.No:
            { 
                AkSoundEngine.SetRTPCValue("SfxMenuVolume", 100.0f);
                AkSoundEngine.SetState("Menu_Screen", "No");
                break;
            }
        }
    }
    void Start()
    {
        switch (musicSwitcher)
        { 
            case MusicSwitcher.Credits:
            { 
                AkSoundEngine.SetState("CurrentLevel", "Credits");
                break;
            }
            case MusicSwitcher.LevelA:
            { 
                AkSoundEngine.SetState("CurrentLevel", "LevelA");
                break;
            }
            case MusicSwitcher.LevelB:
            { 
                AkSoundEngine.SetState("CurrentLevel", "LevelB");
                break;
            }
            case MusicSwitcher.LevelC:
            { 
                AkSoundEngine.SetState("CurrentLevel", "LevelC");
                break;
            }
            case MusicSwitcher.MainMenu:
            { 
                AkSoundEngine.SetState("CurrentLevel", "MainMenu");
                break;
            }
        }
        AkSoundEngine.SetState("PlayerAction", "None");
        OnAmbienceStarted?.Invoke();
        OnMusicStarted?.Invoke();
    }

    private void OnDestroy() {
        OnDestroyEvent?.Invoke();
    }
}

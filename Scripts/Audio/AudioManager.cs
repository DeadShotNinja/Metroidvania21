using Metro;
using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
    public class AudioManager : PersistantSingleton<AudioManager>
    {
        // Soundbanks
        [Header("Soundbanks to Load on Initialization")]
        [SerializeField] private List<AK.Wwise.Bank> Soundbanks;
        // Music
        [Header("Music Events")]
        [SerializeField] private AK.Wwise.Event Play_Music;
        [SerializeField] private AK.Wwise.Event Stop_Music;
        // Ambience
        [Header("Ambience Events")]
        [SerializeField] private AK.Wwise.Event Play_Ambience;
        [SerializeField] private AK.Wwise.Event Stop_Ambience;

        // Awake
        protected override void Awake()
        {
            base.Awake();
            LoadSoundbanks();
        }
        // Start
        private void Start()
        {
            PostEvent(Play_Music);
            PostEvent(Play_Ambience);
        }
        // Generic Post Event
        private void PostEvent(AK.Wwise.Event audioEvent)
        {
            // Posts the wwiseEvent on this game object if the event is valid
            if (audioEvent.IsValid())
            {
                //Debug.Log(wwiseEvent);
                audioEvent.Post(gameObject);
            }

            // Log warning
            else
            {
                Debug.LogWarning("Warning: Wwise Event " + audioEvent + " is not valid");
            }
        }
        // Load Soundbanks
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
        // On Application Quit
        protected override void OnApplicationQuit()
        {
            base.OnApplicationQuit();
        }
    }
}
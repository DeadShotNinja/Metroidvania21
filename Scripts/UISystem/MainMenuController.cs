using UnityEngine;
using UnityEngine.UI;

namespace Metro
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("UI Dependencies")]
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Slider _masterVolSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        private const string MASTER_VOLUME = "MasterVolume";
        private const string MUSIC_VOLUME = "MusicVolume";
        private const string SFX_VOLUME = "SFXVolume";

        private void Start()
        {
            float masterVol = PlayerPrefs.GetFloat(MASTER_VOLUME, 50f);
            float musicVol = PlayerPrefs.GetFloat(MUSIC_VOLUME, 50f);
            float sfxVol = PlayerPrefs.GetFloat(SFX_VOLUME, 50f);
            _masterVolSlider.value = masterVol;
            SetWwiseMaster(masterVol);
            _musicSlider.value = musicVol;
            SetWwiseMusic(musicVol);
            _sfxSlider.value = sfxVol;
            SetWwiseSFX(sfxVol);

            _mainMenuPanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }

        public void OnClick_NewGameButton()
        {
            if (GameDatabase.Instance != null) 
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIClicks)?.Post(gameObject);

            SceneLoader.LoadScene(3);
        }

        public void OnClick_SettingsButton()
        {
            if (GameDatabase.Instance != null) 
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIClicks)?.Post(gameObject);

            _mainMenuPanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        public void OnClick_CreditsButton()
        {
            if (GameDatabase.Instance != null)
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIClicks)?.Post(gameObject);

            SceneLoader.LoadScene(4);
        }

        public void OnClick_BackButton()
        {
            if (GameDatabase.Instance != null)
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIClicks)?.Post(gameObject);

            _mainMenuPanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }

        public void OnClick_ExitButton()
        {
            if (GameDatabase.Instance != null) 
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIClicks)?.Post(gameObject);

            Application.Quit();
        }

        public void OnValueChanged_Master(float value)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME, value);
            SetWwiseMaster(value);
        }

        public void OnValueChanged_Music(float value)
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME, value);
            SetWwiseMusic(value);
        }

        public void OnValueChanged_SFX(float value)
        {
            PlayerPrefs.SetFloat(SFX_VOLUME, value);
            SetWwiseSFX(value);
        }

        private void SetWwiseMaster(float value)
        {
            
            AkSoundEngine.SetRTPCValue("Master_Volume", value);
        }

        private void SetWwiseMusic(float value)
        {
            AkSoundEngine.SetRTPCValue("Music_Volume", value);
        }

        private void SetWwiseSFX(float value)
        {
            AkSoundEngine.SetRTPCValue("SFX_Volume", value);
        }
    }
}

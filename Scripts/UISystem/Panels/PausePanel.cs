using UnityEngine;
using UnityEngine.UI;

namespace Metro
{
	public class PausePanel : MonoBehaviour
	{
        [Header("Setup")]
        [SerializeField] private GameObject _optionsPanel;
        [SerializeField] private Slider _masterVolSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        private const string MASTER_VOLUME = "MasterVolume";
        private const string MUSIC_VOLUME = "MusicVolume";
        private const string SFX_VOLUME = "SFXVolume";

        private void OnEnable()
        {
			if (GameDatabase.Instance != null) 
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIMapOpen)?.Post(gameObject);
        }

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

            _optionsPanel.SetActive(false);
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;

            if (GameDatabase.Instance != null) 
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIMapClose)?.Post(gameObject);
        }

        public void OnClick_Resume()
		{
            if (GameDatabase.Instance != null) 
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIClicks)?.Post(gameObject);

            GameManager.Instance.ChangeGameState(GameState.Playing);
		}

		public void OnClick_Options()
		{
            if (GameDatabase.Instance != null) 
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_HUDClick)?.Post(gameObject);

            _optionsPanel.SetActive(true);
        }

		public void OnClick_MainMenu()
		{
			Time.timeScale = 1f;

            if (GameDatabase.Instance != null) 
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIClicks)?.Post(gameObject);

            SceneLoader.LoadScene(2);
		}

        public void OnClick_BackButton()
        {
            if (GameDatabase.Instance != null)
                GameDatabase.Instance.GetUIAudioEvent(UIAudioType.Play_UIClicks)?.Post(gameObject);

            _optionsPanel.SetActive(false);
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
/*
 * Allows developers to control the volume through UnityEngine.UI.Slider objects and WWise RTPC
 * @ Jacob Shreve '?, @ Nikhil Ghosh '23
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Audio
{
    /// <summary>
    /// A struct containing a certain volume and ways to adjust it.
    /// </summary>
    [System.Serializable]
    public struct VolumeData
    {
        [Tooltip("The slider that controls the volume")]
        public UnityEngine.UI.Slider volumeSlider;

        [Tooltip("The name of the RTPC in Wwise. Note that this will be the same name for PlayerPrefs")]
        public string volumeRTPC;

        /// <summary>
        /// Set the RTPC and PlayerPrefs value for the volume when the value on a slider has changed
        /// </summary>
        /// <param name="newValue">The new value for the RTPC</param>
        public void SetVolumeFromValue(float newValue)
        {
            // Clamps volume value to be 0 or 1
            float realVolume = Mathf.Clamp01(newValue);
            Debug.Log(volumeRTPC + " set to " + realVolume);

            // Sets new volume for UI volume slider, PlayerPrefs, and WWise
            volumeSlider.SetValueWithoutNotify(realVolume * volumeSlider.maxValue);
            PlayerPrefs.SetFloat(volumeRTPC, realVolume);
            AkSoundEngine.SetRTPCValue(volumeRTPC, realVolume * 100);
        }
    }

    public class VolumeController : MonoBehaviour
    {
        [Tooltip("A list of volumes that can be controlled")]
        public List<VolumeData> volumesToControl;

        void Start()
        {
            foreach (VolumeData volume in volumesToControl)
            {
                // Sets new volume if PlayerPrefs contains the WWise RTPC
                if (PlayerPrefs.HasKey(volume.volumeRTPC))
                {
                    Debug.Log("has preferences");
                    volume.SetVolumeFromValue(PlayerPrefs.GetFloat(volume.volumeRTPC));
                }

                // Sets default volume of 0.5f
                else
                {
                    volume.SetVolumeFromValue(0.5f);
                }

                // Adds callback that sets volume when changed by UI slider
                volume.volumeSlider.onValueChanged.AddListener(volume.SetVolumeFromValue);
            }
        }

        /// <summary>
        /// Notifies developer when a volume change occurs
        /// </summary>
        public void onVolumeChange(int index)
        {
            Debug.Log("Changing Volume!");
            Debug.Log(index);
        }
    }
}
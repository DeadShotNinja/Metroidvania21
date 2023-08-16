/* 
 * Activates child gameobjects after a delay measured in frames.
 * Used for delaying sound bank activation when a scene is loaded
 * @ Max Perraut '20
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Audio
{
    public class ActivateChildren : MonoBehaviour
    {
        [Tooltip("Delay in frames between scene start and the activation of the children")]
        public int frameDelay = 1;

        void Start()
        {
            StartCoroutine(DoDelayedActivation());
        }

        /// <summary>
        /// Delays the activation until the specified number of frames
        /// </summary>
        private IEnumerator DoDelayedActivation()
        {
            // Waits for <frameDelay> frames
            for (int i = 0; i < frameDelay; i++)
            {
                yield return null;
            }

            // Activates each child gameobject
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

}
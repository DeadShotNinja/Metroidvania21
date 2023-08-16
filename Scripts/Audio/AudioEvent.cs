/* 
 * Scriptable object specialized for audio events. Allows audio team work to be
 * completely decoupled from all other work, minimizing risk of merge conflicts during the workflow
 * @ Nicolas Williams '20
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Audio
{
    // Creates a ScriptableObject listed in the assets menu to be easily created
    [CreateAssetMenu(menuName = "AudioEvent", fileName = "New Audio Event", order = 1)]
    public class AudioEvent : ScriptableObject
    {
        public AK.Wwise.Event wwiseEvent;

        /// <summary>
        /// Calls the Wwise event, if valid, to play audio
        /// </summary>
        /// <param name="caller">The game object calling the event</param>
        public void PostWwiseEvent(GameObject caller)
        {
            // Posts the wwiseEvent on the caller game object if the event is valid.
            if (wwiseEvent.IsValid())
            {
                //Debug.Log(wwiseEvent);
                wwiseEvent.Post(caller);
            }

            // Log warning for audio not yet created.
            else
            {
                Debug.LogWarning("Warning: missing audio for audio event: " + name);
            }
        }
    }
}

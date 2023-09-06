using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event wwiseEvent;

    /// <summary>
    /// Calls the Wwise event, if valid, to play audio
    /// </summary>
    /// <param name="caller">The game object calling the event</param>
    public void PostEvent(GameObject caller)
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

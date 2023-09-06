using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Create functionality for setting RTPC values which will later be stored in PlayerPrefs.
// Create Scriptable Objects for all Audio Data.





public class AudioRTPCManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // TODO: Use Enums similar to State Manager to make functionality in this Singleton globally accessible
        WwiseUtilities.instance.SetRTPCValue("Master_Volume", 100.0f);
        WwiseUtilities.instance.SetRTPCValue("SFX_Volume", 100.0f);
        WwiseUtilities.instance.SetRTPCValue("Music_Volume", 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

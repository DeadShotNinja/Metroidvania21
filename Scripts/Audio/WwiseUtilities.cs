/*
 * An ongoing set of functions for controlling behavior withing Wwise
 * @ Jacob Wolfe '20
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseUtilities : MonoBehaviour
{
    // Instantiate Singleton Class "WwiseUtilities"
    public static WwiseUtilities instance { get; private set; }
    
    [Header("RTPC Names as they appear in Wwise")]
    [SerializeField] private string[] RTPCNames;
    [Header("AK Environment Names as they appear in Wwise")]
    [SerializeField] private string[] AKEnvironmentNames;
    [Header("Initial AK Environment Values")]
    [SerializeField] private float[] InitialAKEnvironmentValues;
    
    // Used to set RTPC values over a given time period
    private Dictionary<string, RTPC> RTPCs = new Dictionary<string, RTPC>();

    // Used to set initial aux send values.
    // -----> A value in the range [0.0f:16.0f] ( -∞ dB to +24 dB). 
    // Represents the attenuation or amplification factor applied to the volume of the sound going through the auxiliary bus. A value greater than 1.0f will amplify the sound.
    private Dictionary<string, Environment> AKEnvironments = new Dictionary<string, Environment>();
    private AkAuxSendArray auxSendArray;
    private void Awake()
    { 
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            instance = this;
            foreach (string name in RTPCNames)
            { 
                RTPCs.Add(name, new RTPC());
            }
            for (int i = 0; i < AKEnvironmentNames.Length; ++i)
            { 
                AKEnvironments.Add(AKEnvironmentNames[i], new Environment(InitialAKEnvironmentValues[i]));
            }
        }
    }
    // Functions to start and stop our RTPC coroutine on this MonoBehaviour. Call this anywhere.
    // Params:
    //     name = RTPC name (string)
    //     startValue = starting value of RTPC (floating point)
    //     endValue   = ending value of RTPC (floating point)
    //     changeTime = time (seconds) to interpolate from startValue to endValue
    public void ChangeRTPCValue(string name, float startValue, float endValue, float changeTime)
    { 
        try
        { 
            RTPCs[name].ChangeRTPCValue(name, startValue, endValue, changeTime);
            StartCoroutine(RTPCs[name].RepeatingValueChange());

        }
        catch (KeyNotFoundException)
        { 
            Debug.Log("RTPC Not Found");
        }
    }
    // Returns an RTPC Value from Dictionary of RTPCs, avoids making a call to the SoundEngine. Call this anywhere.
    // Params:
    //     name = RTPC name (string)
    // Returns: -1.0 if error and makes a call to Debug.Log, else returns the associated RTPC value. Note that RTPCs can be negative.
    public float GetRTPCValue(string rtpcName)
    { 
        if (RTPCs.ContainsKey(rtpcName))
        { 
            return RTPCs[rtpcName].RTPCValue;
        }
        else
        { 
            Debug.LogWarning("RTPC Not Found");
            return -1.0f;
        }
    }

    public void SetRTPCValue(string rtpcName, float setValue)
    { 
        if (RTPCs.ContainsKey(rtpcName))
        { 
            RTPCs[rtpcName].RTPCValue = setValue;
        }
    }
    // Sets the specified inital aux send values on a GameObject. Call this anywhere.
    // Params:
    //     akGameObject = GameObject to set initial aux send values
    public void SetInitialEnvironmentValues(GameObject akGameObject)
    { 
        auxSendArray = new AkAuxSendArray();
        for (int i = 0; i < AKEnvironmentNames.Length; ++i)
        {
            auxSendArray.Add(akGameObject, AkSoundEngine.GetIDFromString(AKEnvironmentNames[i]), InitialAKEnvironmentValues[i]);
        }
        AkSoundEngine.SetGameObjectAuxSendValues(akGameObject, auxSendArray, (uint)auxSendArray.Count());
    }
}
// RTPC Controller Class. Holds associated RTPC data and makes calls to the SoundEngine.
public class RTPC
{
    private string RTPCName; private float time; private float startingValue; private float endingValue; private float duration; public float RTPCValue;
    public void ChangeRTPCValue(string name, float startValue, float endValue, float changeTime)
    { 
        // Set variables specific to call
        RTPCName = name;
        RTPCValue = startValue;
        startingValue = startValue;
        endingValue = endValue;
        duration = changeTime;
        time = 0.0f;
    }

    public void SetRTPCValue(float setValue)
    { 
        // Set variables specific to call
        RTPCValue = setValue;
    }
    public IEnumerator RepeatingValueChange() 
    {
        while (RTPCValue != endingValue + 1)
        { 
            // Change RTPC value over specified time in-game.
            time += Time.deltaTime / duration;
            RTPCValue = Mathf.Lerp(startingValue, endingValue, time);
            if (RTPCValue <= 100 || RTPCValue >= 0)
            { 
                AkSoundEngine.SetRTPCValue(RTPCName, RTPCValue);
            }
            yield return null;
        }
    }
}
// Aux send "Environment" class. Holds initial aux send value.
public class Environment
{
    public Environment() {}
    public Environment(float initialValue)
    { 
        initValue = initialValue;
    }
    private float initValue = 0.0f;
}
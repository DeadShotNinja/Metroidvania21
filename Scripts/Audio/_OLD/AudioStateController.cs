/*
 * Sets the state of the WWise State Group
 * @Faulker Bodbyl-Mast '21 / Jacob Wolfe '23
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseStateController : MonoBehaviour
{
    [Tooltip("The name of the WWise State Group")]
    public string stateGroup;

    /// <summary>
    /// Changes the state of a WWise State Group. Triggers an error if the event can't be found
    /// </summary>
    /// <param name="in_state">The name of the new state</param>
    public void Change_State(string in_state)
    {
        AKRESULT result = AkSoundEngine.SetState(stateGroup, in_state);
        if (result == AKRESULT.AK_IDNotFound)
        {
            Debug.LogError("ID " + stateGroup + " cannot be found. Make sure that the banks were generated with the 'include string' option.");
        }
    }
}
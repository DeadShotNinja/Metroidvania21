/*
 * Sets corrosponding location of player to Wwise.
 * @ Jacob Wolfe 22
*/

using System.Collections.Generic;
using UnityEngine;

public enum EnclosingEnvironment : uint 
{ 
    Outdoors = 0,
    Cave = 50,
    Archive = 100
}

// Class used to define and update environment-relevant values to the Wwise Sound Engine.
public class WwiseLocation : MonoBehaviour
{
    public EnclosingEnvironment currentEnvironment;
    public bool isExploringFinalCave;
    public bool isSeekingTome;
    // RTPC name (string) that should be set up in Wwise to control amplitude crossfades between environment ambiences. Blend Container would be an option.
    private string environmentRTPCName = "playerEnvironment";
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        { 
            // Update Wwise surface switch 
            switch (currentEnvironment)
            { 
                case EnclosingEnvironment.Outdoors:
                { 
                    AkSoundEngine.SetState("PlayerLocation", "Outdoors");
                    AkSoundEngine.SetSwitch("PlayerSurface", "Grass", other.gameObject);
                    break;
                }
                case EnclosingEnvironment.Cave:
                { 
                    AkSoundEngine.SetState("PlayerLocation", "Cave");
                    AkSoundEngine.SetSwitch("PlayerSurface", "Stone", other.gameObject);
                    break;
                }
                case EnclosingEnvironment.Archive:
                { 
                    AkSoundEngine.SetState("PlayerLocation", "Archives");
                    AkSoundEngine.SetSwitch("PlayerSurface", "Wood", other.gameObject);
                    break;
                }
                default: break;
            }
            if (isSeekingTome)
            { 
                AkSoundEngine.SetState("PlayerAction", "SeekingTome");
            }
            else if (isExploringFinalCave)
            { 
                AkSoundEngine.SetState("PlayerAction", "CaveFinal");
            }
        }
    }
}

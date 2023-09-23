/*
 * Plays an AudioEvent when the object collides with another object, if the object fulfills certain requirements.
 * Note that when the conditions of one are fullfilled, none of the other AudioEvents will be created
 * @ Nikhil Ghosh, Jacob Wolfe '23
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WSoft.Audio
{
    /// <summary>
    /// An enum telling the game how to compare collisions.
    /// </summary>
    public enum CollisionComparisonMode
    {
        NONE = 0, // No comparison required, play audio automatically
        TAG = 1, // Require that object have specific tag in order for audio to be played
        LAYER = 2, // Require that object be in specific LayerMask in order to work
        BOTH = 3 // Require that object both be in layer and have specific tag
    };

    /// <summary>
    /// A struct containing the details for how audio works when two objects collide.
    /// </summary>
    [System.Serializable]
    public struct CollisionAudio
    {
        [Tooltip("How the audio data should be compared.")]
        public CollisionComparisonMode comparisonMode;

        [Tooltip("The LayerMask to compare against. If the CollisionComparisonMode is NONE or TAG, this has no effect")]
        public LayerMask layerMask;

        [Tooltip("The Tag to compare against. If the CollisionComparisonMode is NONE or LAYER, this has no effect")]
        public string tag;

        [Tooltip("The AudioEvent to be played on the collision")]
        public AudioEvent audioEvent;
    }

    public class AudioOnCollide2D : MonoBehaviour
    {
        [Tooltip("Collection of audio data for collisions")]
        public CollisionAudio[] audioDatas;

        /// <summary>
        /// On a collision, cycle through audioDatas to play the appropriate audio.
        /// </summary>
        void OnCollisionEnter2D(Collision2D collision)
        {
            foreach (CollisionAudio audioData in audioDatas)
            {
                bool didAudioPlay = PlayAudioData(audioData, collision.gameObject);
                if (didAudioPlay)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Check if the audio should play, based on the collided object and the audio data,
        /// and play the audio if it should.
        /// </summary>
        /// <param name="audioData">The data of the audio</param>
        /// <param name="collidedObject">The object collided with</param>
        /// <returns>Did the audio actually play</returns>
        bool PlayAudioData(CollisionAudio audioData, GameObject collidedObject)
        {
            switch (audioData.comparisonMode)
            {
                case CollisionComparisonMode.NONE:
                    // Since there is no comparison required, play the audio automatically.
                    audioData.audioEvent.PostWwiseEvent(this.gameObject);
                    return true;
                case CollisionComparisonMode.LAYER:
                    // If the game object has the layer in the LayerMask, play the sound.
                    // Otherwise, don't play the sound.
                    if (HasLayer(collidedObject, audioData.layerMask))
                    {
                        audioData.audioEvent.PostWwiseEvent(this.gameObject);
                        return true;
                    }
                    return false;
                case CollisionComparisonMode.TAG:
                    // If the game object has the specified tag, play the sound.
                    // Otherwise, don't play the sound.
                    if (HasTag(collidedObject, audioData.tag))
                    {
                        audioData.audioEvent.PostWwiseEvent(this.gameObject);
                        return true;
                    }
                    return false;
                case CollisionComparisonMode.BOTH:
                    // If the game object has the layer in the LayerMask and the specified tag,
                    // play the sound. Otherwise, don't play the sound.
                    if (HasTag(collidedObject.gameObject, audioData.tag) && HasLayer(collidedObject, audioData.layerMask))
                    {
                        audioData.audioEvent.PostWwiseEvent(this.gameObject);
                        return true;
                    }
                    return false;
            }
            return false;
        }

        /// <summary>
        /// Check if a GameObject's layer is in a certain LayerMask
        /// </summary>
        bool HasLayer(GameObject testObject, LayerMask mask)
        {
            return IsInLayerMask(mask, testObject.layer);
        }

        /// <summary>
        /// Check if a GameObject has a certain tag
        /// </summary>
        bool HasTag(GameObject testObject, string tag)
        {
            return testObject.CompareTag(tag);
        }
        /// <summary>
        /// Function to check if a GameObject's layer is in a certain LayerMask
        /// </summary>
        public static bool IsInLayerMask(LayerMask mask, int layer)
        {
            if (layer < 0 || layer > 32)
            {
                return false;
            }
            return (((mask.value) >> layer) % 2) == 1;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
    /// <summary>
    /// All helper utility functions in one place.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Will get all the layers that are currently set in the editor that are not blank.
        /// </summary>
        public static IEnumerable<string> GetLayerNames()
        {
            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                if (layerName != "")
                {
                    yield return layerName;
                }
            }
        }

        /// <summary>
        /// Will Debug.Log all layers in a given mask. Optinally provide a collider name for easier readability.
        /// </summary>
        public static void LogLayersInMask(LayerMask mask, string colliderName = "NOT PROVIDED")
        {
            for (int i = 0; i < 32; i++)
            {
                int shifted = 1 << i; // Shift 1 by i places to get the mask for layer i
                if ((mask.value & shifted) == shifted) // Check if the mask includes this layer
                {
                    string layerName = LayerMask.LayerToName(i);
                    Debug.Log("Name: " + colliderName + " Layer: " + i + ": " + layerName);
                }
            }
        }
    }
}
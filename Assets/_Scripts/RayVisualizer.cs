using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the projects line renderer components and their use.
/// </summary>
public class RayVisualizer : MonoBehaviour
{
    private LineRenderer[] _lrs;

    /// <summary>
    /// Create one line renderer component per child object received. Then, grab all of
    /// the line renderers and assign them to the class _lrs array for use.
    /// </summary>
    /// <param name="childObjects">An array of gameobjects, each one meant to server as a 
    /// holder for a line renderer component.</param>
    public void SetLineRenderers(GameObject[] childObjects)
    {
        foreach (GameObject childObject in childObjects)
        {
            childObject.AddComponent<LineRenderer>();
        }
        _lrs = GetComponentsInChildren<LineRenderer>();
    }

    /// <summary>
    /// Formats the line renderers based on given input.
    /// </summary>
    /// <param name="rayWidth">How wide the ray drawn is.</param>
    /// <param name="rayColor">What color the ray drawn is.</param>
    /// <param name="rayMaterial">What material the rays drawn are assigned.</param>
    public void FormatLineRenderers(float rayWidth, Color rayColor, Material rayMaterial)
    {
        foreach (LineRenderer lr in _lrs)
        {
            lr.startColor = rayColor;
            lr.endColor = rayColor;
            lr.positionCount = 2;
            lr.startWidth = rayWidth;
            lr.endWidth = rayWidth;
            lr.material = rayMaterial;

            // Disable shadow casting/receiving to improve performance.
            lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lr.receiveShadows = false;
        }
    }

    /// <summary>
    /// Draws the rays to the screen.
    /// </summary>
    /// <param name="drawPoints">List of Vector3 arrays, each containing the points of a 
    /// ray to be drawn. One Vector3 array per line renderer, the first point is the start
    /// and the second point is the end. The start is always transform.position.</param>
    public void DrawRays(List<Vector3[]> drawPoints)
    {
        for (int i = 0; i < drawPoints.Count; i++)
        {
            _lrs[i].SetPositions(drawPoints[i]);
        }
    }
}

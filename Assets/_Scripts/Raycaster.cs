using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the physics of the rays being cast out.
/// </summary>
public class Raycaster : MonoBehaviour
{
    private int _rayCount; // Number of rays cast out.
    private float _fieldOfView; // Total field of view.
    private float _offsetAngle; // Angle between each ray.

    /// <summary>
    /// Set the class variables.
    /// </summary>
    /// <param name="rayCount">How many rays are being cast out.</param>
    /// <param name="fieldOfView">FOV of the player. Total angle rays are to be cast out to.</param>
    public void SetVariables(int rayCount, float fieldOfView)
    {
        _rayCount = rayCount;
        _fieldOfView = fieldOfView;
        _offsetAngle = CalculateOffestAngle(rayCount, fieldOfView);
    }

    /// <summary>
    /// Determine the offset to be put between each ray.
    /// </summary>
    /// <param name="rayCount">How many rays are being cast out.</param>
    /// <param name="fieldOfView">FOV of the player. Total angle rays are to be cast out to.</param>
    /// <returns></returns>
    private float CalculateOffestAngle(int rayCount, float fieldOfView)
    {
        // Divide FOV by raycount to get offset.
        return fieldOfView / rayCount;
    }

    /// <summary>
    /// Calculate the directions to shoot each ray in using quaternions.
    /// </summary>
    /// <param name="initialDirection">Direction to be used when calculating other directions
    /// using the offset angle.</param>
    /// <returns>An array of directions the cast rays out in.</returns>
    private Vector3[] CalculateDirections(Vector3 initialDirection)
    {
        /* 
         * Sign determines negative or positive angle. Sign is multiplied by -1 every iteration.
         * 
         * Mult determines the intensity of the angle offset. Mult is incremented by 1 every 
         * other iteration, excluding the first.
         */

        Vector3[] directions = new Vector3[_rayCount];
        int sign = -1, mult = 0;

        // Calculate the quaternions that will help determine ray directions.
        for (int i = 0; i < directions.Length; i++)
        {
            // Calculate the quaternion using the current sign, mul, offset values.
            Quaternion q = Quaternion.AngleAxis(sign * mult * _offsetAngle, Vector3.forward);
            Vector3 d = q * initialDirection;
            directions[i] = d;

            // Update multiplier, if necessary.
            mult = i % 2 == 0 ? mult += 1 : mult;

            // Lastly, update the sign value.
            sign *= -1;
        }

        return directions;
    }

    /// <summary>
    /// Cast out rays in the given directions.
    /// </summary>
    /// <param name="rayStart">Point in space the rays start at.</param>
    /// <param name="directions">Array of directions to shoot rays out in.</param>
    /// <returns>An array containing hit data for each raycast.</returns>
    private RaycastHit2D[] CalculateHits(Vector3 rayStart, Vector3[] directions)
    {
        RaycastHit2D[] hits = new RaycastHit2D[_rayCount];
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i] = Physics2D.Raycast(rayStart, directions[i]);
        }

        return hits;
    }

    /// <summary>
    /// Determine where in world space the rays intersected objects.
    /// </summary>
    /// <param name="rayStart">Point in space the rays start at.</param>
    /// <param name="hits">Array of hits containing any data related to each hit.</param>
    /// <returns></returns>
    private List<Vector3[]> CalculateHitpoints(Vector3 rayStart, RaycastHit2D[] hits)
    {
        List<Vector3[]> hitPoints = new List<Vector3[]>();
        for (int i = 0; i < _rayCount; i++)
        {
            /*
             * First vector will always be rayStart. The second vector 
             * is where the raycast hit an object. 
            */
            hitPoints.Add(new Vector3[2] { rayStart, (Vector3) hits[i].point });
        }

        return hitPoints;
    }

    /// <summary>
    /// Algorithm used to visualize rays.
    /// </summary>
    /// <returns>Array of hitpoints representing where rays are in world space.</returns>
    public List<Vector3[]> CalculateHitPoints()
    {
        List<Vector3[]> hitPoints;
        Vector3[] directions;
        RaycastHit2D[] hits;
        Vector3 rayStart, initialDirection;

        // Step 1: Set rayStart, initialDirection variables.
        rayStart = transform.position;
        initialDirection = transform.right.normalized;

        // Step 2: Calculate directions.
        directions = CalculateDirections(initialDirection);
        
        // Step 3: Calculate raycast hits.
        hits = CalculateHits(rayStart, directions);

        // Step 4: Calculate the hit points.
        hitPoints = CalculateHitpoints(rayStart, hits);

        // Step 5: Return hitpoints.
        return hitPoints;
    }

}

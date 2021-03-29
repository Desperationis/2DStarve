using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A static class that deals with holding spawn points. This 
/// is only used server-side.
/// </summary>
public class SpawnRegistry : MonoBehaviour
{
    private static List<Vector2> spawnPoints = new List<Vector2>();

    public static void AddSpawnPoint(Vector2 spawnPoint)
    {
        spawnPoints.Add(spawnPoint);
    }

    /// <summary>
    /// Returns the oldest spawn point added. If there's
    /// no spawn points, return Vector2.zero;
    /// </summary>
    public static Vector2 GetSpawnPoint()
    {
        if(spawnPoints.Count == 0)
        {
            return Vector2.zero;
        }

        return spawnPoints[0];
    }
}

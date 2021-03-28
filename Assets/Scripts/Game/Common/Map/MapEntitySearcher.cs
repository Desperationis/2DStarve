using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperTiled2Unity;

/// <summary>
/// Static class that searches for an entity given a map.
/// </summary>
public class MapEntitySearcher : MonoBehaviour
{
    public static Vector3 GetSpawnPoint(SuperMap map)
    {
        SuperObjectLayer[] layers = map.GetComponentsInChildren<SuperObjectLayer>();
        foreach (SuperObjectLayer layer in layers)
        {
            if(layer.m_TiledName == "SpawnPoint")
            {
                SuperObject[] entities = layer.GetComponentsInChildren<SuperObject>();

                foreach (SuperObject entity in entities)
                {
                    Vector3 position = new Vector3(entity.m_X / map.m_TileWidth, -entity.m_Y / map.m_TileHeight, 0);
                    position += map.transform.position;
                    return position;
                }
            }
        }

        return Vector2.zero;
    }
}

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
                    return MapCoords.EntityToWorld(map, new Vector2(entity.m_X, entity.m_Y));
                }
            }
        }

        return Vector2.zero;
    }
}

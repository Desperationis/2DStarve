using UnityEngine;
using SuperTiled2Unity;

/// <summary>
/// Static class dedicated to converting points in space as being
/// in world or map coordinates. 
/// </summary>
public class MapCoords : MonoBehaviour
{
    /// <summary>
    /// Converts a map coordinate to a world coordinate. Specifically, 
    /// map coordinates are coordinates relative to the topleft, with x-positive
    /// being right and y-positive being down. Each tile on the map represents a single
    /// unit.
    /// </summary>
    public static Vector2 MapToWorld(SuperMap map, Vector2 coords)
    {
        Vector2 worldCoords;
        worldCoords.x = map.transform.position.x + coords.x;
        worldCoords.y = map.transform.position.y - coords.y;

        return worldCoords;
    }


    /// <summary>
    /// Converts an entity coordinate to a world coordinate. Specifically, 
    /// entity coordinates are coordinates relative to the topleft, with x-positive
    /// being right and y-positive being down. However, entity coordinates are scaled
    /// by a factor of the tile size, while map coordinates are not.
    /// </summary>
    public static Vector2 EntityToWorld(SuperMap map, Vector2 coords)
    {
        Vector2 worldCoords;
        worldCoords.x = map.transform.position.x + (coords.x / map.m_TileWidth);
        worldCoords.y = map.transform.position.y - (coords.y / map.m_TileHeight);

        return worldCoords;
    }
}

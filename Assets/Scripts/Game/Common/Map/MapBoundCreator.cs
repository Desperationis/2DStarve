using UnityEngine;
using SuperTiled2Unity;
using Cinemachine;
using System.Collections;

/// <summary>
/// Creates 2D box colliders on the edge of the map to prevent
/// entities walking off it.
/// </summary>
public class MapBoundCreator : MonoBehaviour
{
    [SerializeField]
    private MapLoader mapLoader = null;

    [SerializeField]
    private PolygonCollider2D cameraBound = null;

    [SerializeField]
    private CinemachineConfiner confiner = null;

    private GameObject mapBound = null;


    private int width = 2;

    private void Awake()
    {
        mapLoader.AddLoadListener(SpawnBounds);
        mapBound = (GameObject)Resources.Load("Objects/MapBound");
    }

    /// <summary>
    /// Spawns a map bound at a specific location and size.
    /// </summary>
    /// <param name="offset">
    /// Position of the center of the bound relative to the
    /// top left corner of the map
    /// </param>
    /// <param name="size">
    /// Size of the bound.
    /// </param>
    private void SpawnBound(SuperMap map, Vector3 offset, Vector2 size)
    {
        GameObject bound = GameObject.Instantiate(mapBound, map.transform);
        bound.transform.position = map.transform.position + offset;
        BoxCollider2D collider = bound.GetComponent<BoxCollider2D>();
        collider.size = size;
    }

    private void SpawnBounds(SuperMap map)
    {
        Vector3 offset = Vector3.zero;
        Vector3 size = Vector3.zero;

        offset.x = -width / 2;
        offset.y = -map.m_Height / 2;
        size = new Vector2(width, map.m_Height);
        SpawnBound(map, offset, size); // Left

        offset.x += width + map.m_Width;
        SpawnBound(map, offset, size); // Right

        offset.x = map.m_Width / 2;
        offset.y = width / 2;
        size = new Vector2(map.m_Width, width);
        SpawnBound(map, offset, size); // Top

        offset.y -= width + map.m_Height;
        SpawnBound(map, offset, size); // Right

        cameraBound.SetPath(0, new[]
        {
            MapCoords.MapToWorld(map, new Vector2(0, 0)),
            MapCoords.MapToWorld(map, new Vector2(map.m_Width, 0)),
            MapCoords.MapToWorld(map, new Vector2(map.m_Width, map.m_Height)),
            MapCoords.MapToWorld(map, new Vector2(0, map.m_Height))
        });

        confiner.InvalidatePathCache(); // Let's cinemachine know that the bound changed
    }
}

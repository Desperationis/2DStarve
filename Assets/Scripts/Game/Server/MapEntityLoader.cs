using UnityEngine;
using SuperTiled2Unity;
using Bolt;

/// <summary>
/// Dynamically instantiates the entities of a Tiled Map.
/// </summary>
public class MapEntityLoader : MonoBehaviour
{
    [SerializeField]
    private MapLoader mapLoader = null;

    private void Awake()
    {
        mapLoader.AddLoadListener(SpawnPrefabs);
        tree = (GameObject) Resources.Load("Objects/Tree");
        campfire = (GameObject) Resources.Load("Objects/Campfire");
    }

    /// <summary>
    /// Loads all entities of a specific layer
    /// </summary>
    /// <param name="layer">SuperTiled2Unity Layer</param>
    /// <param name="mob">PrefabId of the mob</param>
    private void LoadLayer(SuperMap map, SuperObjectLayer layer, PrefabId mob)
    {
        SuperObject[] entities = layer.GetComponentsInChildren<SuperObject>();

        foreach (SuperObject entity in entities)
        {
            // Spawn mobs relative to map
            Vector2 entityPosition = new Vector3(entity.m_X, entity.m_Y);
            BoltNetwork.Instantiate(mob, MapCoords.EntityToWorld(map, entityPosition), Quaternion.identity);
        }
    }


    private GameObject tree = null;
    private GameObject campfire = null;

    /// <summary>
    /// TEMP for non-bolt entities; Will be replaced in the future
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="clone"></param>
    private void LoadTempLayer(SuperMap map, SuperObjectLayer layer, GameObject clone)
    {
        SuperObject[] entities = layer.GetComponentsInChildren<SuperObject>();

        foreach (SuperObject entity in entities)
        {
            // Spawn mobs relative to map
            Vector2 entityPosition = new Vector3(entity.m_X, entity.m_Y);
            GameObject.Instantiate(clone, MapCoords.EntityToWorld(map, entityPosition), Quaternion.identity);
        }
    }

    private void SpawnPrefabs(SuperMap map)
    {
        // Only spawn prefabs server-side
        if(BoltNetwork.IsServer)
        {
            SuperObjectLayer[] layers = map.GetComponentsInChildren<SuperObjectLayer>();
            foreach(SuperObjectLayer layer in layers)
            {
                switch (layer.m_TiledName)
                {
                    case "Wolf":
                        LoadLayer(map, layer, BoltPrefabs.Wolf);
                        break;
                    case "ForestBat":
                        LoadLayer(map, layer, BoltPrefabs.ForestBat);
                        break;
                    case "Rabbit":
                        LoadLayer(map, layer, BoltPrefabs.Rabbit);
                        break;
                    case "Tree":
                        LoadTempLayer(map, layer, tree);
                        break;
                    case "Campfire":
                        LoadTempLayer(map, layer, campfire);
                        break;
                }

            }
        }
        
        if(BoltNetwork.IsClient)
        {
            SuperObjectLayer[] layers = map.GetComponentsInChildren<SuperObjectLayer>();
            foreach(SuperObjectLayer layer in layers)
            {
                switch (layer.m_TiledName)
                {
                    case "Tree":
                        LoadTempLayer(map, layer, tree);
                        break;
                    case "Campfire":
                        LoadTempLayer(map, layer, campfire);
                        break;
                }

            }
        }
    }
}

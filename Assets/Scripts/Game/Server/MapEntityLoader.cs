using UnityEngine;
using SuperTiled2Unity;
using Bolt;

/// <summary>
/// Dynamically instantiates the entities of a Tiled Map.
/// </summary>
public class MapEntityLoader : MonoBehaviour
{
    [SerializeField]
    private SuperMap map = null;

    /// <summary>
    /// Loads all entities of a specific layer
    /// </summary>
    /// <param name="layer">SuperTiled2Unity Layer</param>
    /// <param name="mob">PrefabId of the mob</param>
    private void LoadLayer(SuperObjectLayer layer, PrefabId mob)
    {
        SuperObject[] entities = layer.GetComponentsInChildren<SuperObject>();

        foreach (SuperObject entity in entities)
        {
            // Spawn mobs relative to map
            Vector3 position = new Vector3(entity.m_X / map.m_TileWidth, -entity.m_Y / map.m_TileHeight);
            position += map.transform.position;

            BoltNetwork.Instantiate(mob, position, Quaternion.identity);
        }
    }


    private GameObject tree = null;
    private GameObject campfire = null;

    /// <summary>
    /// TEMP for non-bolt entities; Will be replaced in the future
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="clone"></param>
    private void LoadTempLayer(SuperObjectLayer layer, GameObject clone)
    {
        SuperObject[] entities = layer.GetComponentsInChildren<SuperObject>();

        foreach (SuperObject entity in entities)
        {
            // Spawn mobs relative to map
            Vector3 position = new Vector3(entity.m_X / map.m_TileWidth, -entity.m_Y / map.m_TileHeight);
            position += map.transform.position;

            GameObject.Instantiate(clone, position, Quaternion.identity);
        }
    }

    private void Awake()
    {
        tree = (GameObject) Resources.Load("Objects/Tree");
        campfire = (GameObject) Resources.Load("Objects/Campfire");
    }


    void Start()
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
                        LoadLayer(layer, BoltPrefabs.Wolf);
                        break;
                    case "ForestBat":
                        LoadLayer(layer, BoltPrefabs.ForestBat);
                        break;
                    case "Rabbit":
                        LoadLayer(layer, BoltPrefabs.Rabbit);
                        break;
                    case "Tree":
                        LoadTempLayer(layer, tree);
                        break;
                    case "Campfire":
                        LoadTempLayer(layer, campfire);
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
                        LoadTempLayer(layer, tree);
                        break;
                    case "Campfire":
                        LoadTempLayer(layer, campfire);
                        break;
                }

            }
        }
    }
}

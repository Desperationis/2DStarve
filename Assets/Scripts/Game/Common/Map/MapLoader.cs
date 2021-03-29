using UnityEngine;
using UnityEngine.Events;
using SuperTiled2Unity;
using System.Collections.Generic;
using System.Collections;

public class MapLoader : MonoBehaviour
{
    private Dictionary<string, GameObject> maps = new Dictionary<string, GameObject>();

    private GameObject currentMap = null;

    private class MapLoadEvent : UnityEvent<SuperMap> { }

    private MapLoadEvent onMapLoad = new MapLoadEvent();

    private void Start()
    {
        LoadMap("Map1_3282021");
    }

    /// <summary>
    /// Given a name, load a map.
    /// </summary>
    /// <param name="name"></param>
    private void LoadMap(string name)
    {
        if(!maps.ContainsKey(name))
        {
            maps[name] = (GameObject)Resources.Load(string.Format("Map/{0}", name));
            maps[name].transform.position = Vector3.zero;
        }

        currentMap = GameObject.Instantiate(maps[name], Vector3.zero, Quaternion.identity);
        onMapLoad.Invoke(currentMap.GetComponent<SuperMap>());
    }

    /// <summary>
    /// Adds a listener to onMapLoad.
    /// </summary>
    public void AddLoadListener(UnityAction<SuperMap> call)
    {
        onMapLoad.AddListener(call);
    }

    public SuperMap GetCurrentMap()
    {
        if(currentMap != null)
        {
            return currentMap.GetComponent<SuperMap>();
        }
        return null;
    }
}

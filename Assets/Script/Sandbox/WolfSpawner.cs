using UnityEngine;
using Bolt;

public class WolfSpawner : MonoBehaviour
{
    float timer = 0.0f;
    float delay = 2.0f;

    public void Awake()
    {
        timer = Time.time + delay;
    }

    public void Update()
    {
        if (BoltNetwork.IsServer)
        {
            if (Time.time > timer)
            {
                timer = Time.time + delay;
                BoltNetwork.Instantiate(BoltPrefabs.Wolf, Vector3.zero, Quaternion.identity);
            }
        }
    }
}

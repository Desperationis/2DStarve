using UnityEngine;
using Bolt;

public class WolfSpawner : MonoBehaviour
{
    [SerializeField]
    private int hardLimit = 5;

    [SerializeField]
    private float timer = 0.0f;

    [SerializeField]
    private float delay = 2.0f;

    [SerializeField]
    [ReadOnly]
    private int count = 0;

    public void Awake()
    {
        timer = Time.time + delay;
    }

    public void Update()
    {
        if (BoltNetwork.IsServer && count < hardLimit)
        {
            if (Time.time > timer)
            {
                timer = Time.time + delay;

                if(count == 0)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.Rabbit, Vector3.zero, Quaternion.identity);
                }
                if (count == 1)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.ForestBat, Vector3.zero, Quaternion.identity);
                }
                if (count == 2)
                {
                    BoltNetwork.Instantiate(BoltPrefabs.Wolf, Vector3.zero, Quaternion.identity);
                }

                count++;
            }
        }
    }
}

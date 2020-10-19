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
    private int count;

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
                BoltNetwork.Instantiate(BoltPrefabs.Wolf, Vector3.zero, Quaternion.identity);
                count++;
            }
        }
    }
}

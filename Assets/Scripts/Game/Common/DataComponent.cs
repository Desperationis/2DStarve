using UnityEngine;


/// <summary>
/// Component that provides a SAFE reference to a mob's data.
/// </summary>
public class DataComponent : MonoBehaviour
{
    [SerializeField]
    private MobMovementData originalMovementData;

    [SerializeField]
    private MobAIData originalAIData;

    public MobMovementData movementData { get; private set; }
    public MobAIData aiData { get; private set; }

    public void Awake()
    {
        movementData = originalMovementData != null ? Object.Instantiate(originalMovementData) : null;
        aiData = originalAIData != null ? Object.Instantiate(originalAIData) : null;
    }
}

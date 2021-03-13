using UnityEngine;

[CreateAssetMenu(fileName = "New Mob Data", menuName = "MobMovementData")]
public class MobMovementData : ScriptableObject
{
    // Movement Data
    public float speed;
    public float runningMultiplier;
}

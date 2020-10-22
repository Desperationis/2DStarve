using UnityEngine;

[CreateAssetMenu(fileName = "New Mob Data", menuName = "MobSpeedData")]
public class MobSpeedData : ScriptableObject
{
    // Movement Data
    public float speed;
    public float runningMultiplier;
}

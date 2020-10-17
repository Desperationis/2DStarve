using UnityEngine;

[CreateAssetMenu(fileName = "New Mob Data", menuName = "MobData", order = 1)]
public class MobData : ScriptableObject
{
    // AI data.
    public float followerRange;
    public bool runAtFollow;
    public float attackRange;
    public float idleRestTime;
    public float idleMoveTime;

    // Movement Data
    public float speed;
    public float runningMultiplier;
}

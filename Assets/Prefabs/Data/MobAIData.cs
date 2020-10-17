using UnityEngine;

[CreateAssetMenu(fileName = "New Mob Data", menuName = "MobAIData")]
public class MobAIData : ScriptableObject
{
    // AI data.
    public float followerRange;
    public bool runAtFollow;
    public float attackRange;
    public float idleRestTime;
    public float idleMoveTime;
}

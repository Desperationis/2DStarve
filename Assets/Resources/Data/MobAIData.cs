using UnityEngine;

[CreateAssetMenu(fileName = "New Mob Data", menuName = "MobAIData")]
public class MobAIData : ScriptableObject
{
    [Header("Idle AI")]
    [Tooltip("Number of seconds that this mob will rest for before moving.")]
    public float idleRestTime;
    [Tooltip("Number of seconds that this mob will move for before resting.")]
    public float idleMoveTime;

    [Header("Follower AI")]
    [Tooltip("The radius used to search for players in unity units.")]
    public float followerRange;
    [Tooltip("Whether or not to run at a player when following them")]
    public bool runAtFollow;

    [Header("Attack AI")]
    [Tooltip("The radius used to search for players in unity units.")]
    public float attackRange;
}

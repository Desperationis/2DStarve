using UnityEngine;

public class MobVariableController : MonoBehaviour
{
    [SerializeField]
    private BoltEntity entity;

    [SerializeField]
    private float _speed = 0.0f;

    [SerializeField]
    private float _runningMultiplier = 0.0f;

    [SerializeField]
    private int _health = 0;

    public void Start()
    {
        // Self-destruct if this is a client
        if (BoltNetwork.IsClient)
        {
            Destroy(this);
        }
    }

    [ContextMenu("Send Changes")]
    private void SendChanges()
    {
        EntityVariableChangeEvent variableChangeEvent = EntityVariableChangeEvent.Create(entity);
        variableChangeEvent.Speed = _speed;
        variableChangeEvent.RunningMultiplier = _runningMultiplier;
        variableChangeEvent.Health = _health;
        variableChangeEvent.Send();
    }

}

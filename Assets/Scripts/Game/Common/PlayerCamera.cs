using UnityEngine;
using Cinemachine;

/// <summary>
/// Singleton dedicated to following ONLY 1 player via camera.
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    #region Singleton
    private static PlayerCamera _instance = null;

    public static PlayerCamera Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    [SerializeField]
    private new CinemachineVirtualCamera camera = null;

    private BoltEntity controlledPlayer = null;

    private void Start()
    {
        ControlledEntity.AddListener(Follow);
    }

    public void Follow(BoltEntity controlledPlayer)
    {
        camera.Follow = controlledPlayer.transform;
    }
}

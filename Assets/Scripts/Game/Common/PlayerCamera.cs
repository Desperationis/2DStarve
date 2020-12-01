using UnityEngine;

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
    private Vector2 offCenter;


    [SerializeField]
    private new Camera camera = null;

    [SerializeField]
    [ReadOnly]
    private BoltEntity controlledPlayer = null;

    private void Start()
    {
        ControlledEntity.onControlledEntitySwitch.AddListener(Follow);
    }

    public void Follow(BoltEntity controlledPlayer)
    {
        this.controlledPlayer = controlledPlayer;
    }

    private void LateUpdate()
    {
        if(controlledPlayer != null)
        {
            Vector3 newCameraPosition;
            newCameraPosition.x = controlledPlayer.transform.position.x + offCenter.x;
            newCameraPosition.y = controlledPlayer.transform.position.y + offCenter.y;
            newCameraPosition.z = transform.position.z;

            camera.transform.position = newCameraPosition;
        }
    }
}

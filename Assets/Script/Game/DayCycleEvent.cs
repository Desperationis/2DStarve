using UnityEngine;
using UnityEngine.Events;

public class DayCycleEvent : MonoBehaviour
{
    #region Singleton Region

    private static DayCycleEvent _instance;

    public static DayCycleEvent Instance { get { return _instance; } }

    /// <summary>
    /// On creation of another DayCycleEvent, self-destruct it if another
    /// instance of it exists.
    /// </summary>
    private void Awake()
    {
        // If this instance isn't the singleton instance, delete it.
        if (_instance != this && _instance != null)
        {
            Debug.LogWarningFormat("DayCycleEvent.cs: \"{0}\" was deleted, as one already exists.", name);
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    #endregion

    [SerializeField]
    private Color dayColor;

    [SerializeField]
    private Color nightColor;

    private float timer;


    [System.Serializable]
    public class CycleEvent : UnityEvent<Color> { };

    public CycleEvent onCycle = new CycleEvent();

    [ContextMenu("Set Daytime")]
    void SetDaytime()
    {
        onCycle.Invoke(dayColor);
    }

    [ContextMenu("Set Nighttime")]
    void SetNighttime()
    {
        onCycle.Invoke(nightColor);
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 0.1f)
        {
            timer = 0.0f;

            onCycle.Invoke(Color.Lerp(dayColor, nightColor, Mathf.Cos(Time.time * 0.05f) / 2 + 0.5f));
        }
    }
}

using UnityEngine;
using UnityEngine.Tilemaps;

public class DayCycleTint : MonoBehaviour
{
    [SerializeField]
    private new SpriteRenderer renderer;

    [SerializeField]
    private Tilemap tilemap;

    public void Start()
    {
        DayCycleEvent.Instance.onCycle.AddListener(SetTint);
    }

    public void SetTint(Color color)
    {
        if (renderer != null)
        {
            renderer.color = color;
        }
        else if (tilemap != null)
        {
            tilemap.color = color;
        }
    }
}

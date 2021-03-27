using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtSync : MonoBehaviour
{
    public SpriteRenderer parent = null;
    public SpriteRenderer thisThing = null;
    private Dictionary<string, Sprite> spritesheet = new Dictionary<string, Sprite>();

    // Update is called once per frame
    void Awake()
    {
        foreach(var sprite in Resources.LoadAll<Sprite>("Spritesheets/Shirt"))
        {
            spritesheet[sprite.name] = sprite;
        }
    }

    private void LateUpdate()
    {
        var sprite = spritesheet[parent.sprite.name];
        thisThing.sprite = sprite;
        thisThing.flipX = parent.flipX;
    }
}

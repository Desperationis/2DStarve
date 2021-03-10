using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monobehaviour-derived class that exposes
/// an easy reference to a Mob's MobController.
/// </summary>
public class MobBehaviour : MonoBehaviour
{
    protected MobController mobController = null;

    protected virtual void Awake()
    {
        mobController = GetComponent<MobController>();
    }
}

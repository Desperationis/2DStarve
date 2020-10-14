using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class WolfTestScript : Bolt.EntityBehaviour<IMobState>
{
    public float range = 2.0f;
    public float speed = 3.0f;

    public override void Attached()
    {
        state.SetTransforms(state.PositionTransform, transform);
    }

    public override void SimulateOwner()
    {
        foreach(var player in PlayerRegistry.AllPlayers)
        {
            Vector3 position = player.character.gameObject.transform.position;
            Vector3 difference = position - transform.position;

            if(difference.sqrMagnitude < Mathf.Pow(range, 2))
            {
                transform.position += difference.normalized * speed * BoltNetwork.FrameDeltaTime;
                break; 
            }

        }
    }
}

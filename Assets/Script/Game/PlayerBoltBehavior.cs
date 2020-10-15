using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class PlayerBoltBehavior : Bolt.EntityBehaviour<IPlayerState>
{
    Vector2 movement;
    bool attacked = false;

    public MobController mobController;

    public override void Attached()
    {
        state.SetTransforms(state.PositionTransform, transform);
        state.AddCallback("Velocity", VelocityUpdate);
    }

    private void VelocityUpdate()
    {
        //mobController.ForceVelocity(state.Velocity);
    }

    private void Update()
    {
        if(entity.IsControllerOrOwner)
        {
            //mobController.UpdateFrame();
        }
    }

    private void PollInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    public override void SimulateController()
    {
        // This is the client's and server's player's Update();
        PollInput();
        IPlayerMovementAuthInput input = PlayerMovementAuth.Create();
        input.Direction = movement.normalized;

        input.Attack = Input.GetKey(KeyCode.Space) && !attacked;
        attacked = Input.GetKey(KeyCode.Space);

        entity.QueueInput(input);
    }

    // Execute a command on both the controller and owner
    public override void ExecuteCommand(Command command, bool resetState)
    {
        // Bolt saves a query of inputs and slowly fixes the divergence of the
        // client to the cerser's position. This is done with resetState.
        PlayerMovementAuth cmd = (PlayerMovementAuth)command;

        if (resetState)
        {
            // If the client goes to far, rewind it back to original position.
            transform.position = cmd.Result.Position;
            mobController.ForceVelocity(cmd.Result.Velocity);
        }
        else
        {
            // Move the entity on both the client and server.

            // Physics here
            mobController.SetDirection(cmd.Input.Direction);
            mobController.UpdateFrame();

            if(BoltNetwork.IsServer)
            {
                state.Velocity = mobController.Velocity;
            }

            cmd.Result.Position = transform.position;
            cmd.Result.Velocity = mobController.Velocity;

            if(cmd.Input.Attack)
            {
                Bolt.LagCompensation.BoltPhysicsHits hits = BoltNetwork.OverlapSphereAll(cmd.Result.Position, 2.0f, cmd.ServerFrame);

                for(int i = 0; i < hits.count; i++)
                {
                    Debug.Log(string.Format("Hit {0}!", hits[i].body.name));
                }
            }
        }

    }
}

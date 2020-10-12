using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class PlayerBoltBehavior : Bolt.EntityBehaviour<IPlayerState>
{
    int speed = 4;
    Vector2 movement;


    public override void Attached()
    {
        state.SetTransforms(state.PositionTransform, transform);
    }

    private void PollInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void Update()
    {
        // Poll Server and Client for input
        PollInput();
    }

    public override void SimulateController()
    {
        // This is the client's Update();
        PollInput();
        IPlayerMovementAuthInput input = PlayerMovementAuth.Create();
        input.Direction = movement.normalized;
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
            transform.position = cmd.Result.Position;
        }
        else
        {
            // Move the entity if the application owns it; Updates every frame.
            transform.position = transform.position + (cmd.Input.Direction * speed * BoltNetwork.FrameDeltaTime);

            cmd.Result.Position = transform.position;
        }

    }
}

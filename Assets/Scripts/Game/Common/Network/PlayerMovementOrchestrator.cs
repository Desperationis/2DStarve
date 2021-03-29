using UnityEngine;
using Bolt;

/// <summary>
/// Syncs most variables for a player from the server to the clients while also
/// using client-side prediction. Due to this, this is the only script that can
/// move the player.
/// </summary>
public class PlayerMovementOrchestrator : MovementOrchestrator<IPlayerState>
{
    [SerializeField]
    private Rigidbody2D rigidBody = null;

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);

        // Syncronize mobController settings on clients and server.
        state.AddCallback("Direction", DirectionUpdate);
        state.AddCallback("Running", RunningUpdate);
    }

    protected override void DirectionUpdate()
    {
        mobController.SetDirection(state.Direction);
    }

    protected override void RunningUpdate()
    {
        mobController.SetRunning(state.Running);
    }

    private void Update()
    {
        if (!entity.IsControllerOrOwner && !BoltNetwork.IsServer)
        {
            rigidBody.velocity = new Vector2(0, 0);
        }
    }

    public override void ExecuteCommand(Command command, bool resetState)
    {
        // Bolt saves a query of inputs and slowly fixes the divergence of the
        // client to the server's position. This is done with resetState.
        PlayerMovementAuth cmd = (PlayerMovementAuth)command;

        if (resetState)
        {
            // If the client goes to far, rewind it back to original position.
            transform.position = cmd.Result.Position;
            rigidBody.velocity = cmd.Result.Velocity;
        }
        else
        {
            // Move the entity on both the client and server; Client-side prediction
            mobController.SetDirection(cmd.Input.Direction);
            mobController.SetRunning(cmd.Input.Running);

            if(cmd.Input.MovementLocked)
            {
                mobController.DisableMovement();
            }
            else
            {
                mobController.EnableMovement();
            }

            if(BoltNetwork.IsServer)
            {
                state.Direction = mobController.direction;
                state.Running = mobController.isRunning;
            }

            mobController.UpdateFrame(BoltNetwork.FrameDeltaTime);

            if(BoltNetwork.IsClient)
            {
                Physics2D.autoSimulation = false;
                Physics2D.Simulate(Time.deltaTime);
            }

            cmd.Result.Position = transform.position;
            cmd.Result.Velocity = rigidBody.velocity;
        }

    }
}

﻿using UnityEngine;
using Bolt;

public class PlayerBoltBehavior : Bolt.EntityBehaviour<IPlayerState>
{
    [SerializeField]
    private MobController mobController;

    private bool spacePressed = false;

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);

        // Syncronize mobController settings on clients and server. 
        state.AddCallback("Speed", SpeedUpdate);
        state.AddCallback("RunningMultiplier", RunningMultUpdate);
        state.AddCallback("Direction", DirectionUpdate);
        state.AddCallback("Running", RunningUpdate);
    }

    private void DirectionUpdate()
    {
        mobController.SetDirection(state.Direction);
    }

    private void RunningUpdate()
    {
        mobController.SetRunning(state.Running);
    }

    private void SpeedUpdate()
    {
        mobController.SetSpeed(state.Speed);
    }

    private void RunningMultUpdate()
    {
        mobController.SetRunningMultiplier(state.RunningMultiplier);
    }

    public override void SimulateController()
    {
        // This is the client's and server's player's Update();
        IPlayerMovementAuthInput commandInput = PlayerMovementAuth.Create();

        Vector2 inputDirection;
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");
        commandInput.Direction = inputDirection.normalized;

        commandInput.Attack = Input.GetKey(KeyCode.Space) && !spacePressed;
        spacePressed = Input.GetKey(KeyCode.Space);

        commandInput.Running = Input.GetKey(KeyCode.LeftShift);

        entity.QueueInput(commandInput);
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
        }
        else
        {
            // Move the entity on both the client and server; Client-side prediction
            mobController.SetDirection(cmd.Input.Direction);
            mobController.SetRunning(cmd.Input.Running);

            state.Direction = mobController.Direction;
            state.Running = mobController.Running;

            mobController.UpdateFrame(BoltNetwork.FrameDeltaTime);

            cmd.Result.Position = transform.position;

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

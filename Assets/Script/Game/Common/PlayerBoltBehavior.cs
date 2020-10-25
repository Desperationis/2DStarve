﻿using UnityEngine;
using Bolt;

/// <summary>
/// Syncs most variables for a player from the server to the clients while
/// also using client-side prediction. Variables that are extensively
/// manipulated (e.x. health) have their own class.
/// </summary>
public class PlayerBoltBehavior : Bolt.EntityBehaviour<IPlayerState>
{
    [SerializeField]
    [Tooltip("Used to syncronize movement settings.")]
    private MobController mobController = null;

    [SerializeField]
    private PlayerAttack playerAttack;

    private bool _spacePressed = false;
    private bool _lockPlayer = false;

    /// <summary>
    /// If true, client will send input commands that will cause
    /// the player to stand still and be locked out of input.
    /// </summary>
    public void LockPlayer(bool lockPlayer)
    {
        _lockPlayer = lockPlayer;
    }

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
        if(!_lockPlayer)
        {
            // Bundle up all input commands into an authoritive command.
            IPlayerMovementAuthInput commandInput = PlayerMovementAuth.Create();

            Vector2 inputDirection;
            inputDirection.x = Input.GetAxisRaw("Horizontal");
            inputDirection.y = Input.GetAxisRaw("Vertical");

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                Vector2 position = touch.position;
                inputDirection = position - new Vector2(Screen.width / 2, Screen.height / 2);
            }


            commandInput.Direction = inputDirection.normalized;

            commandInput.Attack = Input.GetKey(KeyCode.Space) && !_spacePressed;
            _spacePressed = Input.GetKey(KeyCode.Space);

            commandInput.Running = Input.GetKey(KeyCode.LeftShift);

            commandInput.MovementLocked = playerAttack.requestedLock;

            entity.QueueInput(commandInput);
        }
        else
        {
            IPlayerMovementAuthInput commandInput = PlayerMovementAuth.Create();
            commandInput.Direction = Vector2.zero;
            commandInput.Attack = false;
            commandInput.Running = false;

            entity.QueueInput(commandInput);
        }
    }

    public override void ExecuteCommand(Command command, bool resetState)
    {
        // Bolt saves a query of inputs and slowly fixes the divergence of the
        // client to the cerser's position. This is done with resetState.
        PlayerMovementAuth cmd = (PlayerMovementAuth)command;

        if (resetState)
        {
            // If the client goes to far, rewind it back to original position.
            transform.position = cmd.Result.Position;
            //mobController.SetMovementLock(cmd.Result);
        }
        else
        {
            // Move the entity on both the client and server; Client-side prediction
            mobController.SetDirection(cmd.Input.Direction);
            mobController.SetRunning(cmd.Input.Running);
            mobController.SetMovementLock(cmd.Input.MovementLocked);

            if(BoltNetwork.IsServer)
            {
                state.Direction = mobController.Direction;
                state.Running = mobController.Running;
            }

            mobController.UpdateFrame(BoltNetwork.FrameDeltaTime);

            cmd.Result.Position = transform.position;
        }

    }
}

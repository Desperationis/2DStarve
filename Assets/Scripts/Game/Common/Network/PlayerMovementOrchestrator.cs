using UnityEngine;
using Bolt;

/// <summary>
/// Syncs most variables for a player from the server to the clients while
/// also using client-side prediction. Due to this, this is the only script
/// that can move the player. 
/// </summary>
public class PlayerMovementOrchestrator : MovementOrchestrator<IPlayerState>
{
    private bool lockPlayer = false;
    private bool spacePressed = false;


    /// <summary>
    /// If true, client will override all requested inputs to make 
    /// the player stand still. 
    /// </summary>
    public void OverrideStop(bool lockPlayer)
    {
        this.lockPlayer = lockPlayer;
    }

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

    public override void SimulateController()
    {
        if(!lockPlayer)
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

            commandInput.Attack = Input.GetKey(KeyCode.Space) && !spacePressed;
            spacePressed = Input.GetKey(KeyCode.Space);

            commandInput.Running = Input.GetKey(KeyCode.LeftShift);

            commandInput.MovementLocked = GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"); ;

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

            cmd.Result.Position = transform.position;
        }

    }
}

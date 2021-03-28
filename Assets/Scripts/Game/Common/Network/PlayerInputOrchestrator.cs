using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class PlayerInputOrchestrator : NetworkOrchestrator<IPlayerState>
{
    private bool lockPlayer = false;
    private bool spacePressed = false;

    /// <summary>
    /// If true, client will override all requested inputs to their default state 
    /// so that the player is idle; Useful for staying still while looking at UI.
    /// </summary>
    public void OverrideStop(bool lockPlayer)
    {
        this.lockPlayer = lockPlayer;
    }

    public override void SimulateController()
    {
        if (!lockPlayer)
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
}

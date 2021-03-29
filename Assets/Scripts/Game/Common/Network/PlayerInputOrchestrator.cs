using UnityEngine;

public class PlayerInputOrchestrator : NetworkOrchestrator<IPlayerState>
{
    [SerializeField]
    [Tooltip("Used to determine if the player is currently attacking.")]
    private AttackingComponent attackingComponent = null;

    private bool lockPlayer = false;
    private bool spacePressed = false;

    /// <summary>
    /// If true, client will override all requested inputs to their default
    /// state  so that the player is idle; Useful for staying still while
    /// looking at UI.
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

            commandInput.MovementLocked = attackingComponent.isAttacking;

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

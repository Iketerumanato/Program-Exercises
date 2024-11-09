using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}

public abstract class MoveCommand : ICommand
{
    protected CommandTest commandTest;
    protected Vector3 moveDirection;

    public MoveCommand(CommandTest commandTest, Vector3 moveDirection)
    {
        this.commandTest = commandTest;
        this.moveDirection = moveDirection;
    }

    public abstract void Execute();

    public void Undo()
    {
        ExecuteMove(-moveDirection);
    }

    protected abstract void ExecuteMove(Vector3 direction);
}

public class MoveCmd2D : MoveCommand
{
    public MoveCmd2D(CommandTest commandTest, float moveDistance)
        : base(commandTest, new Vector2(moveDistance, 0)) { }

    public override void Execute()
    {
        ExecuteMove(moveDirection);
    }

    protected override void ExecuteMove(Vector3 direction)
    {
        commandTest.PlayerMove2D(direction.x);
    }
}

public class MoveCmd3D : MoveCommand
{
    public MoveCmd3D(CommandTest commandTest, Vector3 moveDirection)
        : base(commandTest, moveDirection) { }

    public override void Execute()
    {
        ExecuteMove(moveDirection);
    }

    protected override void ExecuteMove(Vector3 direction)
    {
        commandTest.PlayerMove3D(direction);
    }
}

public class JumpCmd : ICommand
{
    private CommandTest commandTest;
    private Vector2 originalPosition;

    public JumpCmd(CommandTest commandTest)
    {
        this.commandTest = commandTest;
        originalPosition = commandTest.transform.position;
    }

    public void Execute()
    {
        commandTest.Jump();
    }

    public void Undo()
    {
        commandTest.transform.position = originalPosition;
    }
}

public class CommandTest : MonoBehaviour
{
    private Stack<ICommand> commandHistory = new();
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Rigidbody2D playerRig2D;
    [SerializeField] Rigidbody playerRig;
    [SerializeField] Transform groundCheckObj;
    [SerializeField] float groundCheckRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;
    private bool wasGrounded2d;
    private bool wasGrounded3d;
    [SerializeField] ShakeEffect shakeEffect;

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandHistory.Push(command); // 実行したコマンドを履歴に保存
    }

    public void UndoCommand()
    {
        if (commandHistory.Count > 0)
        {
            ICommand lastCommand = commandHistory.Pop();
            lastCommand.Undo();
            Debug.LogWarning("コマンドをUndoします");
        }
    }

    public void PlayerMove2D(float moveInput)
    {
        playerRig2D.velocity = new Vector2(moveInput * moveSpeed, playerRig2D.velocity.y);
    }

    public void PlayerMove3D(Vector3 moveDirection)
    {
        playerRig.velocity = new Vector3(moveDirection.x * moveSpeed, playerRig.velocity.y, moveDirection.z * moveSpeed);
    }

    public void Jump()
    {
        if (IsGrounded3D()) playerRig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        if (IsGround2D()) playerRig2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded3D()
    {
        return Physics.CheckSphere(groundCheckObj.position, groundCheckRadius, groundLayer);
    }

    private bool IsGround2D()
    {
        return Physics2D.OverlapCircle(groundCheckObj.position, groundCheckRadius, groundLayer) != null;
    }

    private void Update()
    {
        bool isGrounded3d = IsGrounded3D();
        bool isGrounded2d = IsGround2D();

        if (!wasGrounded2d && isGrounded2d)
        {
            TriggerShakeEffect();
        }

        if (!wasGrounded3d && isGrounded3d)
        {
            TriggerShakeEffect();
        }

        wasGrounded2d = isGrounded2d;
        wasGrounded3d = isGrounded3d;
    }

    private void TriggerShakeEffect()
    {
        if (shakeEffect.isScreenShake)
            shakeEffect.ScreenShaker(shakeEffect.shakeScreenIntensity, shakeEffect.shakeScreenDuration);

        if (shakeEffect.isCameraShake)
            shakeEffect.CameraShaker();
    }
}
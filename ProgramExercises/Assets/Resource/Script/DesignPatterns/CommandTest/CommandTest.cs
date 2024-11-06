using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class MoveCmd2D : ICommand
{
    private CommandTest _commandTest;
    private float moveDistance;

    public MoveCmd2D(CommandTest character, float moveDistance)
    {
        this._commandTest = character;
        this.moveDistance = moveDistance;
    }

    //コマンドの実行
    public void Execute()
    {
        _commandTest.PlayerMove2D(moveDistance);
    }

    //コマンドの取り消し(元の位置に戻す)
    public void Undo()
    {
        _commandTest.PlayerMove2D(-moveDistance);
    }
}

public class MoveCmd3D : ICommand
{
    private CommandTest _commandTest;
    private Vector3 moveDirection;

    public MoveCmd3D(CommandTest commandTest, Vector3 moveDirection)
    {
        this._commandTest = commandTest;
        this.moveDirection = moveDirection;
    }

    public void Execute()
    {
        _commandTest.PlayerMove3D(moveDirection);
    }

    public void Undo()
    {
        _commandTest.PlayerMove3D(-moveDirection);
    }
}

public class JumpCmd : ICommand
{
    private CommandTest _commandTest;
    private Vector2 originalPosition;

    public JumpCmd(CommandTest _commandTest)
    {
        this._commandTest = _commandTest;
        originalPosition = _commandTest.transform.position;
    }

    public void Execute()
    {
        _commandTest.Jump();
    }

    public void Undo()
    {
        _commandTest.transform.position = originalPosition;
    }
}

public class CommandTest : MonoBehaviour
{
    private Stack<ICommand> commandHistory = new();
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Rigidbody2D playerRig2D;
    [SerializeField] Rigidbody playerRig;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.1f;
    [SerializeField] LayerMask groundLayer;
    private bool wasGrounded;
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
        if (IsGrounded())
        {
            playerRig2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerRig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
        //return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Update()
    {
        bool isGrounded = IsGrounded();

        if (!wasGrounded && isGrounded)
        {
            //画面揺れver
            //shakeEffect.StartShake(shakeEffect.shakeScreenIntensity, shakeEffect.shakeScreenDuration);

            //カメラ揺れver
            shakeEffect.CameraShaker();
        }

        wasGrounded = isGrounded;
    }
}
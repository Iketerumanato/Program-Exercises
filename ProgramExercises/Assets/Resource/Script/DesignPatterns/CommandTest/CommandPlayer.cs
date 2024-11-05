using UnityEngine;

public class CommandPlayer : MonoBehaviour
{
    [SerializeField] CommandTest _commandTest;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ICommand moveLeft = new MoveCmd(_commandTest, -1f);
            _commandTest.ExecuteCommand(moveLeft);
        }

        // Dキーで右に移動
        if (Input.GetKey(KeyCode.D))
        {
            ICommand moveRight = new MoveCmd(_commandTest, 1f);
            _commandTest.ExecuteCommand(moveRight);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ICommand jumpCommand = new JumpCmd(_commandTest);
            _commandTest.ExecuteCommand(jumpCommand);
        }

        if (Input.GetKeyDown(KeyCode.Z)) _commandTest.UndoCommand();
    }
}
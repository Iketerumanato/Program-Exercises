using UnityEngine;

public class CommandPlayer : MonoBehaviour
{
    [SerializeField] CommandTest _commandTest;

    // Update is called once per frame
    void Update()
    {

        #region 2D移動
        //if (Input.GetKey(KeyCode.A))
        //{
        //    ICommand moveLeft = new MoveCmd2D(_commandTest, -1f);
        //    _commandTest.ExecuteCommand(moveLeft);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    ICommand moveRight = new MoveCmd2D(_commandTest, 1f);
        //    _commandTest.ExecuteCommand(moveRight);
        //}
        #endregion

        #region 3D移動
        if (Input.GetKey(KeyCode.W))
        {
            ICommand moveForward = new MoveCmd3D(_commandTest, new Vector3(0, 0, 1f));
            _commandTest.ExecuteCommand(moveForward);
        }

        if (Input.GetKey(KeyCode.S))
        {
            ICommand moveBackward = new MoveCmd3D(_commandTest, new Vector3(0, 0, -1f));
            _commandTest.ExecuteCommand(moveBackward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            ICommand moveLeft = new MoveCmd3D(_commandTest, new Vector3(-1f, 0, 0));
            _commandTest.ExecuteCommand(moveLeft);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ICommand moveRight = new MoveCmd3D(_commandTest, new Vector3(1f, 0, 0));
            _commandTest.ExecuteCommand(moveRight);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ICommand jumpCommand = new JumpCmd(_commandTest);
            _commandTest.ExecuteCommand(jumpCommand);
        }

        if (Input.GetKeyDown(KeyCode.Z)) _commandTest.UndoCommand();
    }
}
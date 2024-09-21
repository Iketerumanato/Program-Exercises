using System.Collections.Generic;
using UnityEngine;

public interface IRobotComponent
{
    string Operation();
}

public class RobotBodyComponent : IRobotComponent
{
    public string Operation()
    {
        return "���{�b�g�̃p�[�c��ǉ����悤�I:\n";
    }
}

//���N���X
public class RobotDecorator : IRobotComponent
{
    protected IRobotComponent _component;

    public RobotDecorator(IRobotComponent component)
    {
        _component = component;
    }

    public virtual string Operation()
    {
        return _component.Operation();
    }
}

//�f�R���[�^�[�N���X
public class RobotPartsDecorator : RobotDecorator
{
    private string _partsName;

    public RobotPartsDecorator(IRobotComponent component, string PartsName) : base(component)
    {
        _partsName = PartsName;
    }

    public override string Operation()
    {
        return $"{base.Operation()} + {_partsName} ��ǉ�!\n";
    }
}

public class DecoratorTest : MonoBehaviour
{
    [SerializeField] List<string> PartsList = new();
    IRobotComponent robot = new RobotBodyComponent();

    void Start()
    {
        foreach (string parts in PartsList)
        {
            robot = new RobotPartsDecorator(robot, parts);
        }

        Debug.Log(robot.Operation());
    }
}
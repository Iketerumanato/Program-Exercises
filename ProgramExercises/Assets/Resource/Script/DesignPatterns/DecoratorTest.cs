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
        return "ロボットのパーツを追加しよう！:\n";
    }
}

//基底クラス
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

//デコレータークラス
public class RobotPartsDecorator : RobotDecorator
{
    private string _partsName;

    public RobotPartsDecorator(IRobotComponent component, string PartsName) : base(component)
    {
        _partsName = PartsName;
    }

    public override string Operation()
    {
        return $"{base.Operation()} + {_partsName} を追加!\n";
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
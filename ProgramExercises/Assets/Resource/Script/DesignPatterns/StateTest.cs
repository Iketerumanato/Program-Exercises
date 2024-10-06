using UnityEngine;

public interface IPlayerState
{
    void EnterState(StateTest stateTest);
    void ExitState(StateTest stateTest);
    void UpdateProcess(StateTest stateTest);
    void ActivationSkill(StateTest stateTest);
}

public class NormalState : IPlayerState
{
    public string SkillName { get { return "普通のパンチ"; } }

    public void EnterState(StateTest stateTest)
    {
        Debug.Log("通常モードに移行しました");
    }

    public void ExitState(StateTest stateTest)
    {
        Debug.Log("通常モード解除,別モードに移行しました");
    }

    public void UpdateProcess(StateTest stateTest)
    {
        stateTest.ActivePlayer();
    }

    public void ActivationSkill(StateTest stateTest)
    {
        Debug.Log($"スキル {SkillName} を使用しました");
    }

    public void Run()
    {
        Debug.Log("逃げます");
    }
}

public class AttackState : IPlayerState
{
    public string SkillName { get { return "連続普通のパンチ"; } }

    public void EnterState(StateTest stateTest)
    {
        Debug.Log("攻撃モードに移行しました");
    }

    public void ExitState(StateTest stateTest)
    {
        Debug.Log("攻撃モードを解除,別モードに移行しました");
    }

    public void UpdateProcess(StateTest stateTest)
    {
        Debug.Log("プレイヤー攻撃中...");
        Debug.LogWarning("※別クラスでの定義を推奨");
    }

    public void ActivationSkill(StateTest stateTest)
    {
        Debug.Log($"スキル {SkillName} を使用しました");
    }
}

public class DefenseState : IPlayerState
{
    public string SkillName { get { return "絶対防御"; } }

    public void EnterState(StateTest stateTest)
    {
        Debug.Log("防御モードに移行しました");
    }

    public void ExitState(StateTest stateTest)
    {
        Debug.Log("防御モード解除,別モードに移行しました");
    }

    public void UpdateProcess(StateTest stateTest)
    {
        Debug.Log("プレイヤー防御中...");
        Debug.LogWarning("※このステートでの毎フレーム処理は別クラスでの定義を推奨");
    }

    public void ActivationSkill(StateTest stateTest)
    {
        Debug.Log($"スキル {SkillName} を使用しました");
    }
}

public class StateTest : MonoBehaviour
{
    private IPlayerState currentState;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new NormalState());
        Debug.LogWarning("毎フレームの処理も同時に呼び出すように");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) TransferNormalState();
        if (Input.GetKeyDown(KeyCode.Alpha1)) TransferAttackState();
        if (Input.GetKeyDown(KeyCode.Alpha2)) TransferDefenseState();
        if (Input.GetKeyDown(KeyCode.Space)) UsageSkill();

        if (Input.GetKeyDown(KeyCode.R) && currentState is NormalState normalState) normalState.Run();
    }

    void ChangeState(IPlayerState newState)
    {
        if (currentState != null) currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
        currentState.UpdateProcess(this);
    }

    void UsageSkill()
    {
        currentState.ActivationSkill(this);
    }

    #region マイフレーム処理群
    public void ActivePlayer()
    {
        Debug.Log("プレイヤー稼働中...");
    }

    public void TransferNormalState()
    {
        ChangeState(new NormalState());
    }

    public void TransferAttackState()
    {
        ChangeState(new AttackState());
    }

    public void TransferDefenseState()
    {
        ChangeState(new DefenseState());
    }
    #endregion
}
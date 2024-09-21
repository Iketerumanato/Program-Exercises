using UnityEngine;
using System;

public class EnemyObjects : MonoBehaviour
{
    private Action _onDisable;  // ��A�N�e�B�u�����邽�߂̃R�[���o�b�N
    private float _elapsedTime;  // ����������Ă���̌o�ߎ���

    public void Initialize(Action onDisable)
    {
        _onDisable = onDisable;
        _elapsedTime = 0;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= 2)
        {
            _onDisable?.Invoke();
            gameObject.SetActive(false);
        }
    }
}

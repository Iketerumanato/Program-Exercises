using UnityEngine;
using System;

public class EnemyObject : MonoBehaviour
{
    private Action _onDisable;
    private float _elapsedTime;

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

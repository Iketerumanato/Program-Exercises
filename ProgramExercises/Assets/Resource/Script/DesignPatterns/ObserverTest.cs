using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHpObserver
{
    void OnUpdateHp(int updatePlayerHp);
}

public class PlayerHealthUI : IPlayerHpObserver
{
    public void OnUpdateHp(int updatePlayerHp)
    {
        Debug.Log($"PlayerのHpを {updatePlayerHp} に更新します");
    }
}

public class Player
{
    private int _hp;
    private List<IPlayerHpObserver> _observers = new();

    public Player(int playerHp)
    {
        _hp = playerHp;
    }

    public void RegisterObserver(IPlayerHpObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(IPlayerHpObserver observer)
    {
        _observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (IPlayerHpObserver observer in _observers)
        {
            observer.OnUpdateHp(_hp);
        }
    }

    public void ChangeHealth(int updatePlayerHp)
    {
        _hp = updatePlayerHp;
        NotifyObservers();
    }
}

public class ObserverTest : MonoBehaviour
{
    [SerializeField] int PlayerHp = 100;
    Player _player;
    PlayerHealthUI _playerHealthUI = new();

    private void Start()
    {
        _player = new(PlayerHp);
        _player.RegisterObserver(_playerHealthUI);
    }

    private void Update()
    {
        if(_player != null) _player.ChangeHealth(PlayerHp);
    }
}
using UnityEngine;

public abstract class Monster
{
    protected string Name;
    protected int Hp;

    public Monster(string MonsterName, int MonsterHp)
    {
        this.Name = MonsterName;
        this.Hp = MonsterHp;
    }

    public abstract void ExplanationMyself();
}

public class Slime : Monster
{
    public Slime(string SlimeName, int SlimeHp) : base(SlimeName, SlimeHp) { }

    public override void ExplanationMyself()
    {
        Debug.Log($"My name is {Name} : Hp = {Hp}");
    }
}

public class MonsterFactory
{
    public static Monster CreateMonster(string monsterType,string monsterName,int monsterHp)
    {
        switch(monsterType)
        {
            case "Slime":
                return new Slime(monsterName, monsterHp);

            default:
                throw new System.ArgumentException("Unknown monster type");
        }
    }
}

public class FactoryTest : MonoBehaviour
{
    Slime slime = (Slime)MonsterFactory.CreateMonster("Slime","Blue Slime", 50);

    private void Start()
    {
        slime.ExplanationMyself();
    }
}
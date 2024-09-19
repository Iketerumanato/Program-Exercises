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
    private int Smoothness;

    public Slime(string SlimeName, int SlimeHp, int smoothness) : base(SlimeName, SlimeHp)
    {
        this.Smoothness = smoothness;
    }

    public override void ExplanationMyself()
    {
        Debug.Log($"僕の名前は {Name} : Hpは = {Hp} : 滑らかさは {Smoothness} だよ！");
    }
}

public class Dragon : Monster
{
    private int ScalesNum;

    public Dragon(string DragonName, int DragonHp, int scalesNum) : base(DragonName, DragonHp)
    {
        this.ScalesNum = scalesNum;
    }

    public override void ExplanationMyself()
    {
        Debug.Log($"俺の名前は {Name} : Hpは = {Hp} : 鱗の数は {ScalesNum} だ！");
    }
}

public class MonsterFactory
{
    public static Monster CreateMonster(string monsterType, string monsterName, int monsterHp, int additionalParam)
    {
        switch (monsterType)
        {
            case "Slime":
                return new Slime(monsterName, monsterHp, additionalParam);
            case "Dragon":
                return new Dragon(monsterName, monsterHp, additionalParam);
            default:
                throw new System.ArgumentException("Unknown monster type");
        }
    }
}

public class FactoryTest : MonoBehaviour
{
    Slime slime = (Slime)MonsterFactory.CreateMonster("Slime", "Blue Slime", 50, 500);
    Dragon dragon = (Dragon)MonsterFactory.CreateMonster("Dragon", "Drake", 100, 1000);

    private void Start()
    {
        slime.ExplanationMyself();
        dragon.ExplanationMyself();
    }
}
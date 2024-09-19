using UnityEngine;

public interface IStrategyWeapon
{
    void EquipmentWeapon();
}

public class BananaGun : IStrategyWeapon
{
    public void EquipmentWeapon()
    {
        Debug.Log("バナナ銃を装備");
    }
}

public class BigMarlin : IStrategyWeapon
{
    public void EquipmentWeapon()
    {
        Debug.Log("大きなカジキを装備");
    }
}

public class Hunter
{
    private IStrategyWeapon _strategyweapon;

    public void SetWeapon(IStrategyWeapon strategyweapon)
    {
        _strategyweapon = strategyweapon;
    }

    public void ExecuteEquipmentWeapon()
    {
        _strategyweapon.EquipmentWeapon();
    }
}

public class StrategyTest : MonoBehaviour
{
    Hunter hunter = new();

    // Start is called before the first frame update
    void Start()
    {
        hunter.SetWeapon(new BananaGun());
        hunter.ExecuteEquipmentWeapon();

        hunter.SetWeapon(new BigMarlin());
        hunter.ExecuteEquipmentWeapon();
    }
}
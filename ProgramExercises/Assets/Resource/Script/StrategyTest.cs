using UnityEditor;
using UnityEngine;

public interface IStrategyWeapon
{
    void EquipmentWeapon();
}

public class BananaGun : IStrategyWeapon
{
    public void EquipmentWeapon()
    {
        Debug.Log("�o�i�i�e�𑕔�");
    }
}

public class BigMarlin : IStrategyWeapon
{
    public void EquipmentWeapon()
    {
        Debug.Log("�傫�ȃJ�W�L�𑕔�");
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
        _strategyweapon?.EquipmentWeapon();
    }
}

public class StrategyTest : MonoBehaviour
{
    Hunter hunter = new();

    public void EquipBananaGun()
    {
        hunter.SetWeapon(new BananaGun());
        hunter.ExecuteEquipmentWeapon();
    }

    public void EquipBigMarlin()
    {
        hunter.SetWeapon(new BigMarlin());
        hunter.ExecuteEquipmentWeapon();
    }
}
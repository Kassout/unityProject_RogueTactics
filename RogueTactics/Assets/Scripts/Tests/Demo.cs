using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour
{
    Unit cursedUnit;
    Weapon cursedItem;
    int step;

    void OnEnable()
    {
        this.AddObserver(OnTurnCheck, BattleController.TurnBeganNotification);
    }

    void OnDisable()
    {
        this.RemoveObserver(OnTurnCheck, BattleController.TurnBeganNotification);
    }

    void OnTurnCheck(object sender, object args)
    {
        BattleState battle = sender as BattleState;
        Unit target = battle.GetComponent<BattleController>().units[0];
        switch (step)
        {
            case 0:
                EquipCursedItem(target);
                break;
            case 1:
                Add<SlowStatusEffect>(target, 3);
                break;
            default:
                UnEquipCursedItem(target);
                break;
        }

        step++;
    }

    void Add<T>(Unit target, int duration) where T : StatusEffect
    {
        DurationStatusCondition condition = target.GetComponent<Status>().Add<T, DurationStatusCondition>();
        condition.duration = duration;
    }

    void EquipCursedItem(Unit target)
    {
        cursedUnit = target;
        GameObject prefab = Resources.Load<GameObject>("Weapons/Rapier");
        GameObject obj = Instantiate(prefab, target.transform, true);
        obj.name = prefab.name;
        cursedItem = obj.GetComponent<Weapon>();
        Equipment equipment = target.GetComponent<Equipment>();
        equipment.Equip(cursedItem, EquipSlots.Weapon);
        cursedUnit.GetComponentInChildren<Inventory>().StoreObject(obj);
    }

    void UnEquipCursedItem(Unit target)
    {
        Equipment equipment = target.GetComponent<Equipment>();
        equipment.UnEquip(cursedItem);
        Destroy(cursedItem.gameObject);
        Destroy(this);
    }
}
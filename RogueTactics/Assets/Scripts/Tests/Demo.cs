using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour 
{
    Unit cursedUnit;
    Equippable cursedItem;
    int step;
    void OnEnable ()
    {
        this.AddObserver(OnTurnCheck, BattleController.TurnBeganNotification);
    }
    
    void OnDisable ()
    {
        this.RemoveObserver(OnTurnCheck, BattleController.TurnBeganNotification);
    }
    
    void OnTurnCheck (object sender, object args)
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
            case 2:
                Add<StopStatusEffect>(target, 3);
                break;
            case 3:
                Add<HasteStatusEffect>(target, 3);
                break;
            default:
                UnEquipCursedItem(target);
                break;
        }
        step++;
    }
    void Add<T> (Unit target, int duration) where T : StatusEffect
    {
        DurationStatusCondition condition = target.GetComponent<Status>().Add<T, DurationStatusCondition>();
        condition.duration = duration;
    }
    void EquipCursedItem (Unit target)
    {
        cursedUnit = target;
        GameObject obj = new GameObject("Cursed Sword");
        obj.AddComponent<AddPoisonStatusFeature>();
        cursedItem = obj.AddComponent<Equippable>();
        cursedItem.defaultSlots = EquipSlots.Weapon;
        Equipment equipment = target.GetComponent<Equipment>();
        equipment.Equip(cursedItem, EquipSlots.Weapon);
    }
    void UnEquipCursedItem (Unit target)
    {
        if (target != cursedUnit || step < 10)
            return;
        Equipment equipment = target.GetComponent<Equipment>();
        equipment.UnEquip(cursedItem);
        Destroy(cursedItem.gameObject);
        Destroy(this);
    }
}
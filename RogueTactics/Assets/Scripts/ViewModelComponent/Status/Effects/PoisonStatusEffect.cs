using UnityEngine;

public class PoisonStatusEffect : StatusEffect
{
    private Unit owner;

    private void OnEnable()
    {
        owner = GetComponentInParent<Unit>();
        if (owner)
        {
            this.AddObserver(OnNewTurn, BattleController.TurnBeganNotification);
        }
    }
    
    void OnDisable()
    {
        this.RemoveObserver(OnNewTurn, BattleController.TurnBeganNotification);
    }
    
    void OnNewTurn (object sender, object args)
    {
        UnitStats s = GetComponentInParent<UnitStats>();
        int currentHP = s[UnitStatTypes.HP];
        int maxHP = s[UnitStatTypes.MHP];
        int reduce = Mathf.Min(currentHP, Mathf.FloorToInt(maxHP * 0.1f));
        s.SetValue(UnitStatTypes.HP, (currentHP - reduce), false);
    }
}

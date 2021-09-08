using System;
using System.Collections;
using System.Collections.Generic;
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
        Stats s = GetComponentInParent<Stats>();
        int currentHP = s[StatTypes.HP];
        int maxHP = s[StatTypes.MHP];
        int reduce = Mathf.Min(currentHP, Mathf.FloorToInt(maxHP * 0.1f));
        s.SetValue(StatTypes.HP, (currentHP - reduce), false);
    }
}

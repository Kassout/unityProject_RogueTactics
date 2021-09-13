using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationStatusCondition : StatusCondition
{
    public int duration = 10;

    private void OnEnable()
    {
        this.AddObserver(OnNewTurn, BattleController.TurnBeganNotification);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnNewTurn, BattleController.TurnBeganNotification);
    }

    private void OnNewTurn(object sender, object args)
    {
        duration--;
        if (duration <= 0)
        {
            Remove();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStatusController : MonoBehaviour
{
    void OnEnable()
    {
        this.AddObserver(OnHPDidChangeNotification, UnitStats.DidChangeNotification(UnitStatTypes.HP));
    }

    void OnDisable()
    {
        this.RemoveObserver(OnHPDidChangeNotification, UnitStats.DidChangeNotification(UnitStatTypes.HP));
    }

    void OnHPDidChangeNotification(object sender, object args)
    {
        UnitStats unitStats = sender as UnitStats;
        if (unitStats[UnitStatTypes.HP] == 0)
        {
            Status status = unitStats.GetComponentInChildren<Status>();
            StatComparisonCondition c = status.Add<KnockOutStatusEffect, StatComparisonCondition>();
            c.Init(UnitStatTypes.HP, 0, c.EqualTo);
        }
    }
}
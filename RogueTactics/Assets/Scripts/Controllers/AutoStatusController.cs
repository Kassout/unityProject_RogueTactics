using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStatusController : MonoBehaviour
{
    void OnEnable()
    {
        this.AddObserver(OnHPDidChangeNotification, Stats.DidChangeNotification(StatTypes.HP));
    }

    void OnDisable()
    {
        this.RemoveObserver(OnHPDidChangeNotification, Stats.DidChangeNotification(StatTypes.HP));
    }

    void OnHPDidChangeNotification(object sender, object args)
    {
        Stats stats = sender as Stats;
        if (stats[StatTypes.HP] == 0)
        {
            Status status = stats.GetComponentInChildren<Status>();
            StatComparisonCondition c = status.Add<KnockOutStatusEffect, StatComparisonCondition>();
            c.Init(StatTypes.HP, 0, c.EqualTo);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteStatusEffect : StatusEffect
{
    private UnitStats _myUnitStats;

    private void OnEnable()
    {
        _myUnitStats = GetComponentInParent<UnitStats>();
        if (_myUnitStats)
            this.AddObserver(OnAddedStatus, Status.AddedNotification, this);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnAddedStatus, Status.AddedNotification, this);
    }

    private void OnAddedStatus(object sender, object args)
    {
        int currentMov = _myUnitStats[UnitStatTypes.MOV];
        _myUnitStats.SetValue(UnitStatTypes.MOV, (currentMov + 1), false);
    }
}

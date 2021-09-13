using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopStatusEffect : StatusEffect
{
    UnitStats _myUnitStats;
    
    void OnEnable()
    {
        _myUnitStats = GetComponentInParent<UnitStats>();
        if (_myUnitStats)
            this.AddObserver(OnAddedStatus, Status.AddedNotification, this);
        this.AddObserver(OnAutomaticHitCheck, HitRate.AutomaticHitCheckNotification);
    }
  
    void OnDisable()
    {
        this.RemoveObserver(OnAddedStatus, Status.AddedNotification, this);
        this.RemoveObserver(OnAutomaticHitCheck, HitRate.AutomaticHitCheckNotification);
    }
  
    void OnAddedStatus (object sender, object args)
    {
        int currentMov = _myUnitStats[UnitStatTypes.MOV];
        _myUnitStats.SetValue(UnitStatTypes.MOV, int.MinValue, false);
    }

    void OnAutomaticHitCheck(object sender, object args)
    {
        Unit owner = GetComponentInParent<Unit>();
        MatchException exc = args as MatchException;
        if (owner == exc.target)
        {
            exc.FlipToggle();
        }
    }
}

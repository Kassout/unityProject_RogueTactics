using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopStatusEffect : StatusEffect
{
    Stats _myStats;
    
    void OnEnable()
    {
        _myStats = GetComponentInParent<Stats>();
        if (_myStats)
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
        int currentMov = _myStats[StatTypes.MOV];
        _myStats.SetValue(StatTypes.MOV, int.MinValue, false);
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

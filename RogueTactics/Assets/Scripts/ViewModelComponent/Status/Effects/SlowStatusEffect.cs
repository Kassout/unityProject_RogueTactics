using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowStatusEffect : StatusEffect
{
    private Stats _myStats;
    
    void OnEnable ()
    {
        _myStats = GetComponentInParent<Stats>();
        if (_myStats)
            this.AddObserver(OnAddedStatus, Status.AddedNotification, this);
    }
  
    void OnDisable ()
    {
        this.RemoveObserver(OnAddedStatus, Status.AddedNotification, this);
    }
  
    void OnAddedStatus(object sender, object args)
    {
        int currentMov = _myStats[StatTypes.MOV];
        _myStats.SetValue(StatTypes.MOV, (currentMov - 1), false);
    }
}

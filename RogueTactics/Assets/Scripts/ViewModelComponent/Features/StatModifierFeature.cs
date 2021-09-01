using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifierFeature : Feature
{
    #region Fields / Properties

    public StatTypes type;
    public int amount;

    Stats stats
    {
        get
        {
            return _target.GetComponentInParent<Stats>();
        }
    }
    
    #endregion

    #region Protected

    protected override void OnApply()
    {
        stats[type] += amount;
    }

    protected override void OnRemove()
    {
        stats[type] -= amount;
    }

    #endregion

}

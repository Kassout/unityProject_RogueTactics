using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComparisonCondition : StatusCondition
{
    #region Fields

    public StatTypes type { get; private set; }

    public int value { get; private set; }
    
    public Func<bool> condition { get; private set; }

    private Stats _stats;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _stats = GetComponentInParent<Stats>();
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnStatChanged, Stats.DidChangeNotification(type), _stats);
    }

    #endregion

    #region Public

    public void Init(StatTypes type, int value, Func<bool> condition)
    {
        this.type = type;
        this.value = value;
        this.condition = condition;
        this.AddObserver(OnStatChanged, Stats.DidChangeNotification(type), _stats);
    }

    public bool EqualTo ()
    {
        return _stats[type] == value;
    }
  
    public bool LessThan ()
    {
        return _stats[type] < value;
    }
  
    public bool LessThanOrEqualTo ()
    {
        return _stats[type] <= value;
    }
  
    public bool GreaterThan ()
    {
        return _stats[type] > value;
    }
  
    public bool GreaterThanOrEqualTo ()
    {
        return _stats[type] >= value;
    }
    #endregion
    
    
    #region Notification Handlers
    
    void OnStatChanged (object sender, object args)
    {
        if (condition != null && !condition())
            Remove();
    }
    
    #endregion
}

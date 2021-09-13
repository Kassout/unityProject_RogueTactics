using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
    #region Constants

    private const int MINLevel = 1;

    private const int MAXLevel = 20;

    private const int MAXExperience = 200000;

    #endregion

    #region Fields / Properties

    public int LVL
    {
        get { return _unitStats[UnitStatTypes.LVL]; }
    }

    public int EXP
    {
        get { return _unitStats[UnitStatTypes.EXP]; }
        set { _unitStats[UnitStatTypes.EXP] = value; }
    }

    public float LevelPercent
    {
        get { return (float)(LVL - MINLevel) / (float)(MAXLevel - MINLevel); }
    }

    private UnitStats _unitStats;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _unitStats = GetComponent<UnitStats>();
    }

    private void OnEnable()
    {
        this.AddObserver(OnExpWillChange, UnitStats.WillChangeNotification(UnitStatTypes.EXP), _unitStats);
        this.AddObserver(OnExpDidChange, UnitStats.DidChangeNotification(UnitStatTypes.EXP), _unitStats);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnExpWillChange, UnitStats.WillChangeNotification(UnitStatTypes.EXP), _unitStats);
        this.RemoveObserver(OnExpDidChange, UnitStats.DidChangeNotification(UnitStatTypes.EXP), _unitStats);
    }

    #endregion

    #region Event Handlers

    void OnExpWillChange (object sender, object args)
    {
        ValueChangeException vce = args as ValueChangeException;
        vce.AddModifier(new ClampValueModifier(int.MaxValue, EXP, MAXExperience));
    }
  
    void OnExpDidChange (object sender, object args)
    {
        _unitStats.SetValue(UnitStatTypes.LVL, LevelForExperience(EXP), false);
    }

    #endregion
    
    #region Public
    
    public static int ExperienceForLevel (int level)
    {
        float levelPercent = Mathf.Clamp01((float)(level - MINLevel) / (float)(MAXLevel - MINLevel));
        return (int)EasingEquations.EaseInQuad(0, MAXExperience, levelPercent);
    }
  
    public static int LevelForExperience (int exp)
    {
        int lvl = MAXLevel;
        for (; lvl >= MINLevel; --lvl)
            if (exp >= ExperienceForLevel(lvl))
                break;
        return lvl;
    }
    
    public void Init (int level)
    {
        _unitStats.SetValue(UnitStatTypes.LVL, level, false);
        _unitStats.SetValue(UnitStatTypes.EXP, ExperienceForLevel(level), false);
    }
    
    #endregion
}

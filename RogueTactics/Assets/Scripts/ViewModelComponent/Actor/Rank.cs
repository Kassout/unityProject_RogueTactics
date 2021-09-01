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
        get { return _stats[StatTypes.LVL]; }
    }

    public int EXP
    {
        get { return _stats[StatTypes.EXP]; }
        set { _stats[StatTypes.EXP] = value; }
    }

    public float LevelPercent
    {
        get { return (float)(LVL - MINLevel) / (float)(MAXLevel - MINLevel); }
    }

    private Stats _stats;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        _stats = GetComponent<Stats>();
    }

    private void OnEnable()
    {
        this.AddObserver(OnExpWillChange, Stats.WillChangeNotification(StatTypes.EXP), _stats);
        this.AddObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), _stats);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnExpWillChange, Stats.WillChangeNotification(StatTypes.EXP), _stats);
        this.RemoveObserver(OnExpDidChange, Stats.DidChangeNotification(StatTypes.EXP), _stats);
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
        _stats.SetValue(StatTypes.LVL, LevelForExperience(EXP), false);
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
        _stats.SetValue(StatTypes.LVL, level, false);
        _stats.SetValue(StatTypes.EXP, ExperienceForLevel(level), false);
    }
    
    #endregion
}
